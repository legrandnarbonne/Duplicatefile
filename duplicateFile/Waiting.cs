using System.ComponentModel;
using System.Windows.Forms;

namespace duplicateFile
{
    public partial class Waiting : Form
    {
        public BackgroundWorker Wr;

        public Waiting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Wr.CancelAsync();
        }
    }
}