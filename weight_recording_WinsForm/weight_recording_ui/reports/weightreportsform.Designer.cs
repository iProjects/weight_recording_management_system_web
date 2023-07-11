namespace weight_recording_ui.reports
{
    partial class weightreportsform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(weightreportsform));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnpdfreport = new System.Windows.Forms.Button();
            this.btnchartreport = new System.Windows.Forms.Button();
            this.btncrystalreport = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtlog = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnpdfreport);
            this.groupBox1.Controls.Add(this.btnchartreport);
            this.groupBox1.Controls.Add(this.btncrystalreport);
            this.groupBox1.Controls.Add(this.btnclose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnpdfreport
            // 
            this.btnpdfreport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnpdfreport.Image = ((System.Drawing.Image)(resources.GetObject("btnpdfreport.Image")));
            this.btnpdfreport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnpdfreport.Location = new System.Drawing.Point(12, 14);
            this.btnpdfreport.Name = "btnpdfreport";
            this.btnpdfreport.Size = new System.Drawing.Size(129, 23);
            this.btnpdfreport.TabIndex = 0;
            this.btnpdfreport.Text = "pdf report";
            this.btnpdfreport.UseVisualStyleBackColor = true;
            this.btnpdfreport.Click += new System.EventHandler(this.btnpdfreport_Click);
            // 
            // btnchartreport
            // 
            this.btnchartreport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnchartreport.Image = ((System.Drawing.Image)(resources.GetObject("btnchartreport.Image")));
            this.btnchartreport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnchartreport.Location = new System.Drawing.Point(147, 14);
            this.btnchartreport.Name = "btnchartreport";
            this.btnchartreport.Size = new System.Drawing.Size(129, 23);
            this.btnchartreport.TabIndex = 1;
            this.btnchartreport.Text = "chart report";
            this.btnchartreport.UseVisualStyleBackColor = true;
            this.btnchartreport.Click += new System.EventHandler(this.btnchartreport_Click);
            // 
            // btncrystalreport
            // 
            this.btncrystalreport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncrystalreport.Image = ((System.Drawing.Image)(resources.GetObject("btncrystalreport.Image")));
            this.btncrystalreport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btncrystalreport.Location = new System.Drawing.Point(282, 14);
            this.btncrystalreport.Name = "btncrystalreport";
            this.btncrystalreport.Size = new System.Drawing.Size(144, 23);
            this.btncrystalreport.TabIndex = 2;
            this.btncrystalreport.Text = "crystal report";
            this.btncrystalreport.UseVisualStyleBackColor = true;
            this.btncrystalreport.Click += new System.EventHandler(this.btncrystalreport_Click);
            // 
            // btnclose
            // 
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Image = ((System.Drawing.Image)(resources.GetObject("btnclose.Image")));
            this.btnclose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnclose.Location = new System.Drawing.Point(432, 14);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(90, 23);
            this.btnclose.TabIndex = 3;
            this.btnclose.Text = "close";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtlog);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(545, 242);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // txtlog
            // 
            this.txtlog.BackColor = System.Drawing.Color.Black;
            this.txtlog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtlog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtlog.ForeColor = System.Drawing.Color.Lime;
            this.txtlog.Location = new System.Drawing.Point(3, 16);
            this.txtlog.Multiline = true;
            this.txtlog.Name = "txtlog";
            this.txtlog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtlog.Size = new System.Drawing.Size(539, 223);
            this.txtlog.TabIndex = 0;
            // 
            // weightreportsform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(545, 293);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "weightreportsform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "print weight reports";
            this.Load += new System.EventHandler(this.weightreportsform_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnpdfreport;
        private System.Windows.Forms.Button btnchartreport;
        private System.Windows.Forms.Button btncrystalreport;
        private System.Windows.Forms.TextBox txtlog;
    }
}