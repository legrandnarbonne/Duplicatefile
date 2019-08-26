using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace duplicateFile.Classes
{
    [Serializable]
    public class Config : ICloneable
    {

        private string passphrase = "emfklvkzdc12";
        private string passwordStore;
        private string taskPasswordStore;

        #region properties

        private bool duplicate = true;
        private bool useExtension = true;
        private bool bdd = false;
        private bool sharepoint = false;
        private int minFileSize;
        private int maxSizeDifference = 200;
        private bool ignoreSpFolder = true;
        private bool ignoreAspxFile = true;
        private string server;
        private string user;
        private string password;
        private string taskUser;
        private string taskPassword;
        private string database;
        private bool enableLog = true;
        private string serverSMTP;
        private string reportMail;
        private bool mail = false;
        private string from;
        private bool oneMail = true;
        private bool attStats = true;
        private bool attError = false;
        private bool attDup = true;
        private string[] paths;
        private string sharePointURL;
        private string sharePointDisk;
        private bool followRootLnkFileTarget;
        private int verboseLevel = 1;

        public int VerboseLevel
        {
            get { return verboseLevel; }
            set { verboseLevel = value; }
        }

        public string SharePointDisk
        {
            get { return sharePointDisk; }
            set { sharePointDisk = value; }
        }

        public string SharePointURL
        {
            get { return sharePointURL; }
            set { sharePointURL = value; }
        }

        public bool FollowRootLnkFileTarget
        {
            get { return followRootLnkFileTarget; }
            set { followRootLnkFileTarget = value; }
        }

        /// <summary>
        /// Path to analyse
        /// </summary>
        public string[] Paths
        {
            get { return paths; }
            set { paths = value; }
        }

        public string mailFrom
        {
            get { return from; }
            set { from = value; }
        }

        public bool Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        public string ReportMail
        {
            get { return reportMail; }
            set { reportMail = value; }
        }

        public string ServerSMTP
        {
            get { return serverSMTP; }
            set { serverSMTP = value; }
        }

        public bool EnableLog
        {
            get { return enableLog; }
            set { enableLog = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string User
        {
            get { return user; }
            set { user = value; }
        }

        public string TaskPassword
        {
            get { return taskPassword; }
            set { taskPassword = value; }
        }

        public string TaskUser
        {
            get { return taskUser; }
            set { taskUser = value; }
        }

        public string Server
        {
            get { return server; }
            set { server = value; }
        }

        public string Database
        {
            get { return database; }
            set { database = value; }
        }

        public bool IgnoreAspxFile
        {
            get { return ignoreAspxFile; }
            set { ignoreAspxFile = value; }
        }

        public bool SpFolderOnly
        {
            get { return ignoreSpFolder; }
            set { ignoreSpFolder = value; }
        }

        public int MaxSizeDifference
        {
            get { return maxSizeDifference; }
            set { maxSizeDifference = value; }
        }

        public bool UseExtension
        {
            get { return useExtension; }
            set { useExtension = value; }
        }

        public int MinFileSize
        {
            get { return minFileSize; }
            set { minFileSize = value; }
        }

        public int QualifierMini
        {
            get;
            set;
        }

        public bool Sharepoint
        {
            get { return sharepoint; }
            set { sharepoint = value; }
        }

        public bool Bdd
        {
            get { return bdd; }
            set
            {
                bdd = value;
                //Analyser.InitDataSet();
            }
        }

        public bool Duplicate
        {
            get { return duplicate; }
            set { duplicate = value; }
        }
        public List<String> Qualifiers
        {
            get;
            set;
        }

        public bool OneMail
        {
            get
            {
                return oneMail;
            }

            set
            {
                oneMail = value;
            }
        }

        public bool AttStats
        {
            get
            {
                return attStats;
            }

            set
            {
                attStats = value;
            }
        }

        public bool AttDup
        {
            get
            {
                return attDup;
            }

            set
            {
                attDup = value;
            }
        }

        public bool AttError
        {
            get
            {
                return attError;
            }

            set
            {
                attError = value;
            }
        }

        #endregion properties

        public string ConnectionString
        {
            get
            {
                return String.Format("Server={0};Database={1};Uid={2};Pwd={3};default command timeout=180", server, database, user, password);//Connection Timeout=180
            }
        }

        /// <summary>
        /// Return application path
        /// </summary>
        public static string DefaultPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        /// <summary>
        /// Return jobs path
        /// </summary>
        public static string DefaultJobPath
        {
            get { return DefaultPath + "\\Jobs\\"; }
        }

        /// <summary>
        /// Return default config file path
        /// </summary>
        public static string DefaultFileName
        {
            get { return DefaultPath + "\\default.conf"; }
        }



        [OnSerializing()]
        private void OnSerializingMethod(StreamingContext context)
        {
            passwordStore = Password;
            taskPasswordStore = TaskPassword;

            if (!string.IsNullOrEmpty(Password)) Password = StringEncryption.Encrypt.EncryptString(Password, passphrase);
            if (!string.IsNullOrEmpty(TaskPassword)) TaskPassword = StringEncryption.Encrypt.EncryptString(TaskPassword, passphrase);
        }

        [OnSerialized()]
        private void OnSerializedMethod(StreamingContext context)
        {
            Password = passwordStore;
            TaskPassword = taskPasswordStore;
        }

        [OnDeserialized()]
        private void OnDeserializedMethod(StreamingContext context)
        {
            if (!string.IsNullOrEmpty(Password)) Password = StringEncryption.Encrypt.DecryptString(Password, passphrase);
            if (!string.IsNullOrEmpty(TaskPassword)) TaskPassword = StringEncryption.Encrypt.EncryptString(TaskPassword, passphrase);
        }

        #region method

        /// <summary>
        /// Saves to an xml file
        /// </summary>
        /// <param name="FileName">File path of the new xml file</param>
        public void Save(string FileName)
        {
            using (var writer = new System.IO.StreamWriter(FileName))
            {
                var serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(writer, this);
                writer.Flush();
            }
        }

        /// <summary>
        /// Load an object from an xml file
        /// </summary>
        /// <param name="FileName">Xml file name</param>
        /// <returns>The object created from the xml file</returns>
        public static Config Load(string FileName = null)
        {


            var fileName = FileName == null ?
                DefaultFileName ://no filename use default config
                FileName;

            var fi = new FileInfo(fileName);

            if (!fi.Exists && FileName == null) return new Config();

            using (var stream = System.IO.File.OpenRead(fileName))
            {
                var serializer = new XmlSerializer(typeof(Config));
                return serializer.Deserialize(stream) as Config;
            }

        }

        public virtual object Clone()
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(ms, this);

            ms.Position = 0;
            object obj = bf.Deserialize(ms);
            ms.Close();

            return obj as Config;
        }

        #endregion method
    }
}