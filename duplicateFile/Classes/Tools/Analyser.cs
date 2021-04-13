using DataTools;

//using System.Data.SqlClient;
//using MySql.Data.MySqlClient;

using SimpleLogger;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using duplicateFile.Classes.Tools;
using System.Collections.Generic;
using duplicateFile.Classes.Qualifier;

namespace duplicateFile.Classes
{
    internal static class Analyser
    {
        #region declaration

        private static Config _config;
        private static DataSet _dset;
        private static int _currentAnalysisindexPath;

        public enum status { Deleted, Updated, New, Old }

        private static DbDataAdapter adapter;
        private static DbDataAdapter userAdapter;
        public static DbConnection conn;

        private static BackgroundWorker _bw;
        private static Stats _stats;

        private static int _updatedRowLimit = 300;
        private static int _updatedRowCount;

        private static int _updatedDisplayLimit = 20;
        private static int _updatedDisplayCount;

        private static DataView _storedFileList;

        private static bool _storedFileListeIsEmpty = false;

        private static List<Qualifier.Qualifier> _qualifiers;

        private static string[] _insertParam;
        private static DbType[] _insertType;

        internal static Stats Stats
        {
            get { return Analyser._stats; }
            set { Analyser._stats = value; }
        }

        private static readonly string[] SpFoldersNames = {
            "Cache Profiles",
            "DeviceChannels",
            "Long Running Operation Status",
            "Notification Pages",
            "Pages",
            "PublishedLinks",
            "PublishingImages",
            "Quick Deploy Items",
            "Relationships List",
            "Reports List",
            "ReusableContent",
            "SiteAssets",
            "SiteCollectionDocuments",
            "SiteCollectionImages",
            "Style Library",
            "Translation Status",
            "Variation Labels",
            "WorkflowTasks",
            "_catalogs",
            "_private",
            "images",
            "Lists"
        };

        public static DataSet Dset
        {
            get { return Analyser._dset; }
            set { Analyser._dset = value; }
        }

        public static Config Config
        {
            get { return _config; }
            set { _config = value; }
        }

        #endregion declaration

        public static void InitDataSet()
        {
            if (_config == null) return;//avoid loading config problem

            SimpleLog.Log("MySQL Database initialisation", SimpleLog.Severity.Info);

            initQualifier();


            _dset = new DataSet("analyse");


            if (_config.Bdd)
            {

                _insertParam = new string[] { "Fichier", "Chemin", "Taille", "Modified", "Created", "AnalysisPath", "lastAccess", "user", "status" };
                _insertType = new DbType[] { DbType.String, DbType.String, DbType.Int64, DbType.DateTime, DbType.DateTime, DbType.Int64, DbType.DateTime, DbType.String, DbType.Int16 };

                conn = DataTools.DataTools.SetConnection(_config.ConnectionString);

                adapter = DataTools.DataTools.getAdapter("select * from analyse", conn);

                adapter.SelectCommand.CommandTimeout = 180;
                adapter.UpdateCommand.CommandTimeout = 180;
                adapter.DeleteCommand.CommandTimeout = 180;
                adapter.InsertCommand.CommandTimeout = 180;

                userAdapter = DataTools.DataTools.getAdapter("select * from user", conn);

                adapter.UpdateCommand = conn.CreateCommand();
                adapter.UpdateCommand.CommandText = "UPDATE `analyse` SET `Fichier` = @p1,`Hash` = @p2,`Modified` = @p3,`isDuplicate` = @p4,`Taille` = @p5,`Erreur` = @p6,`Chemin` = @p7,`analysispath` = @p8,`Status` = @p9,`lastAccess` = @p10,`user` = @p11 ,`created` = @p12,`isOriginal` = @p14";
                adapter.UpdateCommand.CommandType = CommandType.Text;
                adapter.UpdateCommand.Parameters.Clear();
                adapter.UpdateCommand.Parameters.AddRange(new DbParameter[]{
                GenericParameter.Get("@p0", DbType.Int32, 300, "id"),
                GenericParameter.Get("@p1", DbType.String, 300, "Fichier"),
                GenericParameter.Get("@p2", DbType.String, 45, "Hash"),
                GenericParameter.Get("@p3", DbType.DateTime, 300, "Modified"),
                GenericParameter.Get("@p4", DbType.Int16, 1, "isDuplicate"),
                GenericParameter.Get("@p14", DbType.Int16, 1, "isOriginal"),
                GenericParameter.Get("@p5", DbType.Int64, 300, "Taille"),
                GenericParameter.Get("@p6", DbType.String, 45, "Erreur"),
                GenericParameter.Get("@p7", DbType.Int16, 300, "Chemin"),
                GenericParameter.Get("@p8", DbType.Int16, 300, "analysispath"),
                GenericParameter.Get("@p9", DbType.Int16,1, "Status"),
                GenericParameter.Get("@p10", DbType.DateTime, 300, "lastAccess"),
                GenericParameter.Get("@p11", DbType.String, 45, "user"),
                GenericParameter.Get("@p12", DbType.DateTime, 45, "created")});

                //adapter.ContinueUpdateOnError=true;//TO do

                for (int i = 0; i < _qualifiers.Count; i++)
                {
                    var q = _qualifiers[i];
                    var u = i + 20;
                    adapter.UpdateCommand.Parameters.Add(GenericParameter.Get("@p" + u, DbType.Int16, 45, q.ColumnName));

                    adapter.UpdateCommand.CommandText += $",`{q.ColumnName}` = @p{u}";
                }


                if (_qualifiers.Count > 0)
                {
                    adapter.UpdateCommand.Parameters.Add(GenericParameter.Get("@pQ", DbType.Int16, 45, "q"));
                    adapter.UpdateCommand.CommandText += $",`q` = @pQ";
                }

                //build insert command

                adapter.InsertCommand.CommandText = $"Insert into analyse ({String.Join(",", _insertParam)}) values (";
                adapter.InsertCommand.Parameters.Clear();

                for (int i = 0; i < _insertParam.Length; i++)
                {
                    var name = "@p" + i;

                    if (i > 0)
                        adapter.InsertCommand.CommandText += ",";
                    adapter.InsertCommand.CommandText += name;

                    var p = GenericParameter.Get(name, _insertType[i], 45, _insertParam[i]);
                    adapter.InsertCommand.Parameters.Add(p);
                }

                adapter.InsertCommand.CommandText += ")";

                //adapter.UpdateCommand.Parameters.Add(GenericParameter.Get("@debug", DbType.String, 45, "debug"));
                //adapter.UpdateCommand.CommandText += $",`debug` = @debug";

                adapter.UpdateCommand.CommandText += " WHERE `id` = @p0";

                loadStoredDataTable();

                _storedFileList = new DataView(_dset.Tables[0],null , "Fichier", DataViewRowState.CurrentRows);//"Status=0"

                _storedFileListeIsEmpty = _storedFileList.Count == 0;
            }
            else
            {
                var dtDoublons = new DataTable("analyse");
                dtDoublons.Columns.Add("Fichier", typeof(string));
                dtDoublons.Columns.Add("user", typeof(string));
                dtDoublons.Columns.Add("Taille", typeof(long));
                dtDoublons.Columns.Add("Modified", typeof(DateTime));
                dtDoublons.Columns.Add("lastAccess", typeof(DateTime));
                dtDoublons.Columns.Add("created", typeof(DateTime));
                dtDoublons.Columns.Add("Hash", typeof(string));
                dtDoublons.Columns.Add("Status", typeof(int));
                dtDoublons.Columns.Add("Chemin", typeof(int));
                dtDoublons.Columns.Add("isDuplicate", typeof(bool));
                dtDoublons.Columns.Add("isOriginal", typeof(bool));
                dtDoublons.Columns.Add("AnalysisPath", typeof(int));
                dtDoublons.Columns.Add("Erreur", typeof(string));

                var dtUser = new DataTable("user");
                dtUser.Columns.Add("login", typeof(string));
                dtUser.Columns.Add("email", typeof(string));
                dtUser.Columns.Add("title", typeof(string));

                foreach (var q in _qualifiers)
                    dtDoublons.Columns.Add(q.ColumnName, typeof(int));

                if (_qualifiers.Count > 0)
                    dtDoublons.Columns.Add("q", typeof(int));

                //dtDoublons.Columns.Add("debug", typeof(string));

                _dset.Tables.Add(dtDoublons);
                _dset.Tables.Add(dtUser);
            }
        }

        private static void initQualifier()
        {
            _qualifiers = new List<Qualifier.Qualifier>();

            foreach (var q in _config.Qualifiers)
            {
                Type t = Type.GetType("duplicateFile.Classes.Qualifier." + q);
                _qualifiers.Add((Qualifier.Qualifier)Activator.CreateInstance(t));
            }
        }

        private static void loadStoredDataTable()
        {
            conn.Open();
            _dset.Tables.Clear();
            var dtAnalyse = _dset.Tables.Add("analyse");
            adapter.Fill(dtAnalyse);
            var dtUsers = _dset.Tables.Add("user");
            userAdapter.Fill(dtUsers);
            conn.Close();
        }

        /// <summary>
        ///     Analyse folder : store filename and size of file
        ///     Apply sharepoint filter    
        ///     Add only modified file
        /// </summary>
        /// <param name="di">Directory info to analyse</param>
        /// <param name="isSharePointLibrary"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        public static bool AnalyseFolder(DirectoryInfo di, bool isSharePointLibrary, BackgroundWorker worker, DoWorkEventArgs e, bool start = false)
        {
            if (start)
            {
                SimpleLog.Log("Starting folder analysis", SimpleLog.Severity.Info2);
                _bw = worker;
            }

            if (!di.Exists)
            {
                SimpleLog.Log("Folder analysis canceled directory doesn't exist. " + di.FullName);
                return true;
            }

            var lnkTargetList = new List<string>();

            _stats.NumberOfDir++;
            try
            {
                isSharePointLibrary = isSharePointLibrary || di.Name == "Documents";

                //analyse files
                if (!_config.Sharepoint || (!_config.SpFolderOnly || isSharePointLibrary))//do not analyse file out of document library in sharepoint mode
                    SimpleLog.Log("Dossier " + di.Parent, SimpleLog.Severity.Info);
                foreach (var fi in di.GetFiles())
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    if (start && _config.Sharepoint && _config.FollowRootLnkFileTarget &&
                        string.Equals(fi.Extension, ".lnk", StringComparison.InvariantCultureIgnoreCase))//look for lnk file
                    {
                        var lnk = LnkReader.GetShortcutTarget(fi.FullName);
                        lnk = lnk.Trim(new char[] { '\\' }).Substring(_config.SharePointURL.Length - 7 + 1);

                        if (lnk != null) lnkTargetList.Add(di.Root + lnk);
                    }

                    var filename = fi.FullName;

                    if (_config.SpFolderOnly &&//ignore aspx files in sharepoint mode
                        _config.IgnoreAspxFile &&
                        string.Equals(fi.Extension, ".aspx", StringComparison.InvariantCultureIgnoreCase)) continue;

                    _stats.NumberOfFile++;
                    try
                    {
                        int rowIndex = -1;

                        if (_config.Bdd && !_storedFileListeIsEmpty)
                        {
                            rowIndex = _storedFileList.Find(filename);

                            if (rowIndex == -1)//file doesn't exist in db
                            {
                                string user = "";// System.IO.File.GetAccessControl(filename).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
                                var dr = AddFileRecord(filename, _currentAnalysisindexPath, fi.Length, fi.LastWriteTime, fi.LastAccessTime, fi.CreationTime, user);
                                buildQualifierNote(dr, fi);
                            }
                            else
                            {//file exist verify date to see if analysis is needed 
                                var modifiedDate = DateTime.Parse(fi.LastWriteTime.ToString());//millisecond problem

                                if ((DateTime)_storedFileList[rowIndex]["Modified"] < modifiedDate)//file have been modified
                                {
                                    var row = _storedFileList[rowIndex];
                                    string user = "";

                                    buildQualifierNote(row.Row, fi, true);

                                    row["Modified"] = modifiedDate;
                                    row["isDuplicate"] = false;
                                    row["Status"] = status.Updated;
                                    row["Hash"] = "";
                                    row["user"] = user;
                                }
                                else
                                {
                                    var row = _storedFileList[rowIndex];
                                    if (row.Row["q"] == DBNull.Value)
                                        buildQualifierNote(row.Row, fi);//warning must be first modifying item in _storedFileList change row order
                                    row["Status"] = status.Old;
                                }

                                updateDataBase(false);
                            }
                        }
                        else//if no database used
                        {
                            string user = "";// System.IO.File.GetAccessControl(filename).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
                            var dr = AddFileRecord(filename, _currentAnalysisindexPath, fi.Length, fi.LastWriteTime, fi.LastAccessTime, fi.CreationTime, user);
                            buildQualifierNote(dr, fi);
                        }

                    }
                    catch (Exception ex)
                    {
                        if (_config.EnableLog) SimpleLog.Log("File :" + fi.FullName + "Error :" + ex.Message, SimpleLog.Severity.Error);
                    }
                }
                //analyse subfolders
                foreach (var d in di.GetDirectories())
                {
                    if (!_config.Sharepoint ||
                        ((!isSharePointLibrary || d.Name != "Forms") && //ignore Forms folders in sharepoint library
                        (!SpFoldersNames.Contains(d.Name) || !_config.SpFolderOnly)))
                        AnalyseFolder(d, isSharePointLibrary, worker, e);
                    if (!worker.CancellationPending) continue;
                    //conn.Close();
                    e.Cancel = true;
                    break;
                }
                //analyse Lnk target
                foreach (var l in lnkTargetList)
                {
                    var lnkdi = new DirectoryInfo(l);
                    if (!_config.Sharepoint ||
                        ((!isSharePointLibrary || lnkdi.Name != "Forms") && //ignore Forms folders in sharepoint library
                        (!SpFoldersNames.Contains(lnkdi.Name) || !_config.SpFolderOnly)))
                        AnalyseFolder(lnkdi, isSharePointLibrary, worker, e);
                    if (!worker.CancellationPending) continue;
                    //conn.Close();
                    e.Cancel = true;
                    break;
                }
            }
            catch (Exception ex)
            {
                if (_config.EnableLog) SimpleLog.Log(ex);
            }

            if (conn!=null) conn.Close();

            SimpleLog.Log("End of folder analysis");

            return false;
        }
        /// <summary>
        /// calculate for qualifier the note for a given file
        /// if force is set to true old value are replaced
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="fi"></param>
        /// <param name="force"></param>
        private static void buildQualifierNote(DataRow dr, FileInfo fi, bool force = false)
        {
            var st = DateTime.Now;

            if (_qualifiers.Count == 0) return;

            if (!Qualifier.Qualifier.AllowedExtension.Contains(fi.Extension.ToLower()))//allowed extension
            {
                dr["q"] = -1;
                return;
            }

            if (fi.Length == 0 || fi.Length < _config.QualifierMini) return;

            var sum = 0;
            SimpleLog.Log($"Start qualifier file :" + fi.FullName, SimpleLog.Severity.Warning);
            var txt = "";

            if (ContentHash.isOfficeFile(fi.Extension))
            {
                txt = OfficeContentExtractor.GetContent(fi);
            }
            else
            {
                using (var sr = new StreamReader(fi.FullName))
                {
                    txt = sr.ReadToEnd();
                }
            }

            if (txt != null)//content could be analysed
            {
                foreach (var q in _qualifiers)
                {
                    if (dr[q.ColumnName] == DBNull.Value || force)//qulifier is not set or force is used
                    {
                        var r = q.Note(txt, fi);//get qualifier value
                        dr[q.ColumnName] = r;//store in result table
                    }
                    sum += (int)dr[q.ColumnName];//calculated aggregated qualifier value
                }

                dr["q"] = sum;//store sum in result table
            }

            Stats.AddQualifierDuration(DateTime.Now - st);

            //dr["debug"] = fi.FullName;//for debug purpose
        }

        /// <summary>
        /// Loop throught file list to find duplicate
        /// file with status<>0 are analysed
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        public static void AnalyseFileSet(BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                SimpleLog.Log("Analyse file set", SimpleLog.Severity.Info2);
                updateDataBase(false, false);
                if (_config.Bdd) loadStoredDataTable();//reload database to get records id

                _updatedRowCount = 0;
                _updatedDisplayCount = 0;

                var filter = _config.MinFileSize > 0 ? "Taille>" + _config.MinFileSize + " and status<>0" : "status<>0";
                var dv = new DataView(_dset.Tables[0], filter, "Taille asc", DataViewRowState.CurrentRows);

                worker.ReportProgress(0,
                    new WaitingFormProperties(Resources.Languages.Resources.Txt_duplicate_file_search, "", 100,
                    ProgressBarStyle.Continuous));
                var offset2 = 2;

                for (var a = 0; a < dv.Count - 1; a += offset2)
                {
                    if (worker.CancellationPending)
                    {
                        //conn.Close();
                        e.Cancel = true;
                        break;
                    }

                    var offset = 1;
                    offset2 = -1;

                    worker.ReportProgress(a,
                        new WaitingFormProperties(Resources.Languages.Resources.Txt_duplicate_file_search, dv[a]["Fichier"].ToString(),
                            dv.Count, ProgressBarStyle.Continuous));

                    var maxSize = ContentHash.isOfficeFile(dv[a]["Fichier"].ToString()) ? _config.MaxSizeDifference : 0.1;//use delta size for office file only

                    if (Math.Abs((long)dv[a]["Taille"] - (long)dv[a + 1]["Taille"]) < maxSize)
                    {
                        string lastError;
                        var flgContinue = true;

                        if (string.IsNullOrEmpty(dv[a]["Hash"].ToString()))//calculate first file hash if needed
                        {
                            var dt = DateTime.Now;
                            dv[a]["Hash"] = ContentHash.GetHash(dv[a]["Fichier"].ToString(), out lastError);
                            _stats.CalculatedHash++;
                            SimpleLog.Log($"Hash : {DateTime.Now - dt}");
                            updateDataBase(false, false);
                        }
                        var currentHash = (string)dv[a]["Hash"];

                        flgContinue = currentHash != "-1";

                        while (a + offset < dv.Count && flgContinue)
                        {
                            flgContinue = Math.Abs((long)dv[a]["Taille"] - (long)dv[a + offset]["Taille"]) < maxSize;
                            if (!flgContinue) continue;

                            //handle extension
                            bool isSameExtension = true;
                            if (_config.UseExtension) isSameExtension =
                                    Path.GetExtension(dv[a]["Fichier"].ToString()) == Path.GetExtension(dv[a + offset]["Fichier"].ToString());

                            //is hash calculation needed for file to compare
                            if (string.IsNullOrEmpty(dv[a + offset]["Hash"].ToString()) && isSameExtension)
                            {
                                var dt = DateTime.Now;
                                dv[a + offset]["Hash"] = ContentHash.GetHash(dv[a + offset]["Fichier"].ToString(), out lastError);
                                _stats.CalculatedHash++;
                                SimpleLog.Log($"Hash(2) : {DateTime.Now - dt}");
                                updateDataBase(false, false);
                            }

                            if (currentHash == dv[a + offset]["Hash"].ToString())
                            {
                                dv[a]["isDuplicate"] = 1;
                                dv[a + offset]["isDuplicate"] = 1;

                                if (_config.Sharepoint)
                                {

                                    SetUser(dv[a]);
                                    SetUser(dv[a + offset]);

                                }

                                updateDataBase(false, false);
                            }
                            else
                            {
                                if (offset2 == -1) offset2 = offset;
                            }

                            offset++;
                        }
                    }
                    if (offset2 == -1) offset2 = offset;
                }

                updateDataBase(true, false);

            }
            catch (Exception ex)
            {
                if (_config.EnableLog) SimpleLog.Log(ex);
            }

            finally
            {
                if (conn!=null) conn.Close();
            }

            SimpleLog.Log("End of Fileset analysis", SimpleLog.Severity.Info2);
        }


        private static void SetUser(DataRowView drFile)
        {
            try
            {
                if (!string.IsNullOrEmpty(drFile["user"].ToString())) return;

                var user = SPTools.getFileLastModifiedUser(_config.SharePointURL, drFile["Fichier"].ToString());
                drFile["user"] = user.login;
                var dr = _dset.Tables[1].NewRow();
                dr["login"] = user.login;
                dr["email"] = user.mail;
                dr["title"] = user.title;
                DataTools.DataTools.ImportRowIfNotExists(_dset.Tables[1], dr, "login");
            }
            catch (Exception uex)
            {
                if (_config.EnableLog)
                {
                    SimpleLog.Log("Error setting user file :" + drFile["Fichier"] + " or " + drFile["Fichier"]);
                    SimpleLog.Log(uex);
                }
            }
        }

        private static DataRow AddFileRecord(string fichier, int usedAnalysisPath, long size, DateTime modif, DateTime access, DateTime creation, string user)
        {
            SimpleLog.Log("Add file record " + fichier);
            var st = DateTime.Now;
            var dr = _dset.Tables[0].NewRow();
            dr["Fichier"] = fichier;
            dr["Chemin"] = fichier.Length;
            dr["Taille"] = size;
            dr["Modified"] = modif;
            dr["Created"] = creation;
            dr["AnalysisPath"] = usedAnalysisPath;
            dr["lastAccess"] = access;
            dr["user"] = user;
            dr["status"] = status.New;
            _dset.Tables[0].Rows.Add(dr);

            if (_config.Bdd)
            {
                var cmd = (MySql.Data.MySqlClient.MySqlCommand)adapter.InsertCommand;

                for (int i = 0; i < _insertParam.Length; i++)
                {
                    cmd.Parameters["@p" + i].Value = dr[_insertParam[i]];
                }

                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();

                dr["id"] = Convert.ToInt32(cmd.LastInsertedId);

                dr.AcceptChanges();
            }

            Stats.AddDataBaseAccessDuration(DateTime.Now - st);

            updateDisplay();

            return dr;
        }

        public static void DisposeDataBase()
        {
            if (!_config.Bdd && Dset != null)
            {
                if (Dset.Tables.Count > 0) Dset.Tables[0].Clear();
                Dset.Clear();
                Dset.Dispose();
                Dset = null;
            }

            Analyser.InitDataSet();
            GC.Collect();
        }

        /// <summary>
        /// Stats dataTable and add current stats
        /// </summary>
        public static void setStats()
        {
            if (!_config.Bdd) return;
            //_stats = new Stats();
            //_stats.DuplicateFileSize = new FileSize(1024);
            //_dset = new DataSet();

            try
            {
                var conn = DataTools.DataTools.SetConnection(Analyser.Config.ConnectionString);
                var stAdapter = DataTools.DataTools.getAdapter("select * from stats", conn);

                var dtStats = _dset.Tables.Add("Stats");

                stAdapter.FillSchema(dtStats, SchemaType.Source);
                //dtStats.Columns["Duration"].DataType = typeof(TimeSpan);
                dtStats.Columns["DuplicateFileSize"].DataType = typeof(Int64);

                conn.Open();
                stAdapter.Fill(dtStats);

                SimpleLog.Log("Adding stats to database");
                var row = dtStats.NewRow();
                _stats.ToDataRow(row);

                dtStats.Rows.Add(row);
                stAdapter.Update(dtStats);

                conn.Close();
            }
            catch (Exception e)
            {
                SimpleLog.Log(e);
            }
        }

        public static void updateDataBase(bool force = false, bool callupdateDisplay = true)
        {
            try
            {
                _updatedRowCount++;

                //SimpleLog.Log("Updating data base");
                if (_updatedRowCount > _updatedRowLimit || force)
                {
                    if (_config.Bdd)
                    {
                        var st = DateTime.Now;
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        adapter.Update(_dset.Tables[0]);
                        userAdapter.Update(_dset.Tables[1]);
                        Stats.AddDataBaseAccessDuration(DateTime.Now - st);

                    }
                    _updatedRowCount = 0;
                }


                if (callupdateDisplay) updateDisplay();


            }
            catch (Exception e)
            {
                if (_config.EnableLog) SimpleLog.Log(e);
                if (_config.Bdd) conn.Close();
            }
            //finally
            //{
            //    if (_config.Bdd) conn.Close();
            //}
        }

        static void updateDisplay()
        {
            _updatedDisplayCount++;

            if (_updatedDisplayCount > _updatedDisplayLimit)
            {
                _bw.ReportProgress(100,
                    new WaitingFormProperties(
                    Resources.Languages.Resources.Txt_Getting_File_List, _stats.NumberOfFile + Resources.Languages.Resources.Txt_Files + "\r\n" +
                    string.Format("{0:# ###,##}", _stats.Rate) + Resources.Languages.Resources.Txt_Files_Seconde,
                    100, ProgressBarStyle.Marquee));
                _updatedDisplayCount = 0;
            }
        }
    }
}