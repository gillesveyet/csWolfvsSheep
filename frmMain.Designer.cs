namespace WolfvsSheep
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnuOptions = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevel1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevel2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLevel3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSinglePlayer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTwoPlayers = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPlayWolf = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPlaySheep = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBack = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mnuQuit = new System.Windows.Forms.ToolStripButton();
            this.mnuNew = new System.Windows.Forms.ToolStripButton();
            this.mnuHelp = new System.Windows.Forms.ToolStripButton();
            this.mnuDiagnostic = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuSpecialStart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPerformanceTest = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLetComputerPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.panelChecker = new WolfvsSheep.CheckerPanel();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 631);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(600, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // mnuOptions
            // 
            this.mnuOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLevel,
            this.toolStripSeparator1,
            this.mnuSinglePlayer,
            this.mnuTwoPlayers,
            this.toolStripSeparator2,
            this.mnuPlayWolf,
            this.mnuPlaySheep});
            this.mnuOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuOptions.Image")));
            this.mnuOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOptions.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(62, 19);
            this.mnuOptions.Text = "Options";
            // 
            // mnuLevel
            // 
            this.mnuLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLevel1,
            this.mnuLevel2,
            this.mnuLevel3});
            this.mnuLevel.Name = "mnuLevel";
            this.mnuLevel.Size = new System.Drawing.Size(172, 22);
            this.mnuLevel.Text = "Niveau PC";
            // 
            // mnuLevel1
            // 
            this.mnuLevel1.CheckOnClick = true;
            this.mnuLevel1.Name = "mnuLevel1";
            this.mnuLevel1.Size = new System.Drawing.Size(111, 22);
            this.mnuLevel1.Text = "Faible";
            this.mnuLevel1.Click += new System.EventHandler(this.mnuLevel_Click);
            // 
            // mnuLevel2
            // 
            this.mnuLevel2.CheckOnClick = true;
            this.mnuLevel2.Name = "mnuLevel2";
            this.mnuLevel2.Size = new System.Drawing.Size(111, 22);
            this.mnuLevel2.Text = "Moyen";
            this.mnuLevel2.Click += new System.EventHandler(this.mnuLevel_Click);
            // 
            // mnuLevel3
            // 
            this.mnuLevel3.CheckOnClick = true;
            this.mnuLevel3.Name = "mnuLevel3";
            this.mnuLevel3.Size = new System.Drawing.Size(111, 22);
            this.mnuLevel3.Text = "Expert";
            this.mnuLevel3.Click += new System.EventHandler(this.mnuLevel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // mnuSinglePlayer
            // 
            this.mnuSinglePlayer.CheckOnClick = true;
            this.mnuSinglePlayer.Name = "mnuSinglePlayer";
            this.mnuSinglePlayer.Size = new System.Drawing.Size(172, 22);
            this.mnuSinglePlayer.Text = "1 joueur contre PC";
            this.mnuSinglePlayer.Click += new System.EventHandler(this.mnuSinglePlayer_Click);
            // 
            // mnuTwoPlayers
            // 
            this.mnuTwoPlayers.CheckOnClick = true;
            this.mnuTwoPlayers.Name = "mnuTwoPlayers";
            this.mnuTwoPlayers.Size = new System.Drawing.Size(172, 22);
            this.mnuTwoPlayers.Text = "2 joueurs";
            this.mnuTwoPlayers.Click += new System.EventHandler(this.mnuTwoPlayers_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(169, 6);
            // 
            // mnuPlayWolf
            // 
            this.mnuPlayWolf.CheckOnClick = true;
            this.mnuPlayWolf.Name = "mnuPlayWolf";
            this.mnuPlayWolf.Size = new System.Drawing.Size(172, 22);
            this.mnuPlayWolf.Text = "Jouer le loup";
            this.mnuPlayWolf.Click += new System.EventHandler(this.mnuPlayWolf_Click);
            // 
            // mnuPlaySheep
            // 
            this.mnuPlaySheep.CheckOnClick = true;
            this.mnuPlaySheep.Name = "mnuPlaySheep";
            this.mnuPlaySheep.Size = new System.Drawing.Size(172, 22);
            this.mnuPlaySheep.Text = "Jouer les agneaux";
            this.mnuPlaySheep.Click += new System.EventHandler(this.mnuPlaySheep_Click);
            // 
            // mnuBack
            // 
            this.mnuBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuBack.Image = ((System.Drawing.Image)(resources.GetObject("mnuBack.Image")));
            this.mnuBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuBack.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.mnuBack.Name = "mnuBack";
            this.mnuBack.Size = new System.Drawing.Size(76, 19);
            this.mnuBack.Text = "Annuler [F3]";
            this.mnuBack.ToolTipText = "Annuler dernier coup joué";
            this.mnuBack.Click += new System.EventHandler(this.mnuBack_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQuit,
            this.mnuOptions,
            this.mnuNew,
            this.mnuBack,
            this.mnuHelp,
            this.mnuDiagnostic});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(600, 22);
            this.toolStrip1.TabIndex = 18;
            // 
            // mnuQuit
            // 
            this.mnuQuit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuQuit.Image = ((System.Drawing.Image)(resources.GetObject("mnuQuit.Image")));
            this.mnuQuit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuQuit.Name = "mnuQuit";
            this.mnuQuit.Size = new System.Drawing.Size(48, 19);
            this.mnuQuit.Text = "Quitter";
            this.mnuQuit.Click += new System.EventHandler(this.mnuQuit_Click);
            // 
            // mnuNew
            // 
            this.mnuNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuNew.Image = ((System.Drawing.Image)(resources.GetObject("mnuNew.Image")));
            this.mnuNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuNew.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.mnuNew.Name = "mnuNew";
            this.mnuNew.Size = new System.Drawing.Size(82, 19);
            this.mnuNew.Text = "Nouveau [F2]";
            this.mnuNew.ToolTipText = "Nouvelle partie";
            this.mnuNew.Click += new System.EventHandler(this.mnuNew_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuHelp.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelp.Image")));
            this.mnuHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(23, 19);
            this.mnuHelp.Text = "?";
            this.mnuHelp.Click += new System.EventHandler(this.mnuHelp_Click);
            // 
            // mnuDiagnostic
            // 
            this.mnuDiagnostic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuDiagnostic.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSpecialStart,
            this.mnuPerformanceTest,
            this.mnuLetComputerPlay});
            this.mnuDiagnostic.Image = ((System.Drawing.Image)(resources.GetObject("mnuDiagnostic.Image")));
            this.mnuDiagnostic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuDiagnostic.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.mnuDiagnostic.Name = "mnuDiagnostic";
            this.mnuDiagnostic.Size = new System.Drawing.Size(76, 19);
            this.mnuDiagnostic.Text = "Diagnostic";
            // 
            // mnuSpecialStart
            // 
            this.mnuSpecialStart.CheckOnClick = true;
            this.mnuSpecialStart.Name = "mnuSpecialStart";
            this.mnuSpecialStart.Size = new System.Drawing.Size(172, 22);
            this.mnuSpecialStart.Text = "Special Start";
            this.mnuSpecialStart.Click += new System.EventHandler(this.mnuSpecialStart_Click);
            // 
            // mnuPerformanceTest
            // 
            this.mnuPerformanceTest.Name = "mnuPerformanceTest";
            this.mnuPerformanceTest.Size = new System.Drawing.Size(172, 22);
            this.mnuPerformanceTest.Text = "Performance Test";
            this.mnuPerformanceTest.Click += new System.EventHandler(this.mnuPerformanceTest_Click);
            // 
            // mnuLetComputerPlay
            // 
            this.mnuLetComputerPlay.Name = "mnuLetComputerPlay";
            this.mnuLetComputerPlay.Size = new System.Drawing.Size(172, 22);
            this.mnuLetComputerPlay.Text = "Let Computer Play";
            this.mnuLetComputerPlay.Click += new System.EventHandler(this.mnuLetComputerPlay_Click);
            // 
            // panelChecker
            // 
            this.panelChecker.Location = new System.Drawing.Point(0, 28);
            this.panelChecker.Name = "panelChecker";
            this.panelChecker.Size = new System.Drawing.Size(600, 600);
            this.panelChecker.TabIndex = 0;
            this.panelChecker.SelectPiece += new WolfvsSheep.CheckerPanel.SelectPieceHandler(this.panelChecker_SelectPiece);
            this.panelChecker.MovePiece += new WolfvsSheep.CheckerPanel.MovePieceHandler(this.panelChecker_MovePiece);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(600, 653);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelChecker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Le loup et les agneaux";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMain_KeyPress);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckerPanel panelChecker;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripDropDownButton mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuLevel;
        private System.Windows.Forms.ToolStripMenuItem mnuLevel1;
        private System.Windows.Forms.ToolStripMenuItem mnuLevel2;
        private System.Windows.Forms.ToolStripMenuItem mnuLevel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuSinglePlayer;
        private System.Windows.Forms.ToolStripMenuItem mnuTwoPlayers;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuPlayWolf;
        private System.Windows.Forms.ToolStripMenuItem mnuPlaySheep;
        private System.Windows.Forms.ToolStripButton mnuBack;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton mnuNew;
        private System.Windows.Forms.ToolStripButton mnuQuit;
        private System.Windows.Forms.ToolStripButton mnuHelp;
        private System.Windows.Forms.ToolStripDropDownButton mnuDiagnostic;
        private System.Windows.Forms.ToolStripMenuItem mnuSpecialStart;
        private System.Windows.Forms.ToolStripMenuItem mnuPerformanceTest;
        private System.Windows.Forms.ToolStripMenuItem mnuLetComputerPlay;
    }
}

