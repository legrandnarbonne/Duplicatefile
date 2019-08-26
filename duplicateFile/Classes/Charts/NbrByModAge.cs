using System.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace duplicateFile.Classes.Charts
{
    public class NbrByModAge : IStatChart
    {
        public string Title
        {
            get { return "Répartition des fichiers par age de modification (Trimestre)"; }
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
                    "select TIMESTAMPDIFF(Quarter, modified, CURDATE()) AS age, count(id) as Nombre from duplicatefile.analyse group by age order by age desc";
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
            chart.Series["Default"].ChartType = SeriesChartType.Line;

            chart.Series["Default"].Points.DataBindXY(view, "age", view, "Nombre");

            return chart;
        }
    }
}