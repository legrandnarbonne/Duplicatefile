using Microsoft.Office.Interop.Excel;
using System;
using System.Drawing;
using System.IO;
using DataTable = System.Data.DataTable;

//using System.Threading.Tasks;

namespace duplicateFile.Classes
{
    public static class DataTableExtensions// thanks to Tomasz Wiśniewski
    {
        /// <summary>
        /// Export DataTable to Excel file
        /// </summary>
        /// <param name="dataTable">Source DataTable</param>
        /// <param name="excelFilePath">Path to result file name</param>
        public static void ExportToExcel(this DataTable dataTable,bool colorize=false, string excelFilePath = null)
        {
            try
            {
                int columnsCount;

                if (dataTable == null || (columnsCount = dataTable.Columns.Count) == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                Application excel = new Application();
                excel.Workbooks.Add();

                // single worksheet
                _Worksheet worksheet = (_Worksheet)excel.ActiveSheet;

                object[] header = new object[columnsCount];

                // column headings
                for (int i = 0; i < columnsCount; i++)
                    header[i] = dataTable.Columns[i].ColumnName;

                Range headerRange = worksheet.Range[(Range)(worksheet.Cells[1, 1]), (Range)(worksheet.Cells[1, columnsCount])];
                headerRange.Value = header;
                headerRange.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                headerRange.Font.Bold = true;

                // DataCells
                int rowsCount = dataTable.Rows.Count;
                object[,] cells = new object[rowsCount, columnsCount];

                for (int j = 0; j < rowsCount; j++)
                    for (int i = 0; i < columnsCount; i++)
                        cells[j, i] = dataTable.Rows[j][i];

                worksheet.Range[(Range)(worksheet.Cells[2, 1]), (Range)(worksheet.Cells[rowsCount + 1, columnsCount])].Value = cells;

                ((Range)worksheet.Columns[2]).Insert();
                ((Range)worksheet.Cells[1, 2]).Value = "Dossier";

                string memHash =colorize? dataTable.Rows[1]["Hash"].ToString():null;
                Color[] colTab = new Color[] { Color.FromArgb(202, 244, 181), Color.FromArgb(255, 212, 157) };
                int colorInUse = 0;

                for (int j = 0; j < rowsCount; j++)
                {
                    worksheet.Hyperlinks.Add(worksheet.Cells[j + 2, 2], "file://" + Path.GetDirectoryName(((Range)worksheet.Cells[j + 2, 1]).Value.ToString()), Type.Missing, "Ouvrir le dossier", "Ouvrir le dossier");
                    worksheet.Hyperlinks.Add(worksheet.Cells[j + 2, 1], "file://" + ((Range)worksheet.Cells[j + 2, 1]).Value, Type.Missing, ((Range)worksheet.Cells[j + 2, 1]).Value, ((Range)worksheet.Cells[j + 2, 1]).Value);

                    if (colorize&&memHash != dataTable.Rows[j]["Hash"].ToString())
                    {
                        colorInUse = colorInUse == 0 ? 1 : 0;
                        memHash = dataTable.Rows[j]["Hash"].ToString();
                    }

                    worksheet.Range[(Range)(worksheet.Cells[j + 2, 1]), (Range)(worksheet.Cells[j + 2, columnsCount])].Interior.Color = ColorTranslator.ToOle(colTab[colorInUse]);
                }

                worksheet.Range[(Range)(worksheet.Cells[2, 1]), (Range)(worksheet.Cells[rowsCount + 1, columnsCount])].Columns.AutoFit();
                // check fielpath
                if (!string.IsNullOrEmpty(excelFilePath))
                {
                    try
                    {
                        worksheet.SaveAs(excelFilePath);
                        excel.Quit();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                            + ex.Message);
                    }
                }
                else    // no filepath is given
                {
                    excel.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }
    }
}