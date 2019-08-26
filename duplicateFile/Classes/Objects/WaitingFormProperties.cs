using System.Windows.Forms;

namespace duplicateFile.Classes
{
    public class WaitingFormProperties
    {
        public readonly string CurrentFile;
        public readonly int MaxProgressValue;
        public readonly ProgressBarStyle ProgressStyle;
        public readonly string Status;

        public WaitingFormProperties(string status, string currentFile = null, int maxProgressValue = 0,
            ProgressBarStyle progressStyle = ProgressBarStyle.Marquee)
        {
            Status = status;
            CurrentFile = currentFile;
            ProgressStyle = progressStyle;
            MaxProgressValue = maxProgressValue;
        }
    }
}