using duplicateFile.Classes;
using System;
using System.Windows.Forms;
using duplicateFile.Resources;
using duplicateFile.Classes.Qualifier;
using System.Reflection;
using System.Linq;

namespace duplicateFile
{
    public partial class configForm : Form
    {
        private Config _config;

        public Config Config
        {
            get { return _config; }
            set { _config = value; }
        }

        public configForm(Config config)
        {
            InitializeComponent();

            _config = (Config)config.Clone();

            setQualifierList();

            cmbSPDisk.SelectedIndex = 0;
            cmbVerbose.SelectedIndex = 0;

            chkDoublons.DataBindings.Add("Checked", _config, "Duplicate");
            chkBDD.DataBindings.Add("Checked", _config, "Bdd");
            chkSharePoint.DataBindings.Add("Checked", _config, "Sharepoint");
            chkSPfolders.DataBindings.Add("Checked", _config, "SpFolderOnly");
            chkSPLnk.DataBindings.Add("Checked", _config, "FollowRootLnkFileTarget");
            chkSPaspx.DataBindings.Add("Checked", _config, "IgnoreAspxFile");
            txtTaille.DataBindings.Add("Text", _config, "MinFileSize");
            txtEcart.DataBindings.Add("Text", _config, "MaxSizeDifference");
            txtHote.DataBindings.Add("Text", _config, "Server");
            txtBase.DataBindings.Add("Text", _config, "DataBase");
            txtUtil.DataBindings.Add("Text", _config, "User");
            txtMDP.DataBindings.Add("Text", _config, "Password");
            chkExt.DataBindings.Add("Checked", _config, "UseExtension");
            chkLog.DataBindings.Add("Checked", _config, "EnableLog");
            chkEmail.DataBindings.Add("Checked", _config, "Mail");
            txtSMTP.DataBindings.Add("Text", _config, "serverSMTP");
            txtQualifierMini.DataBindings.Add("Text", _config, "QualifierMini");
            txtDestMail.DataBindings.Add("Text", _config, "reportMail");
            txtFrom.DataBindings.Add("Text", _config, "mailFrom");
            chkOneMail.DataBindings.Add("Checked", _config, "OneMail");
            chkAttDup.DataBindings.Add("Checked", _config, "AttDup");
            chkAttStats.DataBindings.Add("Checked", _config, "AttStats");
            chkLstErr.DataBindings.Add("Checked", _config, "AttError");
            txtURL.DataBindings.Add("Text", _config, "sharePointURL");
            cmbVerbose.DataBindings.Add(new Binding(
                          "SelectedIndex",
                          _config,
                          "verboseLevel",
                          true,
                          DataSourceUpdateMode.OnPropertyChanged));
            cmbSPDisk.DataBindings.Add(new Binding(
                                      "SelectedItem",
                                      _config,
                                      "sharePointDisk",
                                      true,
                                      DataSourceUpdateMode.OnPropertyChanged));
        }

        private void setQualifierList()
        {
            Assembly qualifierAssembly = typeof(IQualifier).Assembly;

            var qlst = qualifierAssembly.GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IQualifier))).ToList();

            foreach (var t in qlst)
            {
                if (t != typeof(Qualifier))
                {
                    var o = Activator.CreateInstance(t);
                    var isSet = _config!=null&&_config.Qualifiers != null && _config.Qualifiers.Contains(t.Name);
                    chkLstQualifier.Items.Add(o,isSet );
                }
            }
        }

        private void chkSharePoint_CheckedChanged(object sender, EventArgs e)
        {
            grpSP.Enabled = chkSharePoint.Checked;
        }

        private void chkBDD_CheckedChanged(object sender, EventArgs e)
        {
            grpBDD.Enabled = chkBDD.Checked;
        }

        private void chkDoublons_CheckedChanged(object sender, EventArgs e)
        {
            grpDuplicate.Enabled = chkDoublons.Checked;
        }

        private void chkEmail_CheckedChanged(object sender, EventArgs e)
        {
            grpEmail.Enabled = chkEmail.Checked;
        }


        private void chkLstQualifier_SelectedValueChanged(object sender, EventArgs e)
        {
            _config.Qualifiers.Clear();

            foreach (var q in chkLstQualifier.CheckedItems)
                _config.Qualifiers.Add(q.GetType().Name);             
        }

    }
}