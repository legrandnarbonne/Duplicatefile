
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace duplicateFile.Classes.Qualifier
{
    public class Qualifier : IQualifier
    {
        public static string[] AllowedExtension = new string[] { ".txt", ".csv", ".docx", ".dotx", ".xlsx","xlsm" };
        
        public virtual string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual string ColumnName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public virtual int Note(string txt,FileInfo fi)
        {
            throw new NotImplementedException();
        }
    }

    public static class OfficeContentExtractor
    {
        public static readonly Dictionary<string, string> OfficeContentPath = new Dictionary<string, string>
            {
                {".dotx", "word/document.xml"},
                {".docx", "word/document.xml"},
                {".xlsx", "xl/worksheets/*,xl/sharedStrings.xml"}
            };

        public static string GetContent(FileInfo fi)
        {
            if (!OfficeContentPath.ContainsKey(fi.Extension)) return null;

            if (fi.Name.StartsWith("~",StringComparison.InvariantCultureIgnoreCase)||
                fi.Name.StartsWith("$",StringComparison.InvariantCultureIgnoreCase)) return null;//avoid temp file or in recycle bin 

            var result = new StringBuilder();

            var targets = OfficeContentPath[fi.Extension].Split(',');

            ZipFile zipFile;

            using (zipFile = ZipFile.Read(fi.FullName))
            {
                foreach (string target in targets)
                {
                    var filesList = new List<ZipEntry>();
                    if (target.EndsWith("*",StringComparison.InvariantCultureIgnoreCase))
                    {
                        filesList = zipFile.SelectEntries("*", target.Substring(0, target.Length - 1)).ToList();
                    }
                    else
                        filesList = zipFile.SelectEntries(target).ToList();

                    foreach (ZipEntry zEntry in filesList)
                    {
                        MemoryStream tempS = new MemoryStream();
                        zEntry.Extract(tempS);
                        result .Append(Encoding.UTF8.GetString(tempS.ToArray()));
                        tempS.Close();
                    }
                }

                return SearchTools.XmlToText(result.ToString());
            }
        }
    }
}
