using System.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace duplicateFile.Classes.Charts
{
    public class DuplicateNbrByUser : IStatChart
    {
        public string Title
        {
            get { return "Nombre de doublons par utilisateur"; }
        }

        public bool isYFileSize
        {
            get { return false; }
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
                    "select count(id) as Nombre,title as Utilisateur from duplicatefile.analyse join user on user.login=analyse.user where isduplicate=1 and isoriginal=0 group by user order by Nombre desc limit 30";
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
            chart.ChartAreas["Area"].BackColor = System.Drawing.Color.White;

            chart.Series.Add("Default");
            chart.Series["Default"].ChartType = SeriesChartType.Bar;
            chart.Series["Default"]["PieLabelStyle"] = "Outside";
            chart.Series["Default"].Label = "#VALY (#PERCENT{P0})";

            chart.Series["Default"].Points.DataBindXY(view, "Utilisateur", view, "Nombre");

            return chart;
        }
    }
}