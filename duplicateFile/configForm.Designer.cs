namespace duplicateFile
{
    partial class configForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(configForm));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtQualifierMini = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.chkLstQualifier = new System.Windows.Forms.CheckedListBox();
            this.grpDuplicate = new System.Windows.Forms.GroupBox();
            this.chkExt = new System.Windows.Forms.CheckBox();
            this.txtEcart = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTaille = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkDoublons = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkSharePoint = new System.Windows.Forms.CheckBox();
            this.grpSP = new System.Windows.Forms.GroupBox();
            this.chkSPLnk = new System.Windows.Forms.CheckBox();
            this.cmbSPDisk = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSPaspx = new System.Windows.Forms.CheckBox();
            this.chkSPfolders = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.chkBDD = new System.Windows.Forms.CheckBox();
            this.grpBDD = new System.Windows.Forms.GroupBox();
            this.txtBase = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMDP = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtUtil = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHote = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbVerbose = new System.Windows.Forms.ComboBox();
            this.chkEmail = new System.Windows.Forms.CheckBox();
            this.grpEmail = new System.Windows.Forms.GroupBox();
            this.chkLstErr = new System.Windows.Forms.CheckBox();
            this.chkOneMail = new System.Windows.Forms.CheckBox();
            this.chkAttStats = new System.Windows.Forms.CheckBox();
            this.chkAttDup = new System.Windows.Forms.CheckBox();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSMTP = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDestMail = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpDuplicate.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.grpSP.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.grpBDD.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.grpEmail.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Cursor = System.Windows.Forms.Cursors.HSplit;
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.chkBDD_CheckedChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.grpDuplicate);
            this.tabPage1.Controls.Add(this.chkDoublons);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtQualifierMini);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.chkLstQualifier);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtQualifierMini
            // 
            resources.ApplyResources(this.txtQualifierMini, "txtQualifierMini");
            this.txtQualifierMini.Name = "txtQualifierMini";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // chkLstQualifier
            // 
            this.chkLstQualifier.CheckOnClick = true;
            this.chkLstQualifier.FormattingEnabled = true;
            resources.ApplyResources(this.chkLstQualifier, "chkLstQualifier");
            this.chkLstQualifier.Name = "chkLstQualifier";
            this.chkLstQualifier.SelectedValueChanged += new System.EventHandler(this.chkLstQualifier_SelectedValueChanged);
            // 
            // grpDuplicate
            // 
            this.grpDuplicate.Controls.Add(this.chkExt);
            this.grpDuplicate.Controls.Add(this.txtEcart);
            this.grpDuplicate.Controls.Add(this.label4);
            this.grpDuplicate.Controls.Add(this.label5);
            this.grpDuplicate.Controls.Add(this.txtTaille);
            this.grpDuplicate.Controls.Add(this.label2);
            this.grpDuplicate.Controls.Add(this.label3);
            resources.ApplyResources(this.grpDuplicate, "grpDuplicate");
            this.grpDuplicate.Name = "grpDuplicate";
            this.grpDuplicate.TabStop = false;
            // 
            // chkExt
            // 
            resources.ApplyResources(this.chkExt, "chkExt");
            this.chkExt.Checked = true;
            this.chkExt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExt.Name = "chkExt";
            this.chkExt.UseVisualStyleBackColor = true;
            // 
            // txtEcart
            // 
            resources.ApplyResources(this.txtEcart, "txtEcart");
            this.txtEcart.Name = "txtEcart";
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
            // txtTaille
            // 
            resources.ApplyResources(this.txtTaille, "txtTaille");
            this.txtTaille.Name = "txtTaille";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // chkDoublons
            // 
            resources.ApplyResources(this.chkDoublons, "chkDoublons");
            this.chkDoublons.Name = "chkDoublons";
            this.chkDoublons.UseVisualStyleBackColor = true;
            this.chkDoublons.CheckedChanged += new System.EventHandler(this.chkDoublons_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkSharePoint);
            this.tabPage2.Controls.Add(this.grpSP);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chkSharePoint
            // 
            resources.ApplyResources(this.chkSharePoint, "chkSharePoint");
            this.chkSharePoint.Name = "chkSharePoint";
            this.chkSharePoint.UseVisualStyleBackColor = true;
            this.chkSharePoint.CheckedChanged += new System.EventHandler(this.chkSharePoint_CheckedChanged);
            // 
            // grpSP
            // 
            this.grpSP.Controls.Add(this.chkSPLnk);
            this.grpSP.Controls.Add(this.cmbSPDisk);
            this.grpSP.Controls.Add(this.label13);
            this.grpSP.Controls.Add(this.txtURL);
            this.grpSP.Controls.Add(this.label1);
            this.grpSP.Controls.Add(this.chkSPaspx);
            this.grpSP.Controls.Add(this.chkSPfolders);
            resources.ApplyResources(this.grpSP, "grpSP");
            this.grpSP.Name = "grpSP";
            this.grpSP.TabStop = false;
            // 
            // chkSPLnk
            // 
            resources.ApplyResources(this.chkSPLnk, "chkSPLnk");
            this.chkSPLnk.Checked = true;
            this.chkSPLnk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSPLnk.Name = "chkSPLnk";
            this.chkSPLnk.UseVisualStyleBackColor = true;
            // 
            // cmbSPDisk
            // 
            this.cmbSPDisk.FormattingEnabled = true;
            this.cmbSPDisk.Items.AddRange(new object[] {
            resources.GetString("cmbSPDisk.Items"),
            resources.GetString("cmbSPDisk.Items1"),
            resources.GetString("cmbSPDisk.Items2"),
            resources.GetString("cmbSPDisk.Items3"),
            resources.GetString("cmbSPDisk.Items4"),
            resources.GetString("cmbSPDisk.Items5"),
            resources.GetString("cmbSPDisk.Items6"),
            resources.GetString("cmbSPDisk.Items7"),
            resources.GetString("cmbSPDisk.Items8"),
            resources.GetString("cmbSPDisk.Items9"),
            resources.GetString("cmbSPDisk.Items10"),
            resources.GetString("cmbSPDisk.Items11"),
            resources.GetString("cmbSPDisk.Items12"),
            resources.GetString("cmbSPDisk.Items13"),
            resources.GetString("cmbSPDisk.Items14"),
            resources.GetString("cmbSPDisk.Items15"),
            resources.GetString("cmbSPDisk.Items16"),
            resources.GetString("cmbSPDisk.Items17"),
            resources.GetString("cmbSPDisk.Items18"),
            resources.GetString("cmbSPDisk.Items19"),
            resources.GetString("cmbSPDisk.Items20"),
            resources.GetString("cmbSPDisk.Items21"),
            resources.GetString("cmbSPDisk.Items22"),
            resources.GetString("cmbSPDisk.Items23"),
            resources.GetString("cmbSPDisk.Items24"),
            resources.GetString("cmbSPDisk.Items25"),
            resources.GetString("cmbSPDisk.Items26")});
            resources.ApplyResources(this.cmbSPDisk, "cmbSPDisk");
            this.cmbSPDisk.Name = "cmbSPDisk";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // txtURL
            // 
            resources.ApplyResources(this.txtURL, "txtURL");
            this.txtURL.Name = "txtURL";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // chkSPaspx
            // 
            resources.ApplyResources(this.chkSPaspx, "chkSPaspx");
            this.chkSPaspx.Checked = true;
            this.chkSPaspx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSPaspx.Name = "chkSPaspx";
            this.chkSPaspx.UseVisualStyleBackColor = true;
            // 
            // chkSPfolders
            // 
            resources.ApplyResources(this.chkSPfolders, "chkSPfolders");
            this.chkSPfolders.Checked = true;
            this.chkSPfolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSPfolders.Name = "chkSPfolders";
            this.chkSPfolders.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.chkBDD);
            this.tabPage3.Controls.Add(this.grpBDD);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // chkBDD
            // 
            resources.ApplyResources(this.chkBDD, "chkBDD");
            this.chkBDD.Name = "chkBDD";
            this.chkBDD.UseVisualStyleBackColor = true;
            this.chkBDD.CheckedChanged += new System.EventHandler(this.chkBDD_CheckedChanged);
            // 
            // grpBDD
            // 
            this.grpBDD.Controls.Add(this.txtBase);
            this.grpBDD.Controls.Add(this.label9);
            this.grpBDD.Controls.Add(this.txtMDP);
            this.grpBDD.Controls.Add(this.label8);
            this.grpBDD.Controls.Add(this.txtUtil);
            this.grpBDD.Controls.Add(this.label7);
            this.grpBDD.Controls.Add(this.txtHote);
            this.grpBDD.Controls.Add(this.label6);
            resources.ApplyResources(this.grpBDD, "grpBDD");
            this.grpBDD.Name = "grpBDD";
            this.grpBDD.TabStop = false;
            // 
            // txtBase
            // 
            resources.ApplyResources(this.txtBase, "txtBase");
            this.txtBase.Name = "txtBase";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtMDP
            // 
            resources.ApplyResources(this.txtMDP, "txtMDP");
            this.txtMDP.Name = "txtMDP";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtUtil
            // 
            resources.ApplyResources(this.txtUtil, "txtUtil");
            this.txtUtil.Name = "txtUtil";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txtHote
            // 
            resources.ApplyResources(this.txtHote, "txtHote");
            this.txtHote.Name = "txtHote";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.cmbVerbose);
            this.tabPage4.Controls.Add(this.chkEmail);
            this.tabPage4.Controls.Add(this.grpEmail);
            this.tabPage4.Controls.Add(this.chkLog);
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // cmbVerbose
            // 
            this.cmbVerbose.FormattingEnabled = true;
            this.cmbVerbose.Items.AddRange(new object[] {
            resources.GetString("cmbVerbose.Items"),
            resources.GetString("cmbVerbose.Items1"),
            resources.GetString("cmbVerbose.Items2"),
            resources.GetString("cmbVerbose.Items3")});
            resources.ApplyResources(this.cmbVerbose, "cmbVerbose");
            this.cmbVerbose.Name = "cmbVerbose";
            // 
            // chkEmail
            // 
            resources.ApplyResources(this.chkEmail, "chkEmail");
            this.chkEmail.Name = "chkEmail";
            this.chkEmail.UseVisualStyleBackColor = true;
            this.chkEmail.CheckedChanged += new System.EventHandler(this.chkEmail_CheckedChanged);
            // 
            // grpEmail
            // 
            this.grpEmail.Controls.Add(this.chkLstErr);
            this.grpEmail.Controls.Add(this.chkOneMail);
            this.grpEmail.Controls.Add(this.chkAttStats);
            this.grpEmail.Controls.Add(this.chkAttDup);
            this.grpEmail.Controls.Add(this.txtFrom);
            this.grpEmail.Controls.Add(this.label11);
            this.grpEmail.Controls.Add(this.txtSMTP);
            this.grpEmail.Controls.Add(this.label10);
            this.grpEmail.Controls.Add(this.txtDestMail);
            this.grpEmail.Controls.Add(this.label12);
            resources.ApplyResources(this.grpEmail, "grpEmail");
            this.grpEmail.Name = "grpEmail";
            this.grpEmail.TabStop = false;
            // 
            // chkLstErr
            // 
            resources.ApplyResources(this.chkLstErr, "chkLstErr");
            this.chkLstErr.Name = "chkLstErr";
            this.chkLstErr.UseVisualStyleBackColor = true;
            // 
            // chkOneMail
            // 
            resources.ApplyResources(this.chkOneMail, "chkOneMail");
            this.chkOneMail.Name = "chkOneMail";
            this.chkOneMail.UseVisualStyleBackColor = true;
            // 
            // chkAttStats
            // 
            resources.ApplyResources(this.chkAttStats, "chkAttStats");
            this.chkAttStats.Name = "chkAttStats";
            this.chkAttStats.UseVisualStyleBackColor = true;
            // 
            // chkAttDup
            // 
            resources.ApplyResources(this.chkAttDup, "chkAttDup");
            this.chkAttDup.Name = "chkAttDup";
            this.chkAttDup.UseVisualStyleBackColor = true;
            // 
            // txtFrom
            // 
            resources.ApplyResources(this.txtFrom, "txtFrom");
            this.txtFrom.Name = "txtFrom";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // txtSMTP
            // 
            resources.ApplyResources(this.txtSMTP, "txtSMTP");
            this.txtSMTP.Name = "txtSMTP";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // txtDestMail
            // 
            resources.ApplyResources(this.txtDestMail, "txtDestMail");
            this.txtDestMail.Name = "txtDestMail";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // chkLog
            // 
            resources.ApplyResources(this.chkLog, "chkLog");
            this.chkLog.Name = "chkLog";
            this.chkLog.UseVisualStyleBackColor = true;
            // 
            // btnRecord
            // 
            this.btnRecord.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnRecord, "btnRecord");
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // configForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.splitter1);
            this.MaximizeBox = false;
            this.Name = "configForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpDuplicate.ResumeLayout(false);
            this.grpDuplicate.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.grpSP.ResumeLayout(false);
            this.grpSP.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.grpBDD.ResumeLayout(false);
            this.grpBDD.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.grpEmail.ResumeLayout(false);
            this.grpEmail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox grpDuplicate;
        private System.Windows.Forms.CheckBox chkExt;
        private System.Windows.Forms.TextBox txtEcart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTaille;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkDoublons;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox chkSharePoint;
        private System.Windows.Forms.GroupBox grpSP;
        private System.Windows.Forms.CheckBox chkSPaspx;
        private System.Windows.Forms.CheckBox chkSPfolders;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox chkBDD;
        private System.Windows.Forms.GroupBox grpBDD;
        private System.Windows.Forms.TextBox txtBase;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMDP;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtUtil;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtHote;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox chkEmail;
        private System.Windows.Forms.GroupBox grpEmail;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSMTP;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDestMail;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkLog;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSPDisk;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbVerbose;
        private System.Windows.Forms.CheckBox chkAttStats;
        private System.Windows.Forms.CheckBox chkAttDup;
        private System.Windows.Forms.CheckBox chkOneMail;
        private System.Windows.Forms.CheckBox chkLstErr;
        private System.Windows.Forms.CheckBox chkSPLnk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox chkLstQualifier;
        private System.Windows.Forms.TextBox txtQualifierMini;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
    }
}