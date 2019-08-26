using duplicateFile.Classes;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Windows.Forms;

namespace duplicateFile
{
    public partial class JobEditor : Form
    {
        private Job _job;

        public Job Job
        {
            get { return _job; }
            set { _job = value; }
        }

        public JobEditor(Config config)
        {
            _job = new Job(config);
            InitializeComponent();
            cmbFrequency.SelectedIndex = 0;
            setTitle();

            lstPath.DataSource = _job.Config.Paths;

            txtUser.Text = config.TaskUser;
            txtPassword.Text = config.TaskPassword;
        }

        public void setTitle()
        {
            this.Text = "Tâche " + _job.Id;
        }

        private void chkOneTime_CheckedChanged(object sender, EventArgs e)
        {
            dtpOneTime.Enabled = chkOneTime.Checked;
            chkPeriodicaly.Checked = !chkOneTime.Checked;
        }

        private void chkPeriodicaly_CheckedChanged(object sender, EventArgs e)
        {
            cmbFrequency.Enabled = chkPeriodicaly.Checked;
            chkOneTime.Checked = !chkPeriodicaly.Checked;
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            var cfgFrm = new configForm(_job.Config);

            if (cfgFrm.ShowDialog(this) == DialogResult.OK)
            {
                _job.Config = cfgFrm.Config;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var fbd = new FoldersSelect();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                if (fbd.SelectedFolders.Length == 0) return;
                else
                {
                    lstPath.DataSource = fbd.SelectedFolders;
                    _job.Config.Paths = fbd.SelectedFolders;
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "" || txtUser.Text == "")
            {
                MessageBox.Show(
                    Resources.Languages.Resources.Error_Nom_Utilisateur_Non_Vide,
                    Resources.Languages.Resources.Erreur, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (recordJob())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool recordJob()//JobEditor frmJob)
        {
            //record configuration
            _job.Config.TaskPassword = txtPassword.Text;
            _job.Config.TaskUser = txtUser.Text;

            try
            {
                //add task to windows scheduler
                using (TaskService mgr = new TaskService())
                {
                    if (!mgr.RootFolder.SubFolders.Exists("Duplicate File")) mgr.RootFolder.CreateFolder("Duplicate File");

                    TaskDefinition td = mgr.NewTask();

                    td.RegistrationInfo.Description = "Duplicate file job";

                    Trigger trigger;
                    if (chkPeriodicaly.Checked)
                    {
                        trigger = SchedulerTrigger.getTrigger(cmbFrequency.SelectedIndex);
                        trigger.StartBoundary = dtpHour.Value;
                    }
                    else
                    {
                        trigger = SchedulerTrigger.getTrigger((int)SchedulerTrigger.TriggerTypes.OneTime);
                        trigger.StartBoundary = dtpOneTime.Value;
                    }

                    td.Triggers.Add(trigger);

                    var action = new ExecAction()
                    {
                        Path = "duplicateFile.exe",
                        WorkingDirectory = Config.DefaultPath,
                        Arguments = _job.Id.ToString()
                    };

                    td.Actions.Add(action);
                    
                    var task = mgr.RootFolder.RegisterTaskDefinition("\\Duplicate File\\" + _job.Id, td, TaskCreation.CreateOrUpdate, _job.Config.TaskUser, _job.Config.TaskPassword, TaskLogonType.Password);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.Languages.Resources.Error_Creation_tache, Resources.Languages.Resources.Erreur, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            _job.Config.Save(Config.DefaultJobPath + _job.Id + ".conf");
            return true;
        }

    }
}