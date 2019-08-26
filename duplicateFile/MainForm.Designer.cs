using duplicateFile.Properties;

namespace duplicateFile
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnAnalyse = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Actions = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOpen = new JDComponentLib.BoutonPlus();
            this.btnOpenFolder = new JDComponentLib.BoutonPlus();
            this.btnExport = new JDComponentLib.BoutonPlus();
            this.btnSupp = new JDComponentLib.BoutonPlus();
            this.btnUser = new JDComponentLib.BoutonPlus();
            this.btnGraph = new JDComponentLib.BoutonPlus();
            this.btnHash = new JDComponentLib.BoutonPlus();
            this.btnCorr = new JDComponentLib.BoutonPlus();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnJobs = new JDComponentLib.BoutonPlus();
            this.btnConfig = new JDComponentLib.BoutonPlus();
            this.btnCharge = new JDComponentLib.BoutonPlus();
            this.btnEnrg = new JDComponentLib.BoutonPlus();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dgvDoublons = new System.Windows.Forms.DataGridView();
            this.btnStats = new JDComponentLib.BoutonPlus();
            this.lbNbDoublons = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvChemin = new System.Windows.Forms.DataGridView();
            this.lbNbChemin = new System.Windows.Forms.Label();
            this.lbNbCarac = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvErreurs = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.dgv2003 = new System.Windows.Forms.DataGridView();
            this.chkAttributs = new System.Windows.Forms.CheckBox();
            this.btnConvertir = new System.Windows.Forms.Button();
            this.lbNb2003 = new System.Windows.Forms.Label();
            this.tabQualifier = new System.Windows.Forms.TabPage();
            this.splitQualifier = new System.Windows.Forms.SplitContainer();
            this.dgvQualifier = new System.Windows.Forms.DataGridView();
            this.txtQualPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNbQualifier = new System.Windows.Forms.Label();
            this.lblLevelQualifier = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackQualifier = new System.Windows.Forms.TrackBar();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.Actions.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoublons)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChemin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErreurs)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2003)).BeginInit();
            this.tabQualifier.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitQualifier)).BeginInit();
            this.splitQualifier.Panel1.SuspendLayout();
            this.splitQualifier.Panel2.SuspendLayout();
            this.splitQualifier.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQualifier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackQualifier)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAnalyse
            // 
            this.btnAnalyse.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnAnalyse, "btnAnalyse");
            this.btnAnalyse.Name = "btnAnalyse";
            this.btnAnalyse.Text = global::duplicateFile.Resources.Languages.Resources.Txt_Analyser;
            this.btnAnalyse.UseVisualStyleBackColor = false;
            this.btnAnalyse.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // Actions
            // 
            this.Actions.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Actions.Controls.Add(this.flowLayoutPanel2);
            this.Actions.Controls.Add(this.flowLayoutPanel1);
            this.Actions.Controls.Add(this.btnAnalyse);
            resources.ApplyResources(this.Actions, "Actions");
            this.Actions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Actions.Name = "Actions";
            this.Actions.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnOpen);
            this.flowLayoutPanel2.Controls.Add(this.btnOpenFolder);
            this.flowLayoutPanel2.Controls.Add(this.btnExport);
            this.flowLayoutPanel2.Controls.Add(this.btnSupp);
            this.flowLayoutPanel2.Controls.Add(this.btnUser);
            this.flowLayoutPanel2.Controls.Add(this.btnGraph);
            this.flowLayoutPanel2.Controls.Add(this.btnHash);
            this.flowLayoutPanel2.Controls.Add(this.btnCorr);
            resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // btnOpen
            // 
            this.btnOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnOpen, "btnOpen");
            this.btnOpen.Image_active = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image_active")));
            this.btnOpen.Image_activee = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image_activee")));
            this.btnOpen.Image_Desactivee = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image_Desactivee")));
            this.btnOpen.Image_repos = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image_repos")));
            this.btnOpen.Image_Select = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image_Select")));
            this.btnOpen.ModeBascule = false;
            this.btnOpen.Mot_de_Passe = "280572";
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Select = false;
            this.btnOpen.Click += new JDComponentLib.BoutonPlus.ClickedHandler(this.btnOpen_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnOpenFolder, "btnOpenFolder");
            this.btnOpenFolder.Image_active = ((System.Drawing.Image)(resources.GetObject("btnOpenFolder.Image_active")));
            this.btnOpenFolder.Image_activee = ((System.Drawing.Image)(resources.GetObject("btnOpenFolder.Image_activee")));
            this.btnOpenFolder.Image_Desactivee = ((System.Drawing.Image)(resources.GetObject("btnOpenFolder.Image_Desactivee")));
            this.btnOpenFolder.Image_repos = ((System.Drawing.Image)(resources.GetObject("btnOpenFolder.Image_repos")));
            this.btnOpenFolder.Image_Select = ((System.Drawing.Image)(resources.GetObject("btnOpenFolder.Image_Select")));
            this.btnOpenFolder.ModeBascule = false;
            this.btnOpenFolder.Mot_de_Passe = "280572";
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Select = false;
            this.btnOpenFolder.Click += new JDComponentLib.BoutonPlus.ClickedHandler(this.boutonPlus1_Click);
            // 
            // btnExport
            // 
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.Image_active = global::duplicateFile.Resources.Resources.excel_a1;
            this.btnExport.Image_activee = global::duplicateFile.Resources.Resources.excel;
            this.btnExport.Image_Desactivee = global::duplicateFile.Resources.Resources.excel_e1;
            this.btnExport.Image_repos = global::duplicateFile.Resources.Resources.excel;
            this.btnExport.Image_Select = global::duplicateFile.Resources.Resources.excel;
            this.btnExport.ModeBascule = false;
            this.btnExport.Mot_de_Passe = "280572";
            this.btnExport.Name = "btnExport";
            this.btnExport.Select = false;
            this.btnExport.Click += new JDComponentLib.BoutonPlus.ClickedHandler(this.boutonPlus2_Click);
            // 
            // btnSupp
            // 
            this.btnSupp.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnSupp, "btnSupp");
            this.btnSupp.Image_active = ((System.Drawing.Image)(resources.GetObject("btnSupp.Image_active")));
            this.btnSupp.Image_activee = ((System.Drawing.Image)(resources.GetObject("btnSupp.Image_activee")));
            this.btnSupp.Image_Desactivee = ((System.Drawing.Image)(resources.GetObject("btnSupp.Image_Desactivee")));
            this.btnSupp.Image_repos = ((System.Drawing.Image)(resources.GetObject("btnSupp.Image_repos")));
            this.btnSupp.Image_Select = ((System.Drawing.Image)(resources.GetObject("btnSupp.Image_Select")));
            this.btnSupp.ModeBascule = false;
            this.btnSupp.Mot_de_Passe = "280572";
            this.btnSupp.Name = "btnSupp";
            this.btnSupp.Select = false;
            this.btnSupp.Click += new JDComponentLib.BoutonPlus.ClickedHandler(this.btnSupp_Click);
            // 
            // btnUser
            // 
            this.btnUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUser.Image_active = global::duplicateFile.Properties.Resources.user;
            this.btnUser.Image_activee = global::duplicateFile.Properties.Resources.user_a;
            this.btnUser.Image_Desactivee = global::duplicateFile.Properties.Resources.user_e;
            this.btnUser.Image_repos = global::duplicateFile.Properties.Resources.user;
            this.btnUser.Image_Select = global::duplicateFile.Properties.Resources.user;
            resources.ApplyResources(this.btnUser, "btnUser");
            this.btnUser.ModeBascule = false;
            this.btnUser.Mot_de_Passe = "280572";
            this.btnUser.Name = "btnUser";
            this.btnUser.Select = false;
            this.btnUser.Click += new JDComponentLib.BoutonPlus.ClickedHandler(this.btnUser_Click);
            // 
            // btnGraph
            // 
            this.btnGraph.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGraph.Image_active = global::duplicateFile.Resources.Resources.graph;
            this.btnGraph.Image_activee = global::duplicateFile.Resources.Resources.graph;
            this.btnGraph.Image_Desactivee = global::duplicateFile.Resources.Resources.graph_e;
            this.btnGraph.Image_repos = global::duplicateFile.Resources.Resources.graph;
            this.btnGraph.Image_Select = global::duplicateFile.Resources.Resources.graph;
            resources.ApplyResources(this.btnGraph, "btnGraph");
            this.btnGraph.ModeBascule = false;
            this.btnGraph.Mot_de_Passe = "280572";
            this.btnGraph.Name = "btnGraph";
            this.btnGraph.Select = false;
            this.btnGraph.Click += new JDComponentLib.BoutonPlus.ClickedHandler(this.btnGraph_Click);
            // 
            // btnHash
            // 
            this.btnHash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHash.Image_active = global::duplicateFile.Resources.Resources.hash;
            this.btnHash.Image_activee = global::duplicateFile.Resources.Resources.hash;
            this.btnHash.Image_Desactivee = global::duplicateFile.Resources.Resources.hash_e;
            this.btnHash.Image_repos = global::duplicateFile.Resources.Resources.hash;
            this.btnHash.Image_Select = global::duplicateFile.Resources.Resources.hash;
            resources.ApplyResources(this.btnHash, "btnHash");
            this.btnHash.ModeBascule = false;
            this.btnHash.Mot_de_Passe = "280572";
            this.btnHash.Name = "btnHash";
            this.btnHash.Select = false;
            this.btnHash.Click += new JDComponentLib.BoutonPlus.ClickedHandler(this.btnHash_Click);
            // 
            // btnCorr
            // 
            this.btnCorr.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnCorr, "btnCorr");
            this.btnCorr.Image_active = global::duplicateFile.Resources.Resources.corr_a;
            this.btnCorr.Image_activee = global::duplicateFile.Resources.Resources.corr;
            this.btnCorr.Image_Desactivee = global::duplicateFile.Resources.Resources.corr_e;
            this.btnCorr.Image_repos = global::duplicateFile.Resources.Resources.corr;
            this.btnCorr.Image_Select = global::duplicateFile.Resources.Resources.corr;
            this.btnCorr.ModeBascule = false;
            this.btnCorr.Mot_de_Passe = "280572";
            this.btnCorr.Name = "btnCorr";
            this.btnCorr.Select = false;
            this.btnCorr.Click += new JDComponentLib.BoutonPlus.ClickedHandler(this.btnCorr_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnJobs);
            this.flowLayoutPanel1.Controls.Add(this.btnConfig);
            this.flowLayoutPanel1.Controls.Add(this.btnCharge);
            this.flowLayoutPanel1.Controls.Add(this.btnEnrg);
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // btnJobs
            // 
            this.btnJobs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnJobs.Image_active = global::duplicateFile.Resources.Resources.horloge;
            this.btnJobs.Image_activee = global::duplicateFile.Resources.Resources.horloge;
            this.btnJobs.Image_Desactivee = global::duplicateFile.Resources.Resources.horloge;
            this.btnJobs.Image_repos = global::duplicateFile.Resources.Resources.horloge;
            this.btnJobs.Image_Select = global::duplicateFile.Resources.Resources.horloge;
            resources.ApplyResources(this.btnJobs, "btnJobs");
            this.btnJobs.ModeBascule = false;
            this.btnJobs.Mot_de_Passe = "280572";
            this.btnJobs.Name = "btnJobs";
            this.btnJobs.Select = false;
            this.btnJobs.Click += new JDComponentLib.BoutonPlus.ClickedHandler(this.btnJobs_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfig.Image_active = global::duplicateFile.Resources.Resources.config;
            this.btnConfig.Image_activee = global::duplicateFile.Resources.Resources.config;
            this.btnConfig.Image_Desactivee = global::duplicateFile.Resources.Resources.config;
            this.btnConfig.Image_repos = global::duplicateFile.Resources.Resources.config;
            this.btnConfig.Image_Select = global::duplicateFile.Resources.Resources.config;
            resources.ApplyResources(this.btnConfig, "btnConfig");
            this.btnConfig.ModeBascule = false;
            this.btnConfig.Mot_de_Passe = "280572";
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Select = false;
            this.btnConfig.Click += new JDComponentLib.BoutonPlus.ClickedHandler(this.btnConfig_Click);
            // 
            // btnCharge
            // 
            this.btnCharge.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCharge.Image_active = global::duplicateFile.Resources.Resources.ouvr_a;
            this.btnCharge.Image_activee = global::duplicateFile.Resources.Resources.ouvr;
            this.btnCharge.Image_Desactivee = global::duplicateFile.Resources.Resources.ouvr_e;
            this.btnCharge.Image_repos = global::duplicateFile.Resources.Resources.ouvr;
            this.btnCharge.Image_Select = global::duplicateFile.Resources.Resources.ouvr;
            resources.ApplyResources(this.btnCharge, "btnCharge");
            this.btnCharge.ModeBascule = false;
            this.btnCharge.Mot_de_Passe = "280572";
            this.btnCharge.Name = "btnCharge";
            this.btnCharge.Select = false;
            this.btnCharge.Click += new JDComponentLib.BoutonPlus.ClickedHandler(this.btnCharge_Click);
            // 
            // btnEnrg
            // 
            this.btnEnrg.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnEnrg, "btnEnrg");
            this.btnEnrg.Image_active = global::duplicateFile.Resources.Resources.enrg_a;
            this.btnEnrg.Image_activee = global::duplicateFile.Resources.Resources.enrg;
            this.btnEnrg.Image_Desactivee = global::duplicateFile.Resources.Resources.enrg_e;
            this.btnEnrg.Image_repos = global::duplicateFile.Resources.Resources.enrg;
            this.btnEnrg.Image_Select = global::duplicateFile.Resources.Resources.enrg;
            this.btnEnrg.ModeBascule = false;
            this.btnEnrg.Mot_de_Passe = "280572";
            this.btnEnrg.Name = "btnEnrg";
            this.btnEnrg.Select = false;
            this.btnEnrg.Click += new JDComponentLib.BoutonPlus.ClickedHandler(this.btnEnrg_Click);
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Actions);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabQualifier);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer3);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            resources.ApplyResources(this.splitContainer3, "splitContainer3");
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dgvDoublons);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.btnStats);
            this.splitContainer3.Panel2.Controls.Add(this.lbNbDoublons);
            // 
            // dgvDoublons
            // 
            this.dgvDoublons.AllowUserToAddRows = false;
            this.dgvDoublons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvDoublons, "dgvDoublons");
            this.dgvDoublons.Name = "dgvDoublons";
            this.dgvDoublons.ReadOnly = true;
            this.dgvDoublons.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoublons.DataSourceChanged += new System.EventHandler(this.DataSourceChanged);
            this.dgvDoublons.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemContentDoubleClick);
            this.dgvDoublons.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_CellFormatting);
            this.dgvDoublons.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvDoublons_UserDeletingRow);
            // 
            // btnStats
            // 
            this.btnStats.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStats.Image_active = global::duplicateFile.Resources.Resources.stats;
            this.btnStats.Image_activee = global::duplicateFile.Resources.Resources.stats;
            this.btnStats.Image_Desactivee = global::duplicateFile.Resources.Resources.stats;
            this.btnStats.Image_repos = global::duplicateFile.Resources.Resources.stats;
            this.btnStats.Image_Select = global::duplicateFile.Resources.Resources.stats;
            resources.ApplyResources(this.btnStats, "btnStats");
            this.btnStats.ModeBascule = false;
            this.btnStats.Mot_de_Passe = "280572";
            this.btnStats.Name = "btnStats";
            this.btnStats.Select = false;
            this.btnStats.Click += new JDComponentLib.BoutonPlus.ClickedHandler(this.btnStats_Click);
            // 
            // lbNbDoublons
            // 
            resources.ApplyResources(this.lbNbDoublons, "lbNbDoublons");
            this.lbNbDoublons.Name = "lbNbDoublons";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvChemin);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lbNbChemin);
            this.splitContainer2.Panel2.Controls.Add(this.lbNbCarac);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Panel2.Controls.Add(this.trackBar1);
            // 
            // dgvChemin
            // 
            this.dgvChemin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvChemin, "dgvChemin");
            this.dgvChemin.Name = "dgvChemin";
            this.dgvChemin.ReadOnly = true;
            this.dgvChemin.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChemin.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemContentDoubleClick);
            this.dgvChemin.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_CellFormatting);
            // 
            // lbNbChemin
            // 
            resources.ApplyResources(this.lbNbChemin, "lbNbChemin");
            this.lbNbChemin.Name = "lbNbChemin";
            // 
            // lbNbCarac
            // 
            resources.ApplyResources(this.lbNbCarac, "lbNbCarac");
            this.lbNbCarac.Name = "lbNbCarac";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.SystemColors.Info;
            resources.ApplyResources(this.trackBar1, "trackBar1");
            this.trackBar1.Maximum = 300;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 200;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvErreurs);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvErreurs
            // 
            this.dgvErreurs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            resources.ApplyResources(this.dgvErreurs, "dgvErreurs");
            this.dgvErreurs.Name = "dgvErreurs";
            this.dgvErreurs.ReadOnly = true;
            this.dgvErreurs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvErreurs.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemContentDoubleClick);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.splitContainer4);
            resources.ApplyResources(this.tabPage5, "tabPage5");
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            resources.ApplyResources(this.splitContainer4, "splitContainer4");
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.dgv2003);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.chkAttributs);
            this.splitContainer4.Panel2.Controls.Add(this.btnConvertir);
            this.splitContainer4.Panel2.Controls.Add(this.lbNb2003);
            // 
            // dgv2003
            // 
            this.dgv2003.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            resources.ApplyResources(this.dgv2003, "dgv2003");
            this.dgv2003.Name = "dgv2003";
            this.dgv2003.ReadOnly = true;
            this.dgv2003.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv2003.DataSourceChanged += new System.EventHandler(this.DataSourceChanged);
            this.dgv2003.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_CellFormatting);
            // 
            // chkAttributs
            // 
            resources.ApplyResources(this.chkAttributs, "chkAttributs");
            this.chkAttributs.Checked = true;
            this.chkAttributs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAttributs.Name = "chkAttributs";
            this.chkAttributs.UseVisualStyleBackColor = true;
            // 
            // btnConvertir
            // 
            resources.ApplyResources(this.btnConvertir, "btnConvertir");
            this.btnConvertir.Name = "btnConvertir";
            this.btnConvertir.UseVisualStyleBackColor = true;
            this.btnConvertir.Click += new System.EventHandler(this.btnConvertir_Click);
            // 
            // lbNb2003
            // 
            resources.ApplyResources(this.lbNb2003, "lbNb2003");
            this.lbNb2003.Name = "lbNb2003";
            // 
            // tabQualifier
            // 
            this.tabQualifier.Controls.Add(this.splitQualifier);
            resources.ApplyResources(this.tabQualifier, "tabQualifier");
            this.tabQualifier.Name = "tabQualifier";
            this.tabQualifier.UseVisualStyleBackColor = true;
            // 
            // splitQualifier
            // 
            resources.ApplyResources(this.splitQualifier, "splitQualifier");
            this.splitQualifier.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitQualifier.Name = "splitQualifier";
            // 
            // splitQualifier.Panel1
            // 
            this.splitQualifier.Panel1.Controls.Add(this.dgvQualifier);
            // 
            // splitQualifier.Panel2
            // 
            this.splitQualifier.Panel2.Controls.Add(this.txtQualPath);
            this.splitQualifier.Panel2.Controls.Add(this.label2);
            this.splitQualifier.Panel2.Controls.Add(this.lblNbQualifier);
            this.splitQualifier.Panel2.Controls.Add(this.lblLevelQualifier);
            this.splitQualifier.Panel2.Controls.Add(this.label3);
            this.splitQualifier.Panel2.Controls.Add(this.trackQualifier);
            // 
            // dgvQualifier
            // 
            this.dgvQualifier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            resources.ApplyResources(this.dgvQualifier, "dgvQualifier");
            this.dgvQualifier.Name = "dgvQualifier";
            this.dgvQualifier.ReadOnly = true;
            this.dgvQualifier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // txtQualPath
            // 
            resources.ApplyResources(this.txtQualPath, "txtQualPath");
            this.txtQualPath.Name = "txtQualPath";
            this.txtQualPath.TextChanged += new System.EventHandler(this.txtQualPath_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lblNbQualifier
            // 
            resources.ApplyResources(this.lblNbQualifier, "lblNbQualifier");
            this.lblNbQualifier.Name = "lblNbQualifier";
            // 
            // lblLevelQualifier
            // 
            resources.ApplyResources(this.lblLevelQualifier, "lblLevelQualifier");
            this.lblLevelQualifier.Name = "lblLevelQualifier";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // trackQualifier
            // 
            this.trackQualifier.BackColor = System.Drawing.SystemColors.Info;
            resources.ApplyResources(this.trackQualifier, "trackQualifier");
            this.trackQualifier.Maximum = 150;
            this.trackQualifier.Minimum = 1;
            this.trackQualifier.Name = "trackQualifier";
            this.trackQualifier.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackQualifier.Value = 30;
            this.trackQualifier.ValueChanged += new System.EventHandler(this.trackQualifier_ValueChanged);
            // 
            // timer
            // 
            this.timer.Interval = 2000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.Actions.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoublons)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChemin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvErreurs)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv2003)).EndInit();
            this.tabQualifier.ResumeLayout(false);
            this.splitQualifier.Panel1.ResumeLayout(false);
            this.splitQualifier.Panel2.ResumeLayout(false);
            this.splitQualifier.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitQualifier)).EndInit();
            this.splitQualifier.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQualifier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackQualifier)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAnalyse;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox Actions;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvDoublons;
        private System.Windows.Forms.DataGridView dgvChemin;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvErreurs;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label lbNbCarac;
        private System.Windows.Forms.Label lbNbChemin;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label lbNbDoublons;
        private JDComponentLib.BoutonPlus btnOpen;
        private JDComponentLib.BoutonPlus btnOpenFolder;
        private JDComponentLib.BoutonPlus btnSupp;
        private JDComponentLib.BoutonPlus btnExport;
        private JDComponentLib.BoutonPlus btnEnrg;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView dgv2003;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label lbNb2003;
        private JDComponentLib.BoutonPlus btnCorr;
        private System.Windows.Forms.Button btnConvertir;
        private System.Windows.Forms.CheckBox chkAttributs;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private JDComponentLib.BoutonPlus btnStats;
        private JDComponentLib.BoutonPlus btnGraph;
        private JDComponentLib.BoutonPlus btnHash;
        private JDComponentLib.BoutonPlus btnJobs;
        private JDComponentLib.BoutonPlus btnConfig;
        private JDComponentLib.BoutonPlus btnUser;
        private System.Windows.Forms.TabPage tabQualifier;
        private System.Windows.Forms.DataGridView dgvQualifier;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public JDComponentLib.BoutonPlus btnCharge;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.SplitContainer splitQualifier;
        private System.Windows.Forms.Label lblLevelQualifier;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackQualifier;
        private System.Windows.Forms.Label lblNbQualifier;
        private System.Windows.Forms.TextBox txtQualPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer;
    }
}

