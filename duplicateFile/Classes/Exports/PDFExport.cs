using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace duplicateFile.Classes.Exports
{
    

    public static class PDFExport
    {
        public static Dictionary<string, double> colDivideur = new Dictionary<string, double> { { "Taille", 2.1 }, { "Status", 2.1 }, { "Hash", 0.6 }, { "Original", 2.1 }, { "Fichier", 0.5 } };

        public static MemoryStream PDFFromGridView(System.Windows.Forms.DataGridView grd,bool useDefaultCellStyle, string path=null)
        {
            var document = new Document();
            var section = document.AddSection();

            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.Orientation = Orientation.Landscape;
            section.PageSetup.LeftMargin = new Unit(1, UnitType.Centimeter);
            section.PageSetup.RightMargin = new Unit(1, UnitType.Centimeter);
            
            PDFTableHelper.setStyle(document);

            var table = PDFTableHelper.createTable(section,
                                                    getHeaders(grd),
                                                    StyleNames.Header, colDivideur);        

            table.Borders.Width = 0.75;

            foreach (System.Windows.Forms.DataGridViewRow dr in grd.Rows)
            {

                var currentRow = PDFTableHelper.addRowFromGridViewRow(table, dr, StyleNames.Normal);

                currentRow.Shading.Color = useDefaultCellStyle ?
                    new Color(dr.DefaultCellStyle.BackColor.R, dr.DefaultCellStyle.BackColor.G, dr.DefaultCellStyle.BackColor.B) :
                    new Color(255,255,255,255);
                
            }

            document.LastSection.Add(table);

            var renderer = new MigraDoc.Rendering.PdfDocumentRenderer(true);
            renderer.Document = document;
            renderer.RenderDocument();

            if (path != null) { renderer.PdfDocument.Save(path); return null; }

            MemoryStream ms = new MemoryStream();
            renderer.PdfDocument.Save(ms,false);

            return ms;
        }

        private static string[] getHeaders(System.Windows.Forms.DataGridView grd)
        {
            var result = new List<string>();

            for (int i = 0; i < grd.Columns.Count; i++)
                if (grd.Columns[i].Visible) result.Add(grd.Columns[i].HeaderText);

            return result.ToArray();
        }

    }
}
