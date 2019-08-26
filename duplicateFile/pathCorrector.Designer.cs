namespace duplicateFile
{
    partial class PathCorrector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PathCorrector));
            this.dgvConv = new System.Windows.Forms.DataGridView();
            this.btnCorriger = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chkConfirm = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvConv
            // 
            this.dgvConv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvConv.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.dgvConv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvConv, "dgvConv");
            this.dgvConv.Name = "dgvConv";
            // 
            // btnCorriger
            // 
            resources.ApplyResources(this.btnCorriger, "btnCorriger");
            this.btnCorriger.Name = "btnCorriger";
            this.btnCorriger.UseVisualStyleBackColor = true;
            this.btnCorriger.Click += new System.EventHandler(this.btnCorriger_Click);
            // 
            // btnAnnuler
            // 
            resources.ApplyResources(this.btnAnnuler, "btnAnnuler");
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.UseVisualStyleBackColor = true;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // chkConfirm
            // 
            resources.ApplyResources(this.chkConfirm, "chkConfirm");
            this.chkConfirm.Name = "chkConfirm";
            this.chkConfirm.UseVisualStyleBackColor = true;
            // 
            // PathCorrector
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkConfirm);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnCorriger);
            this.Controls.Add(this.dgvConv);
            this.Name = "PathCorrector";
            this.Load += new System.EventHandler(this.pathCorrector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvConv;
        private System.Windows.Forms.Button btnCorriger;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox chkConfirm;
    }
}