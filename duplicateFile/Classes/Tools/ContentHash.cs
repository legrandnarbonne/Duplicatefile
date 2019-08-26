using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

//using System.Threading.Tasks;

namespace duplicateFile.Classes
{
    public static class ContentHash
    {
        public static bool isOfficeFile(string fileName)
        {
            string ext = "";
            return isOfficeFile(fileName, out ext);
        }

        public static bool isOfficeFile(string fileName, out string ext)
        {
            ext = "";
            var extension = Path.GetExtension(fileName);
            if (extension == null) return false;
            ext = extension.Trim('.').ToLower();

            return XmlOfficeHash.ExtFiles.ContainsKey(ext);
        }

        /// <summary>
        ///     get hash form file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>string containing hash. Return -1 on error, message in lastError string</returns>
        public static string GetHash(string fileName, out string lastError)
        {
            lastError = null;
            string ext;
            var xmlOfficeFile = isOfficeFile(fileName, out ext);

            if (!xmlOfficeFile) ;

            return xmlOfficeFile ? XmlOfficeHash.GetHash(fileName, XmlOfficeHash.ExtFiles[ext], out lastError) : DefaultHash.getHash(fileName, out lastError);
        }

        private static class OfficeHash
        {
            public static readonly Dictionary<string, string> ExtFiles = new Dictionary<string, string>
            {
                {"dot", "word"},
                {"doc", "word"},
                {"docm", "word"},
                {"xls", "excel"},
                {"xlsm", "excel"},
                {"ppt", "powerpoint"},
                {"pptm", "powerpoint"},
                {"msg", "powerpoint"}
            };

            /// <summary>
            ///     For old office file hash is filename
            /// </summary>
            /// <param name="fileName"></param>
            /// <param name="folderName"></param>
            /// <returns></returns>
            public static string GetHash(string fileName, string folderName, out string lastError)
            {
                return DefaultHash.getHash(fileName, out lastError);
            }
        }

        private static class XmlOfficeHash
        {
            public static readonly Dictionary<string, string> ExtFiles = new Dictionary<string, string>
            {
                {"dotx", "word"},
                {"docx", "word"},
                {"xlsx", "xl/worksheets;xl"},
                {"pptx", "ppt/slides;ppt"}
            };

            public static string GetHash(string fileName, string foldersName, out string lastError)
            {
                return GetCrc(fileName, foldersName, out lastError);
            }

            /// <summary>
            ///     Load docx from file
            /// </summary>
            /// <param name="zipFilename">Docx file name</param>
            /// <param name="filename">path to Docx content</param>
            /// <returns>xml content definition</returns>
            private static string GetCrc(string zipFilename, string foldersName, out string lastError)
            {
                int result;
                lastError = "";

                try
                {
                    ZipFile zipFile;
                    using (zipFile = ZipFile.Read(zipFilename))
                    {
                        var folders = foldersName.Split(';');
                        var filesList = (
                            from folder
                                in folders
                            from file
                                in zipFile.SelectEntries("*", folder + "\\")
                            select new ZipFileProperties(file.Crc, file.UncompressedSize)).ToList();

                        //order by size & combine crc
                        filesList.Sort();

                        result = filesList[0].Crc;
                        for (var j = 1; j < filesList.Count; j++)
                        {
                            result ^= filesList[j].Crc;
                        }
                    }
                }
                catch (Exception e)
                {
                    result = -1;
                    lastError = e.Message;
                }

                return result.ToString();
            }
        }

        private static class DefaultHash
        {
            public static string getHash(string fileName, out string lastError)
            {
                lastError = "";
                try
                {
                    using (var stream = File.OpenRead(fileName))
                    {
                        using (var bufferedStream = new BufferedStream(stream, 1024 * 32))
                        {
                            var sha = new MD5CryptoServiceProvider();
                            var checksum = sha.ComputeHash(bufferedStream);
                            return BitConverter.ToString(checksum).Replace("-", String.Empty);
                        }
                    }
                }
                catch (Exception e)
                {
                    lastError = e.Message;
                    return "-1";
                }
            }
        }

        private class ZipFileProperties : IComparable
        {
            public readonly int Crc;
            private readonly long _fileSize;

            public ZipFileProperties(int crc, long size)
            {
                Crc = crc;
                _fileSize = size;
            }

            public int CompareTo(object obj)
            {
                var properties = obj as ZipFileProperties;
                if (properties != null)
                {
                    return _fileSize.CompareTo(properties._fileSize);
                }
                throw new ArgumentException("Object is not a fileSize");
            }
        }
    }
}