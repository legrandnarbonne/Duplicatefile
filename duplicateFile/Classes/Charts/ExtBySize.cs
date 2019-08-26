using System.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace duplicateFile.Classes.Charts
{
    public class ExtBySize : IStatChart
    {
        public string Title
        {
            get { return "Répartition des extensions par volume"; }
        }

        public bool isYFileSize
        {
            get { return true; }
        }

        public bool isYStatus
        {
            get { return false; }
        }

        public bool concatXvalue
        {
            get { return false; }
        }

        public string Sql
        {
            get
            {
                return
                    "select sum(taille) as Volume,SUBSTRING_INDEX(fichier,'.',-1) as Extension from duplicatefile.analyse where (Length(fichier)-Locate (\".\",fichier))<5 group by Extension order by volume desc limit 30";
            }
        }

        public Chart draw(DataView view)
        {
            Chart chart = new Chart();

            chart.Width = 900;
            chart.Height = 600;

            chart.ChartAreas.Add("Area");
            chart.ChartAreas["Area"].AxisX.LabelStyle.Interval = 1;
            chart.ChartAreas["Area"].Area3DStyle.Enable3D = true;

            chart.Series.Add("Default");
            chart.Series["Default"].ChartType = SeriesChartType.Pie;
            chart.Series["Default"]["PieLabelStyle"] = "Outside";
            chart.Series["Default"].Label = "#VALX #VALY (#PERCENT{P0})";

            chart.Series["Default"].Points.DataBindXY(view, "Extension", view, "Volume");

            return chart;
        }
    }
}