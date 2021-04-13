using duplicateFile.Classes;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using duplicateFile.Classes.Tools;
using duplicateFile.Classes.Qualifier;

namespace duplicateFile
{
    public partial class MainForm : Form
    {
        private int _cmtpDoub;

        //private TimeSpan _analyseDuration;
        private int _currentAnalysisindexPath;

        //private string[] _currentAnalysisPath;

        private DataView _dv2003;
        private DataView _dvChemins;
        private DataView _dvDoublons;
        private DataView _dvErreurs;
        private DataView _dvQualifier;
        private string _saveAs = "";
        private int _selectedTab;
        private Waiting _wf;

        private readonly BackgroundWorker _bwSupp = new BackgroundWorker();
        private BackgroundWorker _gbw;

        private bool automaticMode = false;
        private Guid _jobId;

        private string _pathFilterMem;

        private enum exportType { Excel, PDF };

        public MainForm(string[] args)
        {

            InitializeComponent();

            string job = null;

            if (args.Length > 0)
            {
                automaticMode = true;
                job = Config.DefaultJobPath + "\\" + args[0] + ".conf";
                SimpleLog.Log("Automatic mode job id:" + args[0]);
                _jobId = new Guid(args[0]);
            }

            InitConfig(job);
            SimpleLog.Log("Config loaded");

            SimpleLog.LogLevel = (SimpleLog.Severity)Analyser.Config.VerboseLevel;

            //SimpleLog.LogLevel = SimpleLog.Severity.Info;

            if (automaticMode)
            {
                if (!string.IsNullOrEmpty(Analyser.Config.SharePointDisk) &&
                    !string.IsNullOrEmpty(Analyser.Config.SharePointURL))
                    connectNetWorkDrive(Analyser.Config.SharePointDisk, Analyser.Config.SharePointURL);

                startAnalyse();
            }
        }

        private void EndAnalyse(bool canceled)
        {
            SimpleLog.Log("End analyse deleteing old file");
            if (Analyser.Config.Bdd&&!canceled)
                DataTools.DataTools.Command("Delete From analyse Where Status=0;", Analyser.Config.ConnectionString);//remove all file taged as deleted

            SimpleLog.Log("End analyse file deleted");

            _dvDoublons = Analyser.Config.Bdd ?
                new DataView(Analyser.Dset.Tables[0], "isDuplicate = 1", "Hash asc", DataViewRowState.CurrentRows) :
                new DataView(Analyser.Dset.Tables[0], "isDuplicate = 1", "Hash asc", DataViewRowState.Added);

            SimpleLog.Log("Build stat");

            Analyser.Stats.NumberOfDuplicateFile = Analyser.Dset.Tables[0].Rows.Count;
            Analyser.Stats.EndDate = DateTime.Now;
            Analyser.Stats.NumberOfDuplicateFile = _dvDoublons.Count;
            Analyser.Stats.TotalFileSize.Size = Analyser.Dset.Tables[0].Rows.Count == 0 ? 0 : (long)Analyser.Dset.Tables[0].Compute("Sum(Taille)", "");

            toolStripStatusLabel1.Text = string.Format(Resources.Languages.Resources.Txt_Repertoire_Analyses,
                Analyser.Stats.NumberOfDir,
                Analyser.Stats.NumberOfFile,
                TimeSpan.FromSeconds(Analyser.Stats.Duration).ToReadableString());//_analyseDuration.ToReadableString()


        }

        private void initDataBase()
        {
            try
            {
                ControlState(false);

                InitWorker(dbLoad, genericBackgroundWorkerEnd);

                _wf = new Waiting
                {
                    label = { Text = Resources.Languages.Resources.Txt_Chargement_des_données },
                    progressBar = { Style = ProgressBarStyle.Marquee },
                    Wr = _gbw
                };

                _gbw.RunWorkerAsync();
                _wf.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show(Resources.Languages.Resources.Txt_LA_Base_est_inaccessible);
                Analyser.Config.Bdd = false;
                SimpleLog.Log("Database connection error :" + e.Message, SimpleLog.Severity.Error);
            }
        }

        private void DataSourceChanged(object sender, EventArgs e)
        {
            var name = ((DataGridView)sender).Name;

            switch (name)
            {
                case "dgvDoublons":
                    lbNbDoublons.Text = string.Format("{0} doublon(s) détecté(s)", _dvDoublons.Count);
                    break;

                case "dgv2003":
                    lbNb2003.Text = string.Format("{0} fichiers Office 2003 détectés", _dv2003.Count);
                    break;
            }
        }


        #region init

        private void InitWorker(DoWorkEventHandler doWork, RunWorkerCompletedEventHandler endWork)
        {
            _gbw = new BackgroundWorker();
            _gbw.WorkerSupportsCancellation = true;
            _gbw.WorkerReportsProgress = true;
            _gbw.DoWork += doWork;
            _gbw.ProgressChanged += bw_ProgressChanged;
            _gbw.RunWorkerCompleted += endWork;
        }

        /// <summary>
        /// Set binding and load default config
        /// </summary>
        private void InitConfig(string job)
        {
            Analyser.Config = Config.Load(job);
            ControlState(false);
            DataTools.DataTools.DefaultProviderName = "MySql.Data.MySqlClient";

            Analyser.Config = Analyser.Config;
            Analyser.Dset = Analyser.Dset;
        }

        #endregion init

        #region display

        private void ControlState(bool enabled)
        {
            dgvChemin.Enabled = dgvDoublons.Enabled = dgvErreurs.Enabled = dgv2003.Enabled = dgvQualifier.Enabled = enabled;

            btnOpen.Enabled =
                btnSupp.Enabled =
                    btnOpenFolder.Enabled =
                        btnExport.Enabled =
                            btnCharge.Enabled = btnEnrg.Enabled = btnCorr.Enabled = btnConvertir.Enabled = enabled;

            btnGraph.Enabled = Analyser.Config.Bdd;

            if (!Analyser.Config.Duplicate) dgvDoublons.Enabled = false;
        }

        private void Display(bool endAnalyse)
        {
            SimpleLog.Log("Finalise...");
            if (Analyser.Config.Duplicate)
            {

                dgvDoublons.DataSource = _dvDoublons;
                if (_dvDoublons!=null&&_dvDoublons.Count < 5000)
                    dgvDoublons.AutoResizeColumns(
                        DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                hideColumnFromList(dgvDoublons, new string[] { "Erreur", "Chemin", "isDuplicate", "AnalysisPath", "debug" });

                if (Analyser.Config.Bdd) dgvDoublons.Columns["id"].Visible = false;

                hideQualifierColumn(dgvDoublons);

                ResourceHelper.setHeaders(dgvDoublons);

                Finalize(endAnalyse);
            }

            UpdateChemin();

            _dvErreurs = new DataView(Analyser.Dset.Tables[0], "Erreur<>''", "Fichier asc", DataViewRowState.CurrentRows);
            dgvErreurs.DataSource = _dvErreurs;
            dgvErreurs.AutoResizeColumns(
                DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

            hideColumnFromList(dgvErreurs, new string[] { "Chemin", "Taille", "isDuplicate", "Hash", "AnalysisPath", "isOriginal", "debug" });

            if (Analyser.Config.Bdd) dgvErreurs.Columns["id"].Visible = false;
            hideQualifierColumn(dgvErreurs);

            ResourceHelper.setHeaders(dgvErreurs);

            _dv2003 = new DataView(Analyser.Dset.Tables[0],
                "Fichier like '%.doc' or Fichier like '%.rtf' or Fichier like '%.dot' or Fichier like '%.xls' or Fichier like '%.ppt'"
                , "Fichier asc", DataViewRowState.CurrentRows);

            dgv2003.DataSource = _dv2003;
            dgv2003.AutoResizeColumns(
                DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

            hideColumnFromList(dgv2003, new string[] { "Erreur", "Chemin", "isDuplicate", "Hash", "AnalysisPath", "isOriginal"});

            if (Analyser.Config.Bdd) dgv2003.Columns["id"].Visible = false;
            hideQualifierColumn(dgv2003);

            ResourceHelper.setHeaders(dgv2003);

            if (Analyser.Config.Qualifiers.Count > 0)
                updateQualifier();
            else
            {
                tabControl1.TabPages["tabQualifier"].Hide();
            }
            if (endAnalyse) Analyser.setStats();

            if (endAnalyse)
                if (Analyser.Config.Mail)
                    if (!string.IsNullOrEmpty(Analyser.Config.ReportMail))
                        SMTPHelper.sendReport(dgvDoublons, dgvErreurs);


            if (automaticMode) Application.Exit();
        }

        private void hideColumnFromList(DataGridView dgv, string[] lst)
        {
            foreach (var columnName in lst)
            {
                if (dgv.Columns.Contains(columnName)) dgv.Columns[columnName].Visible = false;
            }
        }

        private void hideQualifierColumn(DataGridView dgv)
        {
            foreach (var q in Analyser.Config.Qualifiers)
            {
                Type t = Type.GetType("duplicateFile.Classes.Qualifier." + q);
                var qo = (Qualifier)Activator.CreateInstance(t);

                dgv.Columns[qo.ColumnName].Visible = false;
            }

            if (dgv.Columns.Contains("q")) dgv.Columns["q"].Visible = false;
        }

        /// <summary>
        /// Set background color of duplicate file, original flag and calculate duplicate file summary size
        /// </summary>
        private void Finalize(bool endAnalyse)
        {
            if (dgvDoublons.Rows.Count < 2) return;
            var memHash = dgvDoublons.Rows[0].Cells["Hash"].Value.ToString();
            Color[] colTab = { Color.FromArgb(202, 244, 181), Color.FromArgb(255, 212, 157) };

            var colorInUse = 0;
            DateTime? originalCreation = null;
            int originalID = 0;



            for (var a = 0; a < dgvDoublons.Rows.Count; a++)
            {
                if (memHash != dgvDoublons.Rows[a].Cells["Hash"].Value.ToString())
                {
                    colorInUse = colorInUse == 0 ? 1 : 0;
                    memHash = dgvDoublons.Rows[a].Cells["Hash"].Value.ToString();
                    originalCreation = null;
                    dgvDoublons.Rows[originalID].Cells["isOriginal"].Value = 1;
                }
                else
                    if (endAnalyse) Analyser.Stats.DuplicateFileSize.Add((long)dgvDoublons.Rows[a].Cells["Taille"].Value);

                if (originalCreation == null || originalCreation > (DateTime)dgvDoublons.Rows[a].Cells["Created"].Value)
                {
                    originalCreation = (DateTime)dgvDoublons.Rows[a].Cells["Created"].Value;
                    originalID = a;
                }

                dgvDoublons.Rows[a].DefaultCellStyle.BackColor = colTab[colorInUse];
            }



            if (endAnalyse)
            {
                Analyser.updateDataBase(true);
                SimpleLog.Log("Analyse ended", SimpleLog.Severity.Info2);
            }

        }
        /// <summary>
        /// Hide duplicate file that do not implicate user
        /// </summary>
        /// <param name="user"></param>
        private void userFilter(string user)
        {
            for (var a = 0; a < dgvDoublons.Rows.Count; a++)
            {
                dgvDoublons.Rows[a].Visible = false;
            }
        }
        private void UpdateChemin()
        {
            _dvChemins = new DataView(Analyser.Dset.Tables[0], "Chemin>" + trackBar1.Value, "Fichier asc",
                DataViewRowState.CurrentRows);
            dgvChemin.DataSource = _dvChemins;
            dgvChemin.AutoResizeColumns(
                DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

            hideColumnFromList(dgvChemin, new string[] { "Erreur", "isDuplicate", "Hash", "AnalysisPath", "isOriginal", "debug" });

            hideQualifierColumn(dgvChemin);

            ResourceHelper.setHeaders(dgvChemin);
            lbNbChemin.Text = string.Format(Resources.Languages.Resources.Txt__0__chemin_s__détecté_s_, _dvChemins.Count);
        }

        private void updateQualifier()
        {
            tabControl1.TabPages["tabQualifier"].Show();

            var filter = $"q>" + trackQualifier.Value;

            if (!string.IsNullOrEmpty(txtQualPath.Text))
                filter += $" and fichier like '{txtQualPath.Text}%'";

            _dvQualifier = new DataView(Analyser.Dset.Tables[0], filter, "q desc", DataViewRowState.CurrentRows);
            dgvQualifier.DataSource = _dvQualifier;
            dgvQualifier.AutoResizeColumns(
                DataGridViewAutoSizeColumnsMode.AllCells);

            hideColumnFromList(dgvQualifier, new string[] { "Erreur", "User", "Status", "Chemin", "isDuplicate", "Hash", "AnalysisPath", "isOriginal", "debug" });

            if (Analyser.Config.Bdd) dgvQualifier.Columns["id"].Visible = false;

            ResourceHelper.setHeaders(dgvQualifier);

            lblNbQualifier.Text = dgvQualifier.Rows.Count + duplicateFile.Properties.Resources.NbQualifier;
        }

        private void DisposeDataBase()
        {
            dgvChemin.DataSource = dgvDoublons.DataSource = dgvErreurs.DataSource = dgv2003.DataSource = null;
            Analyser.DisposeDataBase();
        }

        #endregion display

        #region event

        private void btnConvertir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK) return;

            btnAnalyse.Text = Resources.Languages.Resources.Txt_Annuler;
            ControlState(false);
            InitWorker(bwConv_DoWork, genericBackgroundWorkerEnd);

            _wf = new Waiting
            {
                label = { Text = Resources.Languages.Resources.Txt_Conversion_en_cours___ },
                progressBar = { Style = ProgressBarStyle.Continuous },
                Wr = _gbw
            };

            _gbw.RunWorkerAsync();
            _wf.ShowDialog();

            btnAnalyse.Text = Resources.Languages.Resources.Txt_Analyser;
        }

        //Analyse
        private void button1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = Resources.Languages.Resources.Txt_Analyse_en_cours___;
            var fbd = new FoldersSelect();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                if (fbd.SelectedFolders.Length == 0) return;
                //if (_bw.IsBusy) return;
                Analyser.Config.Paths = fbd.SelectedFolders;//_currentAnalysisPath
                startAnalyse();
            }
            else
            {
                toolStripStatusLabel1.Text = "";
                btnAnalyse.Text = Resources.Languages.Resources.Txt_Analyser;
            }
        }

        private void startAnalyse()
        {
            SimpleLog.Log("Analyse started", SimpleLog.Severity.Info2);

            Analyser.Stats = new Stats();

            if (_jobId != null) Analyser.Stats.JobId = _jobId;

            ControlState(false);

            _currentAnalysisindexPath = 0;

            InitWorker(doAnalysisBackground, analysisBackgroundWorkerEnd);

            _wf = new Waiting
            {
                label = { Text = Resources.Languages.Resources.Txt_Analyse_des_fichiers_en_cours___ },
                lbStatus = { Text = Resources.Languages.Resources.Txt_Getting_File_List },
                progressBar = { Style = ProgressBarStyle.Marquee },
                Wr = _gbw
            };

            if (Analyser.Config.Bdd)
            {
                SimpleLog.Log("Database initialisation", SimpleLog.Severity.Info);
                DataTools.DataTools.Command("Update analyse Set Status=0,isDuplicate=0,isOriginal=0;", Analyser.Config.ConnectionString);//all file taged as deleted
            }

            DisposeDataBase();

            _gbw.RunWorkerAsync();
            if (!automaticMode) _wf.ShowDialog();

            btnAnalyse.Text = Resources.Languages.Resources.Txt_Analyser;
        }

        //Correcteur
        private void btnCorr_Click()
        {
            var pc = new PathCorrector { Paths = Analyser.Dset.Tables[0] };

            pc.ShowDialog();
        }

        //divers
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState == null) return;
            _wf.lbStatus.Text = ((WaitingFormProperties)e.UserState).Status;
            _wf.progressBar.Maximum = ((WaitingFormProperties)e.UserState).MaxProgressValue;
            _wf.progressBar.Value = e.ProgressPercentage;
            _wf.txtFile.Text = ((WaitingFormProperties)e.UserState).CurrentFile;
            _wf.progressBar.Style = ((WaitingFormProperties)e.UserState).ProgressStyle;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            lbNbCarac.Text = string.Format(Resources.Languages.Resources.Txt___0__caractères_, trackBar1.Value);
            UpdateChemin();
        }

        private void ItemContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Process.Start(((DataGridView)sender).Rows[e.RowIndex].Cells["Fichier"].Value.ToString());
        }
        /// <summary>
        /// Open selected files folders
        /// </summary>
        private void boutonPlus1_Click()
        {
            openSelectFilesFolders();
        }

        private void btnOpen_Click()
        {
            openSelectedFiles();
        }

        private void openSelectFilesFolders()
        {
            foreach (var f in GetSelectedFileName())
            {
                try
                {
                    Process.Start(Path.GetDirectoryName(f));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), Resources.Languages.Resources.Erreur,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void openSelectedFiles()
        {
            foreach (var f in GetSelectedFileName())
            {
                Process.Start(f);
            }
        }

        private void boutonPlus2_Click()
        {
            ControlState(false);

            var saveFileDialog1 = new SaveFileDialog
            {
                InitialDirectory = @"C:\",
                Title = Resources.Languages.Resources.Txt_Export_Excel_PDF,
                CheckPathExists = true,
                Filter = "PDF files (*.pdf)|*.pdf|Excel files (*.XLSX)|*.XLSX",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            InitWorker(bwExp_DoWork, genericBackgroundWorkerEnd);

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {


                _wf = new Waiting
                {
                    label = { Text = Resources.Languages.Resources.Txt_Export },
                    progressBar = { Style = ProgressBarStyle.Marquee },
                    Wr = _gbw
                };

                _saveAs = saveFileDialog1.FileName;

                _gbw.RunWorkerAsync(saveFileDialog1.FilterIndex);
                _wf.ShowDialog();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedTab = tabControl1.SelectedIndex;
        }

        private void dgvDoublons_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = true;
            if (_bwSupp.IsBusy) return;
            ControlState(false);
            _wf = new Waiting
            {
                label = { Text = Resources.Languages.Resources.Txt_Suppression_des_fichiers_en_cours___ },
                progressBar = { Style = ProgressBarStyle.Continuous },
                Wr = _bwSupp
            };
            //wf.Show();
            _bwSupp.RunWorkerAsync();
            _wf.ShowDialog();
        }

        private void btnEnrg_Click()
        {
            var saveFileDialog1 = new SaveFileDialog
            {
                InitialDirectory = @"C:\",
                Title = Resources.Languages.Resources.Txt_Enregistrer_l_analyse,
                CheckPathExists = true,
                DefaultExt = "afd",
                Filter = Resources.Languages.Resources.Txt_Analyse____afd____afd,
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            var fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
            Analyser.Dset.Tables[0].WriteXml(fs);
            fs.Close();
        }

        private void btnCharge_Click()
        {
            if (Analyser.Dset.Tables[0].Rows.Count > 0)
            {
                if (
                    MessageBox.Show(
                        Resources.Languages.Resources.Txt_Si_vous_continuez_les_données_actuelles_seront_perdues__Souhaitez_vous_continuer_,
                        Resources.Languages.Resources.Txt_Information, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)
                    return;
            }
            var dialog = new OpenFileDialog
            {
                Filter = Resources.Languages.Resources.Txt_Analyse____afd____afd,
                InitialDirectory = "C:",
                Title = Resources.Languages.Resources.Txt_Selectionnez_un_fichier
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DisposeDataBase();
                var fs = new FileStream(dialog.FileName, FileMode.Open);
                Analyser.Dset.Tables[0].ReadXml(fs);
                fs.Close();
            }

            statusStrip1.Text = string.Format(Resources.Languages.Resources.Txt_0_Fichiers, Analyser.Dset.Tables[0].Rows.Count);

            _dvDoublons = new DataView(Analyser.Dset.Tables[0], "", "Hash asc", DataViewRowState.CurrentRows)
            {
                RowFilter = "isDuplicate = 1"
            };

            Display(false);

            ControlState(true);
        }

        #endregion event

        #region Background jobs

        private void dbLoad(object sender, DoWorkEventArgs e)
        {

            Analyser.InitDataSet();

        }

        private void drawChart(object sender, DoWorkEventArgs e)
        {
            Classes.Charts.ChartTools.draw();
        }

        private void genericBackgroundWorkerEnd(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!automaticMode) _wf.Close();
            ControlState(true);
        }

        private void analysisBackgroundWorkerEnd(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!automaticMode) _wf.Close();
            ControlState(true);

            tabControl1.SelectTab(0);
            Display(true);
        }

        private void doAnalysisBackground(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            bool canceled = false;

            for (_currentAnalysisindexPath = 0;
                _currentAnalysisindexPath < Analyser.Config.Paths.Count();
                _currentAnalysisindexPath++)
            {
                var di = new DirectoryInfo(Analyser.Config.Paths[_currentAnalysisindexPath]);
                //find files sizes
                canceled = Analyser.AnalyseFolder(di, false, worker, e, true);

                if (canceled) return;
            }


            Analyser.Stats.DeletedFiles = Analyser.Dset.Tables[0].Select("status=0").Length;
            Analyser.Stats.NewFiles = Analyser.Dset.Tables[0].Select("status=" + ((int)Analyser.status.New)).Length;

            //find files with same size and get hash

            if (Analyser.Config.Duplicate) Analyser.AnalyseFileSet(worker, e);

            canceled = e.Cancel;
            
            worker.ReportProgress(0, new WaitingFormProperties(Resources.Languages.Resources.Txt_Analyse_des_résultats));
            EndAnalyse(canceled);
        }

        //Conversion
        private void bwConv_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            var cmpt = 0;
            OfficeConverter.SilentMode = true;

            var drtc = new List<DataGridViewRow>();
            var newFiles = new List<string>();

            OfficeConverter.KeepOpen = true;

            foreach (DataGridViewRow dr
                in dgv2003.Rows.Cast<DataGridViewRow>().Where(dr => dr.Cells["Fichier"].Value != null))
            {
                cmpt++;
                if (worker != null)
                {
                    worker.ReportProgress((int)(100 * (cmpt / ((double)dgv2003.RowCount))));
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                }
                var filePath = dr.Cells["Fichier"].Value.ToString();
                var path = Path.GetDirectoryName(filePath);
                var parentAnalysisPath = Path.GetDirectoryName(Analyser.Config.Paths[(int)dr.Cells["AnalysisPath"].Value]);

                var relativePath = parentAnalysisPath == null ?
                    "\\" + Analyser.Config.Paths[(int)dr.Cells["AnalysisPath"].Value].Substring(0, 1) ://drive analysis
                    path.Substring(parentAnalysisPath.Length);//folder in a drive

                try
                {
                    string destination;
                    if (!OfficeConverter.ConvertToNewOfficeDocument(filePath, out destination)) continue;
                    var fi = new FileInfo(filePath);

                    if (chkAttributs.Checked)
                    {
                        File.SetCreationTime(destination, fi.CreationTime);
                        File.SetLastAccessTime(destination, fi.LastAccessTime);
                        File.SetLastWriteTime(destination, fi.LastWriteTime);
                    }
                    var newFolder = folderBrowserDialog1.SelectedPath +
                        relativePath;
                    var di = new DirectoryInfo(newFolder);
                    di.Create();

                    fi.MoveTo(newFolder + "\\" + Path.GetFileName(filePath));
                    drtc.Add(dr);
                    newFiles.Add(destination);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Resources.Languages.Resources.Error_Conversion, filePath, ex.Message),
                        Resources.Languages.Resources.Erreur, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            OfficeConverter.DisposeOfficesApp();

            for (var a = 0; a < drtc.Count(); a++)
            {
                drtc[a].Cells["Fichier"].Value = newFiles[a];
            }
        }

        //Suppression
        private void bwSupp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _wf.label.Text = Resources.Languages.Resources.Txt_Rafraichissement_de_l_affichage___;

            RemoveSelectedRow(GetSelectedFileName());

            Display(false);

            _wf.Close();

            ControlState(true);
        }

        private void bwSupp_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            var filename = GetSelectedFileName();

            if (filename.Length == 0) return;

            if (
                MessageBox.Show(
                    Resources.Languages.Resources.Txt_vous_êtes_sur_le_point_de_supprimer_definitivement_le_s__fichier_s___Confirmez_vous_cette_opération_,
                    Resources.Languages.Resources.Txt_Confirmation, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation)
                == DialogResult.OK)
            {
                var cmpt = 0;

                foreach (var f in filename)
                {
                    cmpt++;
                    worker.ReportProgress((int)(100 * (cmpt / ((double)filename.Count()))));
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    var fi = new FileInfo(f);
                    try
                    {
                        fi.Delete();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format(Resources.Languages.Resources.Error_Supp, f, ex.Message), Resources.Languages.Resources.Erreur,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //Export
        private void bwExp_DoWork(object sender, DoWorkEventArgs e)
        {

            var gdv = getCurrentDGV();

            switch ((int)e.Argument)
            {
                case 1:
                    var ms=Classes.Exports.PDFExport.PDFFromGridView(gdv, true, _saveAs);
                    break;
                case 2:
                    var dt = ((DataView)gdv.DataSource).ToTable();//_dvDoublons

                    foreach (DataGridViewColumn c in gdv.Columns)
                    {
                        if (!c.Visible)
                            dt.Columns.Remove(c.Name);
                    }
                    //dt.Columns.Remove("Chemin");
                    //dt.Columns.Remove("isDuplicate");

                    dt.ExportToExcel(gdv == dgvDoublons, _saveAs);
                    break;
            }
        }
        /// <summary>
        /// Search active tab and return corresponding datagridview
        /// </summary>
        /// <returns></returns>
        private DataGridView getCurrentDGV()
        {
            var dgv = new DataGridView[] { dgvDoublons, dgvChemin, dgvErreurs, dgv2003, dgvQualifier };

            for (int i = 0; i < tabControl1.TabPages.Count; i++)
                if (tabControl1.TabPages[i].Visible) return dgv[i];

            return null;
        }

        private void btnSupp_Click()
        {
            deleteFile();
        }

        private void deleteFile()
        {
            if (_bwSupp.IsBusy == true) return;
            ControlState(false);

            InitWorker(bwSupp_DoWork, bwSupp_RunWorkerCompleted);

            _wf = new Waiting
            {
                label = { Text = Resources.Languages.Resources.Txt_Suppression_des_fichiers_en_cours___ },
                progressBar = { Style = ProgressBarStyle.Continuous },
                Wr = _gbw
            };

            _gbw.RunWorkerAsync();
            _wf.ShowDialog();

            Display(false);
        }

        private void btnStats_Click()
        {
            if (Analyser.Stats != null)
            {
                var frm = new StatsFrm(Analyser.Stats);

                frm.ShowDialog();
            }
        }

        private void btnGraph_Click()
        {
            if (Analyser.Config.Bdd)
            {
                if (DataTools.DataTools.DefaultFactory == null) initDataBase();

                ControlState(false);

                InitWorker(drawChart, genericBackgroundWorkerEnd);

                _wf = new Waiting
                {
                    label = { Text = Resources.Languages.Resources.Txt_Generation_Rapport },
                    progressBar = { Style = ProgressBarStyle.Marquee },
                    Wr = _gbw
                };

                _gbw.RunWorkerAsync();
                _wf.ShowDialog();
            }
        }

        private void btnHash_Click()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                Filter = Resources.Languages.Resources.Txt_Tout_Fichiers,
                FilterIndex = 1,
                Multiselect = false,
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string err;

                MessageBox.Show(Resources.Languages.Resources.Txt_Resultat + ContentHash.GetHash(openFileDialog1.FileName, out err));
            }
        }

        private void btnConfig_Click()
        {
            configForm cf = new configForm(Analyser.Config);
            var result = cf.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                Analyser.Config = cf.Config;
                Analyser.Config.Paths = null;
                Analyser.Config.Save(Config.DefaultFileName);
                ControlState(btnOpen.Enabled);//update enabled button
            }
        }

        /// <summary>
        /// Jobs edition
        /// </summary>
        private void btnJobs_Click()
        {
            var frmJob = new JobList();
            frmJob.ShowDialog(this);
        }

        #endregion Background jobs

        #region tools

        private void RemoveSelectedRow(string[] files)
        {
            if (files.Length == 0) return;
            dgvDoublons.DataSource = dgvChemin.DataSource = dgvErreurs.DataSource = null;
            //var toDelete = new List<DataRow>();
            var hash = new List<string>();
            foreach (var dr in
                files.Select(f => f.Replace("'", "''")).Select(ft => Analyser.Dset.Tables[0].Select("Fichier='" + ft + "'")))
            {
                hash.Add(dr[0]["Hash"].ToString());
                dr[0].Delete();
            }

            foreach (var dr
                in hash.Select(h => Analyser.Dset.Tables[0].Select("hash='" + h + "'")).Where(dr => dr.Count() == 1))
            {
                dr[0]["isDuplicate"] = false;
            }
        }
        /// <summary>
        /// Return array of selected files paths in the current tab
        /// </summary>
        /// <returns></returns>
        private string[] GetSelectedFileName()
        {
            DataGridView dgv = null;

            switch (_selectedTab)
            {
                case 0:
                    dgv = dgvDoublons;
                    break;

                case 1:
                    dgv = dgvChemin;
                    break;

                case 2:
                    dgv = dgvErreurs;
                    break;
                case 3:
                    dgv = dgv2003;
                    break;
                case 4:
                    dgv = dgvQualifier;
                    break;
            }

            if (dgv == null) return new string[0];
            if (dgv.SelectedRows.Count <= 0)
                return dgv.CurrentRow != null ? new[] { dgv.CurrentRow.Cells["Fichier"].Value.ToString() } : new string[0];
            var result = new string[dgv.SelectedRows.Count];
            for (var a = 0; a < dgv.SelectedRows.Count; a++)
            {
                result[a] = dgv.SelectedRows[a].Cells["Fichier"].Value.ToString();
            }
            return result;
        }


        private static long VerifParam(string title, string value, out bool valide)
        {
            long result;
            if (long.TryParse(value, out result) && result >= 0)
            {
                valide = true;
                return result;
            }
            MessageBox.Show(string.Format(Resources.Languages.Resources.Txt_Le_paramètre___0___n_a_pas_une_valeur_correcte, title), Resources.Languages.Resources.Erreur,
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            valide = false;
            return -1;
        }

        #endregion tools

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.Value == null) return;
                var dataPropertyName = ((DataGridView)sender).Columns[e.ColumnIndex].DataPropertyName;
                if (dataPropertyName == "Taille")
                {
                    e.Value = new FileSize((long)e.Value);
                    return;
                }
                if (dataPropertyName == "Status")
                {
                    e.Value = (Analyser.status)(Convert.ToInt32(e.Value));
                }
            }
            catch (Exception ex)
            {
                if (Analyser.Config.EnableLog) SimpleLog.Log(ex);
            }
        }

        private bool connectNetWorkDrive(string driveLetter, string path)
        {
            SimpleLog.Log("Création d'un lecteur réseau " + driveLetter, SimpleLog.Severity.Info);

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "Net",
                    Arguments = string.Format("use {0}: {1} /persistent:no", driveLetter, path),
                    RedirectStandardError = true,
                    StandardErrorEncoding = Console.OutputEncoding,//Encoding.UTF8,
                    UseShellExecute = false
                }
            };
            process.Start();
            process.WaitForExit();
            int result = process.ExitCode;
            string stderr = process.StandardError.ReadToEnd();

            if (result == 0) return true;

            SimpleLog.Log(Resources.Languages.Resources.Error_Network_Drive + "\n" + stderr, SimpleLog.Severity.Warning);
            return false;
        }


        private void btnUser_Click()
        {

            if (Analyser.Dset == null) initDataBase();

            var uf = new userForm();

            if (uf.ShowDialog() == DialogResult.OK)
                applyFilter(uf.checkedListBox1.CheckedItems.Cast<DataRowView>()
                .Select(li => li[1].ToString())
                .ToList());
        }

        private void applyFilter(List<string> users)
        {
            dgvDoublons.Columns["isOriginal"].Visible = true;
            if (users.Count == 0)
                dgvDoublons.DataSource = _dvDoublons;//dv.RowFilter = "isDuplicate=1";
            else
            {
                ControlState(false);

                InitWorker(getFiltredFiles, genericBackgroundWorkerEnd);

                _wf = new Waiting
                {
                    label = { Text = Resources.Languages.Resources.Txt_Chargement_des_données },
                    progressBar = { Style = ProgressBarStyle.Continuous },
                    Wr = _gbw
                };

                _gbw.RunWorkerAsync(users);
                _wf.ShowDialog();

                dgvDoublons.Columns["isOriginal"].Visible = false;
                if (dgvDoublons.Columns["Hash1"] != null)
                {
                    dgvDoublons.Columns["Hash1"].Visible = false;
                    dgvDoublons.Columns["Hash2"].Visible = false;
                }

            }

            lbNbDoublons.Text = string.Format("{0} doublon(s) détecté(s)", dgvDoublons.RowCount);
            Finalize(false);
        }

        private void getFiltredFiles(object sender, DoWorkEventArgs e)
        {
            var users = (List<string>)(e.Argument);
            dgvDoublons.Invoke((Action)(() => dgvDoublons.DataSource = DataTools.DataTools.Data(
                "select *,t2.Hash from analyse as t1 inner join (SELECT analyse.Hash FROM analyse where isduplicate = 1 and isOriginal = 0 and user in (\"" + string.Join("\",\"", users).Replace("\\", "\\\\") + "\")  group by hash) as t2 on t1.hash = t2.hash order by t1.hash, t1.created;",
                Analyser.conn)));

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Analyser.Config.Bdd && !automaticMode)
            {
                try
                {
                    initDataBase();
                    _dvDoublons = new DataView(Analyser.Dset.Tables[0], "isDuplicate = 1", "Hash asc", DataViewRowState.CurrentRows);
                    Display(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Properties.Resources.DbError, Resources.Languages.Resources.Erreur, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Analyser.Config.Bdd = false;
                }

            }
        }

        private void trackQualifier_ValueChanged(object sender, EventArgs e)
        {
            lblLevelQualifier.Text = $"({trackQualifier.Value})";
            timer.Start();
        }

        private void txtQualPath_TextChanged(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            updateQualifier();
            timer.Stop();
        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openSelectedFiles();
        }

        private void dgv_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];
                if (!c.Selected)
                {
                    c.DataGridView.ClearSelection();
                    c.DataGridView.CurrentCell = c;
                    c.Selected = true;
                }
            }
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F10 && e.Shift) || e.KeyCode == Keys.Apps)
            {
                e.SuppressKeyPress = true;
                DataGridViewCell currentCell = (sender as DataGridView).CurrentCell;
                if (currentCell != null)
                {
                    ContextMenuStrip cms = currentCell.ContextMenuStrip;
                    if (cms != null)
                    {
                        Rectangle r = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, false);
                        Point p = new Point(r.X + r.Width, r.Y + r.Height);
                        cms.Show(currentCell.DataGridView, p);
                    }
                }
            }
        }

        private void ouvrirLeDossierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openSelectFilesFolders();
        }

        private void supprimerLeFichierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteFile();
        }
    }
}