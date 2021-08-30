using duplicateFile.Properties;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using _Application = Microsoft.Office.Interop.Word._Application;
using Application = Microsoft.Office.Interop.Word.Application;

namespace duplicateFile.Classes
{
    public static class OfficeConverter
    {
        private enum OfficeApplication
        {
            Word,
            Excel,
            PowerPoint
        };

        public static bool SilentMode;
        public static bool KeepOpen;
        private static _Application _wordApp;
        private static Microsoft.Office.Interop.Excel._Application _excelApp;
        private static Microsoft.Office.Interop.PowerPoint._Application _powerpointApp;

        private static readonly Dictionary<string, OfficeDestination> DestinationDictionary = new Dictionary
            <string, OfficeDestination>
        {
            {".doc", new OfficeDestination(OfficeApplication.Word, ".docx", WdSaveFormat.wdFormatXMLDocument)},
            {".rtf", new OfficeDestination(OfficeApplication.Word, ".docx", WdSaveFormat.wdFormatXMLDocument)},
            {".dot", new OfficeDestination(OfficeApplication.Word, ".dotx", WdSaveFormat.wdFormatFlatXMLTemplate)},
            {".odt", new OfficeDestination(OfficeApplication.Word, ".docx", WdSaveFormat.wdFormatXMLDocument)},

            {".xls", new OfficeDestination(OfficeApplication.Excel, ".xlsx", XlFileFormat.xlOpenXMLWorkbook)},
            {".xlt", new OfficeDestination(OfficeApplication.Excel, ".xltx", XlFileFormat.xlOpenXMLTemplate)},
            {".ods", new OfficeDestination(OfficeApplication.Excel, ".xlsx", XlFileFormat.xlOpenXMLWorkbook)},

            {".ppt", new OfficeDestination(OfficeApplication.PowerPoint, ".pptx",PpSaveAsFileType.ppSaveAsOpenXMLPresentation)},
            {".pot", new OfficeDestination(OfficeApplication.PowerPoint, ".potx", PpSaveAsFileType.ppSaveAsOpenXMLTemplate)},
            {".odp", new OfficeDestination(OfficeApplication.PowerPoint, ".pptx",PpSaveAsFileType.ppSaveAsOpenXMLPresentation)}
        };

        public static bool ConvertToNewOfficeDocument(string fileName, out string newFileName)
        {
            string ext;
            return ConvertToNewOfficeDocument(fileName, out newFileName, out ext);
        }

        public static bool ConvertToNewOfficeDocument(string fileName, out string newFileName, out string newExt)
        {
            var ext = Path.GetExtension(fileName).ToLower();
            newExt = ext;

            newFileName = fileName;

            if (!DestinationDictionary.ContainsKey(ext)) return false;
            if (fileName.StartsWith("~*")) return false;

            var dest = DestinationDictionary[ext];
            newExt = dest.Extension;

            newFileName = ReplaceExtension(fileName, dest.Extension);

            object obj;

            var result = true;

            switch (dest.Application)
            {
                case OfficeApplication.Word:
                    if (_wordApp == null) _wordApp = new Application();
                    _Document wordDoc = null;

                    try
                    {
                        wordDoc = _wordApp.Documents.Open(fileName);
                        if (Path.GetExtension(fileName)!=".odt") wordDoc.Convert();
                        if (wordDoc.HasVBProject)
                        {
                            newFileName = ReplaceExtension(newFileName, ".docm");
                            dest = new OfficeDestination(OfficeApplication.Word, ".docm",
                                WdSaveFormat.wdFormatXMLDocumentMacroEnabled); //for files with macro
                        }
                        wordDoc.SaveAs(newFileName, dest.FileFormat);
                    }
                    catch (Exception e)
                    {
                        newFileName = fileName;
                        result = false;
                        DisplayError(fileName, e);
                    }
                    finally
                    {
                        if (wordDoc != null)
                        {
                            wordDoc.Close();
                            obj = wordDoc;
                            DisposeInteropObject(ref obj);
                        }
                        if (!KeepOpen || !result)
                        {
                            _wordApp.Quit();
                            obj = _wordApp;
                            DisposeInteropObject(ref obj);
                            _wordApp = null;
                        }
                    }
                    break;

                case OfficeApplication.Excel:
                    if (_excelApp == null) _excelApp = new Microsoft.Office.Interop.Excel.Application();
                    _Workbook excelDoc = null;
                    try
                    {
                        excelDoc = _excelApp.Workbooks.Open(fileName);
                        if (excelDoc.HasVBProject)
                        {
                            newFileName = ReplaceExtension(newFileName, ".xlsm");
                            dest = new OfficeDestination(OfficeApplication.Excel, ".xlsm",
                                XlFileFormat.xlOpenXMLWorkbookMacroEnabled); //for files with macro
                        }
                        excelDoc.SaveAs(newFileName, dest.FileFormat);
                    }
                    catch (Exception e)
                    {
                        newFileName = fileName;
                        result = false;
                        DisplayError(fileName, e);
                    }
                    finally
                    {
                        if (excelDoc != null)
                        {
                            excelDoc.Close();
                            obj = excelDoc;
                            DisposeInteropObject(ref obj);
                        }
                        if (!KeepOpen || !result)
                        {
                            _excelApp.Quit();
                            obj = _excelApp;
                            DisposeInteropObject(ref obj);
                            _excelApp = null;
                        }
                    }
                    break;

                case OfficeApplication.PowerPoint:
                    if (_powerpointApp == null) _powerpointApp = new Microsoft.Office.Interop.PowerPoint.Application();
                    _Presentation powerpointDoc = null;
                    try
                    {
                        powerpointDoc = _powerpointApp.Presentations.Open(fileName, MsoTriState.msoTrue,
                            MsoTriState.msoTrue, MsoTriState.msoFalse);
                        if (powerpointDoc.HasVBProject)
                        {
                            newFileName = ReplaceExtension(newFileName, ".pptm");
                            dest = new OfficeDestination(OfficeApplication.PowerPoint, ".pptm",
                                PpSaveAsFileType.ppSaveAsOpenXMLPresentationMacroEnabled); //for files with macro
                        }
                        powerpointDoc.SaveAs(newFileName, (PpSaveAsFileType)dest.FileFormat);
                    }
                    catch (Exception e)
                    {
                        newFileName = fileName;
                        result = false;
                        DisplayError(fileName, e);
                    }
                    finally
                    {
                        if (powerpointDoc != null)
                        {
                            powerpointDoc.Close();
                            obj = powerpointDoc;
                            DisposeInteropObject(ref obj);
                        }
                        if (!KeepOpen || !result)
                        {
                            _powerpointApp.Quit();
                            obj = _powerpointApp;
                            DisposeInteropObject(ref obj);
                            _powerpointApp = null;
                        }
                    }
                    break;
            }

            GarbageCollector();
            return result;
        }

        private static void DisplayError(string fileName, Exception e)
        {
            if (SilentMode)
            {
                throw e;
            }
            MessageBox.Show(Resources.Languages.Resources.Txt_Le_fichier__ + fileName + Resources.Languages.Resources.Error_n_a_pu_etre_converti + e.Message,
                Resources.Languages.Resources.Erreur, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        ///     dispose interop object
        /// </summary>
        /// <param name="o">object to dispose</param>
        private static void DisposeInteropObject(ref object o)
        {
            if (o != null)
                Marshal.ReleaseComObject(o);
            o = null;
        }

        public static void DisposeOfficesApp()
        {
            try
            {
                object obj;
                if (_wordApp != null)
                {
                    _wordApp.Quit();
                    obj = _wordApp;
                    DisposeInteropObject(ref obj);
                }

                if (_excelApp != null)
                {
                    _excelApp.Quit();
                    obj = _excelApp;
                    DisposeInteropObject(ref obj);
                }

                if (_powerpointApp != null)
                {
                    _powerpointApp.Quit();
                    obj = _powerpointApp;
                    DisposeInteropObject(ref obj);
                }

                GarbageCollector();
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        private static void GarbageCollector()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            // GC needs to be called twice in order to get the Finalizers called
            // - the first time in, it simply makes a list of what is to be
            // finalized, the second time in, it actually is finalizing. Only
            // then will the object do its automatic ReleaseComObject.
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private static string ReplaceExtension(string filename, string newExt)
        {
            return String.Format("{0}\\{1}{2}", Path.GetDirectoryName(filename),
                Path.GetFileNameWithoutExtension(filename), newExt);
        }

        private class OfficeDestination
        {
            public readonly OfficeApplication Application;
            public readonly string Extension;
            public readonly object FileFormat;

            public OfficeDestination(OfficeApplication application, string extension, object fileFormat)
            {
                Application = application;
                Extension = extension;
                FileFormat = fileFormat;
            }
        }
    }
}