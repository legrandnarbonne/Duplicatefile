//using MySql.Data.MySqlClient;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms.DataVisualization.Charting;

namespace duplicateFile.Classes.Charts
{
    public static class ChartTools
    {
        /// <summary>
        /// Generate statistic report
        /// return pdf path of statistic report
        /// </summary>
        /// <param name="start">open pdf</param>
        /// <returns></returns>
        public static string draw(bool start = true)
        {
            var chartNames = new List<String> { "NbrDupFileByTime", "SizeDupFileByTime", "NewDuplicateSizeByUser", "NbrFileByTime", "ExtByType", "ExtBySize", "NbrByModAge", "NbrByAccAge", "ByStatus", "DuplicateNbrByUser", "DuplicateSizeByUser", "DuplicateSizeByExt","SizeByPath" };
            //var chartNames = new List<String> { "SizeByPath" };

            var document = new PdfDocument();

            foreach (string chartName in chartNames)
            {
                IStatChart ch = Activator.CreateInstance(Type.GetType("duplicateFile.Classes.Charts." + chartName)) as IStatChart; //new ByType();

                var View = getData(ch.Sql);

                XFont titleFont = new XFont("Verdana", 20, XFontStyle.Bold);

                var page = document.AddPage();
                page.Orientation = PdfSharp.PageOrientation.Landscape;
                var gfx = XGraphics.FromPdfPage(page);

                //add title
                gfx.DrawString(ch.Title, titleFont, XBrushes.Black,
                new XRect(0, 0, page.Width, 50), XStringFormat.Center);

                //add chart
                Chart c = ch.draw(View);

                ChartTools.setPoint(c, ch);

                addPage(page, c, gfx);
            }

            var tempPath = Path.ChangeExtension(Path.GetTempFileName(), "pdf");

            document.Save(tempPath);//.Save(ms);

            if (start) System.Diagnostics.Process.Start(tempPath);

            return tempPath;
        }

        private static void addPage(PdfPage page, Chart c, XGraphics gfx)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                c.SaveImage(stream, ChartImageFormat.Png);

                using (System.Drawing.Image img = System.Drawing.Image.FromStream(stream))
                {
                    using (var xi = XImage.FromGdiPlusImage(img))
                    {
                        gfx.DrawImage(xi, 5, 50, 0.9 * (int)c.Width, 0.9 * (int)c.Height);
                    }
                }
            }
        }

        private static DataView getData(string sql)
        {
            var adapter = DataTools.DataTools.DefaultFactory.CreateDataAdapter();//.getAdapter(sql, Analyser.conn);
            adapter.SelectCommand = Analyser.conn.CreateCommand();
            adapter.SelectCommand.CommandText = sql;
            adapter.SelectCommand.CommandType = CommandType.Text;

            if (Analyser.Dset.Tables["Stats"] != null) Analyser.Dset.Tables.Remove("Stats");//.Tables["Stats"].Clear();
            adapter.Fill(Analyser.Dset, "Stats");
            return new DataView(Analyser.Dset.Tables["Stats"]);
        }

        public static void setPoint(Chart chart, IStatChart ch)
        {
            chart.ApplyPaletteColors();
            foreach (var series in chart.Series)
            {
                foreach (var point in series.Points)
                {
                    point.Color = System.Drawing.Color.FromArgb(180, point.Color);
                    if (ch.isYFileSize)
                        point.Label = point.Label.Replace("#VALY", new FileSize((long)point.YValues[0]));
                    if (ch.isYStatus)
                        point.Label = point.Label.Replace("#VALY", ((Analyser.status)point.YValues[0]).ToString());
                }
            }
        }
    }
}