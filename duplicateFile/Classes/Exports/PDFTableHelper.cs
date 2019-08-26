using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;

namespace duplicateFile.Classes.Exports
{
    public static class PDFTableHelper
    {
        /// <summary>
        /// Create migradoc Table from column title array
        /// </summary>
        /// <param name="section">Migradoc secton</param>
        /// <param name="fields">Array of title</param>
        /// <param name="headerStyle">Style</param>
        /// <returns></returns>
        public static Table createTable(Section section, string[] fields, string headerStyle,Dictionary<string, double> colDivideur)
        {

            
            var table = new Table();

            if (fields.Length == 0) return table;

            var columnWidth = getSectionWith(section).Centimeter / (double)(fields.Length);

            addColumn(table, fields, columnWidth,colDivideur);

            var currentRow = addRowFromArray(table, fields, headerStyle);

            currentRow.HeadingFormat = true;
            currentRow.Shading.Color = new MigraDoc.DocumentObjectModel.Color(93, 123, 157);

            return table;
        }
        /// <summary>
        /// Add row from field array
        /// </summary>
        /// <param name="table">Migradoc Table</param>
        /// <param name="fields">field values array</param>
        /// <param name="style">style of row</param>
        /// <returns></returns>
        private static Row addRowFromArray(Table table, object[] fields, string style)
        {
            Paragraph p = null;
            Row currentRow = table.AddRow();

            for (int i = 0; i < fields.Length; i++)
            {
                p = currentRow.Cells[i].AddParagraph(fields[i].ToString());
                p.Style = style;
            }

            return currentRow;
        }

                /// <summary>
        /// Add row from field array
        /// </summary>
        /// <param name="table">Migradoc Table</param>
        /// <param name="fields">field values array</param>
        /// <param name="style">style of row</param>
        /// <returns></returns>
        public static Row addRowFromGridViewRow(Table table, System.Windows.Forms.DataGridViewRow gvr, string style)
        {
            Paragraph p = null;
            Row currentRow = table.AddRow();
            int pdfcol = 0;

            for (int i = 0; i < gvr.Cells.Count; i++)
            {
                if (gvr.Cells[i].Visible)
                {
                    p = currentRow.Cells[pdfcol].AddParagraph(gvr.Cells[i].EditedFormattedValue.ToString());
                    p.Style = style;
                    pdfcol++;
                }
            }

            return currentRow;
        }

        /// <summary>
        /// Set document default styles
        /// </summary>
        /// <param name="document">Migradoc Document</param>
        public static void setStyle(Document document)
        {
            var style = document.Styles[StyleNames.Header];
            style.Font.Name = "Thaoma";
            style.Font.Color = Colors.White;
            style.Font.Size = 9;
            style.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            style = document.Styles[StyleNames.Normal];
            style.Font.Name = "Thaoma";
            style.Font.Size = 7;
            style.ParagraphFormat.Alignment = ParagraphAlignment.Center;

        }

        /// <summary>
        /// Find section useful with
        /// </summary>
        /// <param name="section">Migradoc section to set</param>
        /// <returns></returns>
        public static Unit getSectionWith(Section section)
        {
            Unit width;
            Unit height;

            PageSetup.GetPageSize(section.PageSetup.PageFormat,
                out width,
                out height);

            if (section.PageSetup.Orientation == Orientation.Landscape)
            {
                var tmp = width;
                width = height;
                height = tmp;
            }

            return width - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;
        }

        /// <summary>
        /// Add column to migradoc table with specified width
        /// </summary>
        /// <param name="table">Migrdoc table</param>
        /// <param name="count">Number of row</param>
        /// <param name="columnWith">Column width</param>
        public static void addColumn(Table table, string[] titles, double columnWidth, Dictionary<string, double> colDivideur)
        {
            for (int c = 0; titles.Length > c; c++)
            {
                var colWidth = columnWidth;
                if (colDivideur.ContainsKey(titles[c])) colWidth = colWidth / colDivideur[titles[c]];

                Column column = table.AddColumn(Unit.FromCentimeter(colWidth));
            }
        }

    }
}
