using System.Windows.Forms;

namespace duplicateFile
{
    public partial class StatsFrm : Form
    {
        public StatsFrm(Classes.Stats s)
        {
            InitializeComponent();
            txtStats.Text = s.ToString();
        }
    }
}