using System;
using System.Windows.Forms;

namespace duplicateFile
{
    public partial class FoldersSelect : Form
    {
        public string[] SelectedFolders;

        public FoldersSelect()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectedFolders = foldersBrowser1.GetSelectedFolders();
            this.Close();
        }
    }
}