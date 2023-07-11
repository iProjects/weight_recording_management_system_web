namespace weight_recording_ui
{
    partial class listweightform
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(listweightform));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnclearfilter = new System.Windows.Forms.Button();
            this.dtpdateto = new System.Windows.Forms.DateTimePicker();
            this.dtpdatefrom = new System.Windows.Forms.DateTimePicker();
            this.cbo_filter_by_app = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnfilter = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnedit = new System.Windows.Forms.Button();
            this.btnadd = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewweights = new System.Windows.Forms.DataGridView();
            this.Columnweight_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columnweight_weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columnweight_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columncreated_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columnweight_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourceweights = new System.Windows.Forms.BindingSource(this.components);
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewweights)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceweights)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnclearfilter);
            this.groupBox1.Controls.Add(this.dtpdateto);
            this.groupBox1.Controls.Add(this.dtpdatefrom);
            this.groupBox1.Controls.Add(this.cbo_filter_by_app);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnfilter);
            this.groupBox1.Controls.Add(this.btnclose);
            this.groupBox1.Controls.Add(this.btnprint);
            this.groupBox1.Controls.Add(this.btndelete);
            this.groupBox1.Controls.Add(this.btnedit);
            this.groupBox1.Controls.Add(this.btnadd);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(887, 86);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnclearfilter
            // 
            this.btnclearfilter.Image = ((System.Drawing.Image)(resources.GetObject("btnclearfilter.Image")));
            this.btnclearfilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnclearfilter.Location = new System.Drawing.Point(139, 52);
            this.btnclearfilter.Name = "btnclearfilter";
            this.btnclearfilter.Size = new System.Drawing.Size(92, 23);
            this.btnclearfilter.TabIndex = 12;
            this.btnclearfilter.Text = "clear filter";
            this.btnclearfilter.UseVisualStyleBackColor = true;
            this.btnclearfilter.Click += new System.EventHandler(this.btnclearfilter_Click);
            // 
            // dtpdateto
            // 
            this.dtpdateto.Location = new System.Drawing.Point(604, 18);
            this.dtpdateto.Name = "dtpdateto";
            this.dtpdateto.Size = new System.Drawing.Size(200, 20);
            this.dtpdateto.TabIndex = 7;
            this.dtpdateto.ValueChanged += new System.EventHandler(this.dtpdateto_ValueChanged);
            // 
            // dtpdatefrom
            // 
            this.dtpdatefrom.Location = new System.Drawing.Point(321, 17);
            this.dtpdatefrom.Name = "dtpdatefrom";
            this.dtpdatefrom.Size = new System.Drawing.Size(200, 20);
            this.dtpdatefrom.TabIndex = 6;
            this.dtpdatefrom.ValueChanged += new System.EventHandler(this.dtpdatefrom_ValueChanged);
            // 
            // cbofilterbyname
            // 
            this.cbo_filter_by_app.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_filter_by_app.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_filter_by_app.FormattingEnabled = true;
            this.cbo_filter_by_app.Location = new System.Drawing.Point(85, 17);
            this.cbo_filter_by_app.Name = "cbofilterbyname";
            this.cbo_filter_by_app.Size = new System.Drawing.Size(162, 21);
            this.cbo_filter_by_app.TabIndex = 5;
            this.cbo_filter_by_app.SelectedIndexChanged += new System.EventHandler(this.cbofilterbyname_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(264, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "date from";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(558, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "date to";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "filter by name";
            // 
            // btnfilter
            // 
            this.btnfilter.Image = ((System.Drawing.Image)(resources.GetObject("btnfilter.Image")));
            this.btnfilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnfilter.Location = new System.Drawing.Point(27, 52);
            this.btnfilter.Name = "btnfilter";
            this.btnfilter.Size = new System.Drawing.Size(106, 23);
            this.btnfilter.TabIndex = 8;
            this.btnfilter.Text = "filter";
            this.btnfilter.UseVisualStyleBackColor = true;
            this.btnfilter.Click += new System.EventHandler(this.btnfilter_Click);
            // 
            // btnclose
            // 
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.Image = ((System.Drawing.Image)(resources.GetObject("btnclose.Image")));
            this.btnclose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnclose.Location = new System.Drawing.Point(665, 52);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(75, 24);
            this.btnclose.TabIndex = 4;
            this.btnclose.Text = "close";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnprint
            // 
            this.btnprint.Image = ((System.Drawing.Image)(resources.GetObject("btnprint.Image")));
            this.btnprint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnprint.Location = new System.Drawing.Point(558, 52);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(101, 23);
            this.btnprint.TabIndex = 3;
            this.btnprint.Text = "print";
            this.btnprint.UseVisualStyleBackColor = true;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // btndelete
            // 
            this.btndelete.Image = ((System.Drawing.Image)(resources.GetObject("btndelete.Image")));
            this.btndelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btndelete.Location = new System.Drawing.Point(446, 52);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(101, 23);
            this.btndelete.TabIndex = 2;
            this.btndelete.Text = "delete";
            this.btndelete.UseVisualStyleBackColor = true;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // btnedit
            // 
            this.btnedit.Image = ((System.Drawing.Image)(resources.GetObject("btnedit.Image")));
            this.btnedit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnedit.Location = new System.Drawing.Point(361, 52);
            this.btnedit.Name = "btnedit";
            this.btnedit.Size = new System.Drawing.Size(75, 23);
            this.btnedit.TabIndex = 1;
            this.btnedit.Text = "edit";
            this.btnedit.UseVisualStyleBackColor = true;
            this.btnedit.Click += new System.EventHandler(this.btnedit_Click);
            // 
            // btnadd
            // 
            this.btnadd.Image = ((System.Drawing.Image)(resources.GetObject("btnadd.Image")));
            this.btnadd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnadd.Location = new System.Drawing.Point(272, 52);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(75, 23);
            this.btnadd.TabIndex = 0;
            this.btnadd.Text = "add";
            this.btnadd.UseVisualStyleBackColor = true;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewweights);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 86);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(887, 344);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // dataGridViewweight
            // 
            this.dataGridViewweights.AllowUserToAddRows = false;
            this.dataGridViewweights.AllowUserToDeleteRows = false;
            this.dataGridViewweights.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewweights.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Columnweight_id,
            this.Columnweight_weight,
            this.Columnweight_date,
            this.Columncreated_date,
            this.Columnweight_status});
            this.dataGridViewweights.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewweights.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewweights.Name = "dataGridViewweight";
            this.dataGridViewweights.ReadOnly = true;
            this.dataGridViewweights.Size = new System.Drawing.Size(881, 325);
            this.dataGridViewweights.TabIndex = 0;
            this.dataGridViewweights.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewweights_CellContentDoubleClick);
            // 
            // Columnweight_id
            // 
            this.Columnweight_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Columnweight_id.DataPropertyName = "weight_id";
            this.Columnweight_id.HeaderText = "id";
            this.Columnweight_id.Name = "Columnweight_id";
            this.Columnweight_id.ReadOnly = true;
            this.Columnweight_id.Width = 50;
            // 
            // Columnweight_weight
            // 
            this.Columnweight_weight.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Columnweight_weight.DataPropertyName = "weight_weight";
            this.Columnweight_weight.HeaderText = "weight";
            this.Columnweight_weight.Name = "Columnweight_weight";
            this.Columnweight_weight.ReadOnly = true;
            this.Columnweight_weight.Width = 200;
            // 
            // Columnweight_date
            // 
            this.Columnweight_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Columnweight_date.DataPropertyName = "weight_date";
            this.Columnweight_date.HeaderText = "date";
            this.Columnweight_date.Name = "Columnweight_date";
            this.Columnweight_date.ReadOnly = true;
            this.Columnweight_date.Width = 200;
            // 
            // Columncreated_date
            // 
            this.Columncreated_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Columncreated_date.DataPropertyName = "created_date";
            this.Columncreated_date.HeaderText = "created date";
            this.Columncreated_date.Name = "Columncreated_date";
            this.Columncreated_date.ReadOnly = true;
            this.Columncreated_date.Width = 200;
            // 
            // Columnweight_status
            // 
            this.Columnweight_status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Columnweight_status.DataPropertyName = "weight_status";
            this.Columnweight_status.HeaderText = "status";
            this.Columnweight_status.Name = "Columnweight_status";
            this.Columnweight_status.ReadOnly = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // listweightform
            // 
            this.AcceptButton = this.btnadd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(887, 430);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "listweightform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "weight recording";
            this.Load += new System.EventHandler(this.listweightform_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewweights)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceweights)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.BindingSource bindingSourceweights;
        private System.Windows.Forms.DataGridView dataGridViewweights;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnedit;
        private System.Windows.Forms.Button btnadd;
        private System.Windows.Forms.DateTimePicker dtpdateto;
        private System.Windows.Forms.DateTimePicker dtpdatefrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnfilter;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columnweight_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columnweight_weight;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columnweight_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columncreated_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columnweight_status;
        private System.Windows.Forms.Button btnclearfilter;
        private System.Windows.Forms.ComboBox cbo_filter_by_app;
        private System.Windows.Forms.Label label1;
    }
}

