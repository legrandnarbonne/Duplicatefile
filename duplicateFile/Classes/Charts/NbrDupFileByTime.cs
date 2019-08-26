using System.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace duplicateFile.Classes.Charts
{
    public class NbrDupFileByTime : IStatChart
    {
        public string Title
        {
            get { return Resources.Languages.Resources.Gr_NumberOfDuplicateFile; }
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
                    "select NumberOfDuplicateFile as nombre,startDate as date from duplicatefile.stats";
            }
        }

        public Chart draw(DataView view)
        {
            Chart chart = new Chart();

            chart.Width = 900;
            chart.Height = 600;

            chart.ChartAreas.Add("Area");
            chart.ChartAreas["Area"].AxisX.LabelStyle.Interval = 1;
            chart.ChartAreas["Area"].Area3DStyle.Enable3D = false;
            chart.ChartAreas["Area"].BackColor = System.Drawing.Color.White;

            chart.Series.Add("Default");
            chart.Series["Default"].ChartType = SeriesChartType.SplineArea;

            chart.Series["Default"].Points.DataBindXY(view, "Date", view, "Nombre");

            return chart;
        }
    }
}