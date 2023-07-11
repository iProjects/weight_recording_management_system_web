namespace weight_recording_ui.reports
{
    partial class chartform
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(chartform));
            this.weightchart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnloadchart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbocharttype = new System.Windows.Forms.ComboBox();
            this.btnclose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.weightchart)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // weightchart
            // 
            chartArea1.Name = "ChartArea1";
            this.weightchart.ChartAreas.Add(chartArea1);
            this.weightchart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.weightchart.Legends.Add(legend1);
            this.weightchart.Location = new System.Drawing.Point(3, 16);
            this.weightchart.Name = "weightchart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.weightchart.Series.Add(series1);
            this.weightchart.Size = new System.Drawing.Size(1333, 667);
            this.weightchart.TabIndex = 0;
            this.weightchart.Text = "weight chart";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnloadchart);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbocharttype);
            this.groupBox1.Controls.Add(this.btnclose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1339, 58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnloadchart
            // 
            this.btnloadchart.Image = ((System.Drawing.Image)(resources.GetObject("btnloadchart.Image")));
            this.btnloadchart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnloadchart.Location = new System.Drawing.Point(25, 19);
            this.btnloadchart.Name = "btnloadchart";
            this.btnloadchart.Size = new System.Drawing.Size(161, 23);
            this.btnloadchart.TabIndex = 4;
            this.btnloadchart.Text = "load chart";
            this.btnloadchart.UseVisualStyleBackColor = true;
            this.btnloadchart.Click += new System.EventHandler(this.btnloadchart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(218, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "chart type";
            // 
            // cbocharttype
            // 
            this.cbocharttype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbocharttype.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbocharttype.FormattingEnabled = true;
            this.cbocharttype.Location = new System.Drawing.Point(278, 21);
            this.cbocharttype.Name = "cbocharttype";
            this.cbocharttype.Size = new System.Drawing.Size(119, 21);
            this.cbocharttype.TabIndex = 2;
            this.cbocharttype.SelectedIndexChanged += new System.EventHandler(this.cbocharttype_SelectedIndexChanged);
            // 
            // btnclose
            // 
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.Image = ((System.Drawing.Image)(resources.GetObject("btnclose.Image")));
            this.btnclose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnclose.Location = new System.Drawing.Point(433, 24);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(86, 23);
            this.btnclose.TabIndex = 1;
            this.btnclose.Text = "close";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.weightchart);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 58);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1339, 686);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // chartform
            // 
            this.AcceptButton = this.btnloadchart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(1339, 744);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "chartform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "chartform";
            this.Load += new System.EventHandler(this.chartform_Load);
            ((System.ComponentModel.ISupportInitialize)(this.weightchart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart weightchart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbocharttype;
        private System.Windows.Forms.Button btnloadchart;
    }
}