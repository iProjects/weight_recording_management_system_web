namespace weight_recording_ui.reports
{
    partial class pdfviewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pdfviewer));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.weightspdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelreportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblstatusinfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbltimelapsed = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtlog = new System.Windows.Forms.RichTextBox();
            this.pdfwebBrowser = new System.Windows.Forms.WebBrowser();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.weightspdfToolStripMenuItem,
            this.excelreportsToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1077, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fileToolStripMenuItem.Image")));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fileToolStripMenuItem.Text = "exit";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // weightspdfToolStripMenuItem
            // 
            this.weightspdfToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("weightspdfToolStripMenuItem.Image")));
            this.weightspdfToolStripMenuItem.Name = "weightspdfToolStripMenuItem";
            this.weightspdfToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.weightspdfToolStripMenuItem.Text = "pdf reports";
            this.weightspdfToolStripMenuItem.Click += new System.EventHandler(this.weightspdfToolStripMenuItem_Click);
            // 
            // excelreportsToolStripMenuItem
            // 
            this.excelreportsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("excelreportsToolStripMenuItem.Image")));
            this.excelreportsToolStripMenuItem.Name = "excelreportsToolStripMenuItem";
            this.excelreportsToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.excelreportsToolStripMenuItem.Text = "excel reports";
            this.excelreportsToolStripMenuItem.Click += new System.EventHandler(this.weightsExcelToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblstatusinfo,
            this.toolStripStatusLabel1,
            this.lbltimelapsed});
            this.statusStrip1.Location = new System.Drawing.Point(0, 647);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1077, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblstatusinfo
            // 
            this.lblstatusinfo.BackColor = System.Drawing.Color.Black;
            this.lblstatusinfo.DoubleClickEnabled = true;
            this.lblstatusinfo.ForeColor = System.Drawing.Color.Lime;
            this.lblstatusinfo.Name = "lblstatusinfo";
            this.lblstatusinfo.Size = new System.Drawing.Size(72, 17);
            this.lblstatusinfo.Text = "lblstatusinfo";
            this.lblstatusinfo.Click += new System.EventHandler(this.lblstatusinfo_Click);
            this.lblstatusinfo.DoubleClick += new System.EventHandler(this.lblstatusinfo_DoubleClick);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(67, 17);
            this.toolStripStatusLabel1.Text = "                    ";
            // 
            // lbltimelapsed
            // 
            this.lbltimelapsed.BackColor = System.Drawing.Color.Black;
            this.lbltimelapsed.ForeColor = System.Drawing.Color.Lime;
            this.lbltimelapsed.Name = "lbltimelapsed";
            this.lbltimelapsed.Size = new System.Drawing.Size(78, 17);
            this.lbltimelapsed.Text = "lbltimelapsed";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtlog);
            this.splitContainer1.Panel1MinSize = 15;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pdfwebBrowser);
            this.splitContainer1.Size = new System.Drawing.Size(1077, 623);
            this.splitContainer1.SplitterDistance = 201;
            this.splitContainer1.TabIndex = 2;
            // 
            // txtlog
            // 
            this.txtlog.BackColor = System.Drawing.Color.Black;
            this.txtlog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtlog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtlog.ForeColor = System.Drawing.Color.Lime;
            this.txtlog.Location = new System.Drawing.Point(0, 0);
            this.txtlog.Name = "txtlog";
            this.txtlog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtlog.Size = new System.Drawing.Size(201, 623);
            this.txtlog.TabIndex = 0;
            this.txtlog.Text = "";
            // 
            // pdfwebBrowser
            // 
            this.pdfwebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfwebBrowser.Location = new System.Drawing.Point(0, 0);
            this.pdfwebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.pdfwebBrowser.Name = "pdfwebBrowser";
            this.pdfwebBrowser.Size = new System.Drawing.Size(872, 623);
            this.pdfwebBrowser.TabIndex = 0;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.aboutToolStripMenuItem.Text = "about";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // pdfviewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 669);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "pdfviewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pdfviewer";
            this.Load += new System.EventHandler(this.pdfviewer_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblstatusinfo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser pdfwebBrowser;
        private System.Windows.Forms.ToolStripMenuItem weightspdfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelreportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lbltimelapsed;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.RichTextBox txtlog;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}