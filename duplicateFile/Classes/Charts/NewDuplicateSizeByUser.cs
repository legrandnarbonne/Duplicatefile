using System.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace duplicateFile.Classes.Charts
{
    public class NewDuplicateSizeByUser : IStatChart
    {
        public string Title
        {
            get { return Resources.Languages.Resources.GR_Vol_Duplique_Dern_Analyse; }
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
                    "select sum(taille) as Volume,title as Utilisateur from duplicatefile.analyse join user on user.login=analyse.user where isduplicate=1 and status=2 group by user order by volume desc limit 30";
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
            //chart.Series["Default"].Palette = ChartColorPalette.Chocolate;
            chart.Series["Default"]["PieLabelStyle"] = "Outside";
            chart.Series["Default"].Label = "#VALY (#PERCENT{P0})";

            chart.Series["Default"].Points.DataBindXY(view, "Utilisateur", view, "Volume");

            return chart;
        }
    }
}