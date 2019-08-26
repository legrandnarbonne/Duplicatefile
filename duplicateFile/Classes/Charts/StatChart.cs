using System.Windows.Forms.DataVisualization.Charting;

namespace duplicateFile.Classes.Charts
{
    public interface IStatChart
    {
        Chart draw(System.Data.DataView view);

        string Sql { get; }
        string Title { get; }
        bool isYFileSize { get; }
        bool isYStatus { get; }
        bool concatXvalue { get; }
    }
}