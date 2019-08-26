using System.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace duplicateFile.Classes.Charts
{
    public class SizeByPath : IStatChart
    {
        public string Title
        {
            get { return Resources.Languages.Resources.GR_Size_By_Path; }
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
                    "select substring_index(fichier,'\\\\',3) as grp,sum(taille) as volume from analyse where fichier like 'O:\\\\\\\\%' group by grp order by volume desc limit 20";
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
            chart.Series["Default"].Label = "#VALX grp :#VALY (#PERCENT{P0})";

            chart.Series["Default"].Points.DataBindXY(view, "grp", view, "volume");

            return chart;
        }
    }
}