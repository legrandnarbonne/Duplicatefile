using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using duplicateFile.Classes;

namespace duplicateFile
{
    public partial class userForm : Form
    {
        public userForm()
        {
            InitializeComponent();


            checkedListBox1.DataSource = Analyser.Dset.Tables["user"];
            checkedListBox1.DisplayMember = "title";
            checkedListBox1.ValueMember = "Login";
        }
    }
}
