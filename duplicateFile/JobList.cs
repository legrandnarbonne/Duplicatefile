
using Microsoft.Win32.TaskScheduler;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;

using duplicateFile.Classes;
using duplicateFile.Classes.Tools;

namespace duplicateFile
{
    public partial class JobList : Form
    {
        public JobList()
        {
            InitializeComponent();
            updateJobList();
        }

        private void updateJobList()
        {
            lstJob.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(Config.DefaultJobPath);

            foreach (FileInfo fi in di.GetFiles())
                lstJob.Items.Add(Path.GetFileNameWithoutExtension(fi.Name));
            
            if (lstJob.Items.Count > 0) lstJob.SelectedIndex = 0;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstJob.SelectedIndex == -1) return;
            var jobName = lstJob.SelectedItem.ToString();

            var config = Config.Load(Config.DefaultJobPath + "\\" + jobName + ".conf");
            var frmJob = new JobEditor(config);
            frmJob.btnOk.Text = Resources.Languages.Resources.Txt_Modifier;

            //get task information
            var task = GetTask(jobName);
            if (task == null) return;

            frmJob.Job.Id = new Guid(jobName);
            frmJob.setTitle();

            var triggerType = SchedulerTrigger.getFromTrigger(task.Definition.Triggers[0]);
            if (triggerType == SchedulerTrigger.TriggerTypes.OneTime)
            {
                frmJob.chkOneTime.Checked = true;
                frmJob.dtpOneTime.Value = task.Definition.Triggers[0].StartBoundary;
            }
            else
            {
                frmJob.dtpHour.Value = task.Definition.Triggers[0].StartBoundary;
                frmJob.cmbFrequency.SelectedIndex = (int)triggerType;
            }

            if (frmJob.ShowDialog(this) == DialogResult.OK)
            {
                //recordJob(frmJob);
            }
        }

        private void btnSupp_Click(object sender, EventArgs e)
        {
            if (lstJob.SelectedIndex == -1) return;
            var jobName = lstJob.SelectedItem.ToString();

            try
            {
                DeleteTask(jobName);

                var fi = new FileInfo(Config.DefaultJobPath + "\\" + jobName + ".conf");
                if (fi.Exists) fi.Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Languages.Resources.Erreur, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            updateJobList();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var frmJob = new JobEditor((Config)Analyser.Config.Clone());
            var result = frmJob.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                //recordJob(frmJob);
            }

            updateJobList();
        }


        public static bool DeleteTask(string taskName)
        {
            using (TaskService ts = new TaskService())
            {
                if (GetTask(taskName) == null) return false;
                ts.GetFolder("\\Duplicate File").DeleteTask(taskName);
                return true;
            }
        }

        public static Microsoft.Win32.TaskScheduler.Task GetTask(string taskName)
        {
            using (TaskService ts = new TaskService())
            {
                var task = ts.GetFolder("\\Duplicate File").GetTasks().Where(a => a.Name.ToLower() == taskName.ToLower()).FirstOrDefault();
                return task;
            }
            return null;
        }

        private void btnHisto_Click(object sender, EventArgs e)
        {
            if (lstJob.SelectedItem == null) return;

            var conn = DataTools.DataTools.SetConnection(Analyser.Config.ConnectionString);
            var adapter = DataTools.DataTools.getAdapter("select * from stats", conn);

            DataTable dtStats = new DataTable();

            adapter.FillSchema(dtStats, SchemaType.Source);

            conn.Open();
            adapter.Fill(dtStats);
            conn.Close();

            var frmJH = new JobHistory();

            frmJH.dgvHisto.DataSource = dtStats;

            frmJH.dgvHisto.Columns["IdStats"].Visible = false;
            frmJH.dgvHisto.Columns["JobId"].Visible = false;
            ResourceHelper.setHeaders(frmJH.dgvHisto);

            frmJH.ShowDialog();

            }
    }
}