using System.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace duplicateFile.Classes.Charts
{
    public class ByStatus : IStatChart
    {
        public string Title
        {
            get { return Resources.Languages.Resources.Gr_FileByStatus; }
        }

        public bool isYFileSize
        {
            get { return false; }
        }

        public bool isYStatus
        {
            get { return true; }
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
                    "select count(id) as nombre,status from duplicatefile.analyse group by status";
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
            chart.Series["Default"].Label = "#VALX Status :#VALY (#PERCENT{P0})";

            chart.Series["Default"].Points.DataBindXY(view, "Nombre", view, "Status");

            return chart;
        }
    }
}