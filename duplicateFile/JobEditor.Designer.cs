namespace duplicateFile
{
    partial class JobEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobEditor));
            this.btnOk = new System.Windows.Forms.Button();
            this.cmbFrequency = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkPeriodicaly = new System.Windows.Forms.CheckBox();
            this.chkOneTime = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpHour = new System.Windows.Forms.DateTimePicker();
            this.dtpOneTime = new System.Windows.Forms.DateTimePicker();
            this.btnConfig = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lstPath = new System.Windows.Forms.ListBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cmbFrequency
            // 
            this.cmbFrequency.FormattingEnabled = true;
            this.cmbFrequency.Items.AddRange(new object[] {
            resources.GetString("cmbFrequency.Items"),
            resources.GetString("cmbFrequency.Items1"),
            resources.GetString("cmbFrequency.Items2"),
            resources.GetString("cmbFrequency.Items3"),
            resources.GetString("cmbFrequency.Items4"),
            resources.GetString("cmbFrequency.Items5"),
            resources.GetString("cmbFrequency.Items6"),
            resources.GetString("cmbFrequency.Items7"),
            resources.GetString("cmbFrequency.Items8")});
            resources.ApplyResources(this.cmbFrequency, "cmbFrequency");
            this.cmbFrequency.Name = "cmbFrequency";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // chkPeriodicaly
            // 
            resources.ApplyResources(this.chkPeriodicaly, "chkPeriodicaly");
            this.chkPeriodicaly.Checked = true;
            this.chkPeriodicaly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPeriodicaly.Name = "chkPeriodicaly";
            this.chkPeriodicaly.UseVisualStyleBackColor = true;
            this.chkPeriodicaly.CheckedChanged += new System.EventHandler(this.chkPeriodicaly_CheckedChanged);
            // 
            // chkOneTime
            // 
            resources.ApplyResources(this.chkOneTime, "chkOneTime");
            this.chkOneTime.Name = "chkOneTime";
            this.chkOneTime.UseVisualStyleBackColor = true;
            this.chkOneTime.CheckedChanged += new System.EventHandler(this.chkOneTime_CheckedChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // dtpHour
            // 
            resources.ApplyResources(this.dtpHour, "dtpHour");
            this.dtpHour.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHour.Name = "dtpHour";
            this.dtpHour.ShowUpDown = true;
            // 
            // dtpOneTime
            // 
            resources.ApplyResources(this.dtpOneTime, "dtpOneTime");
            this.dtpOneTime.Name = "dtpOneTime";
            // 
            // btnConfig
            // 
            resources.ApplyResources(this.btnConfig, "btnConfig");
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Abort;
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lstPath
            // 
            this.lstPath.FormattingEnabled = true;
            resources.ApplyResources(this.lstPath, "lstPath");
            this.lstPath.Name = "lstPath";
            this.lstPath.SelectionMode = System.Windows.Forms.SelectionMode.None;
            // 
            // btnBrowse
            // 
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtUser
            // 
            resources.ApplyResources(this.txtUser, "txtUser");
            this.txtUser.Name = "txtUser";
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // JobEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lstPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cmbFrequency);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkPeriodicaly);
            this.Controls.Add(this.chkOneTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpHour);
            this.Controls.Add(this.dtpOneTime);
            this.MaximizeBox = false;
            this.Name = "JobEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstPath;
        private System.Windows.Forms.Button btnBrowse;
        public System.Windows.Forms.ComboBox cmbFrequency;
        public System.Windows.Forms.CheckBox chkPeriodicaly;
        public System.Windows.Forms.CheckBox chkOneTime;
        public System.Windows.Forms.DateTimePicker dtpHour;
        public System.Windows.Forms.DateTimePicker dtpOneTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtUser;
        public System.Windows.Forms.TextBox txtPassword;
        public System.Windows.Forms.Button btnOk;
    }
}