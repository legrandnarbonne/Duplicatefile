using System;
using System.ComponentModel;
using System.Linq;
using System.Data;

using duplicateFile.Classes.Tools;

namespace duplicateFile.Classes
{
    public class Stats
    {
        private int numberOfFile = 0;
        private int numberOfDir = 0;
        private int numberOfDuplicateFile = 0;
        private DateTime startDate;
        private DateTime endDate; 
        private TimeSpan qualifierDuration;
        private TimeSpan dataBaseAccessDuration;
        private FileSize totalFileSize = new FileSize();

        private int deletedFiles;
        private int newFiles;
        private int calculatedHash = 0;
        private string[] paths;
        private Guid jobId;


        private FileSize duplicateFileSize = new FileSize();

        #region Constructors
        public Stats()
        {
            startDate = DateTime.Now;
            qualifierDuration = new TimeSpan();
            dataBaseAccessDuration = new TimeSpan();
        }
        #endregion

        #region Properties

        public Guid JobId
        {
            get { return jobId; }
            set { jobId = value; }
        }

        [Description("Durée de l'analyse")]
        public Double Duration
        {
            get { return (endDate - startDate).TotalSeconds; }
        }

        [Description("Durée totale pour la qualification du fichier (RGPD)")]
        public Double QualifierDuration
        {
            get { return qualifierDuration.TotalSeconds; }
        }

        [Description("Durée totale pour la qualification du fichier (RGPD)")]
        public Double DataBaseAccessDuration
        {
            get { return dataBaseAccessDuration.TotalSeconds; }
        }

        [Description("Taux d'analyse (fichiers/seconde)")]
        public double Rate
        {
            get { return (double)numberOfFile / (DateTime.Now - startDate).TotalSeconds; }
        }

        //public string[] Paths
        //{
        //    get { return paths; }
        //    set { paths = value; }
        //}

        [Description("Date de début")]
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        [Description("Date de fin")]
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        [Description("Checksum calculés")]
        public int CalculatedHash
        {
            get { return calculatedHash; }
            set { calculatedHash = value; }
        }

        [Description("Fichiers effacés")]
        public int DeletedFiles
        {
            get { return deletedFiles; }
            set { deletedFiles = value; }
        }

        [Description("Nouveaux fichiers")]
        public int NewFiles
        {
            get { return newFiles; }
            set { newFiles = value; }
        }

        [Description("Fichiers dupliqués")]
        public int NumberOfDuplicateFile
        {
            get { return numberOfDuplicateFile; }
            set { numberOfDuplicateFile = value; }
        }

        [Description("Taille des fichiers dupliqués")]
        public FileSize DuplicateFileSize
        {
            get { return duplicateFileSize; }
            set { duplicateFileSize = value; }
        }

        [Description("Répertoires analysés")]
        public int NumberOfDir
        {
            get { return numberOfDir; }
            set { numberOfDir = value; }
        }

        [Description("Fichiers analysés")]
        public int NumberOfFile
        {
            get { return numberOfFile; }
            set { numberOfFile = value; }
        }
        [Description("Taille totale des fichiers")]
        public FileSize TotalFileSize
        {
            get
            {
                return totalFileSize;
            }

            set
            {
                totalFileSize = value;
            }
        }

        #endregion

        #region Method
        public string ToString()
        {
            string result = "Statistiques\r\n\r\n";

            foreach (var pair in GetNamesAndValues())
            {
                result += ResourceHelper.getResource(pair.Name) + ":" + pair.Value + "\r\n";
            }

            return result;
        }

        public void ToDataRow(DataRow dr)
        {
            foreach (var pair in GetNamesAndValues())
            {
                var col = dr.Table.Columns[pair.Name];
                if (col == null)
                {
                    SimpleLogger.SimpleLog.Log("Warning column " + pair.Name + " un avaible!");
                    return;
                }
                //Type cType=col.DataType;
                dr[pair.Name] = pair.Value;
            }

        }
        /// <summary>
        /// return names/values pair from object
        /// </summary>
        /// <param name="getPropertyName">if set to true names contain property name. If set to false names contain description</param>
        /// <returns></returns>
        public dynamic GetNamesAndValues(bool getPropertyName=true)
        {
            return this.GetType()
             .GetProperties()
             .Where(pi => pi.GetGetMethod() != null)
             .Select(pi => new
             {
                 Name = getPropertyName ?
                    pi.Name :
                    ((DescriptionAttribute)pi.GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description,
                 Value = pi.GetGetMethod().Invoke(this, null)
             });
        }

        public void AddQualifierDuration(TimeSpan duration)
        {
            qualifierDuration=qualifierDuration.Add(duration);
        }

        public void AddDataBaseAccessDuration(TimeSpan duration)
        {
            dataBaseAccessDuration = dataBaseAccessDuration.Add(duration);
        }
        #endregion
    }
}