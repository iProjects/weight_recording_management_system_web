namespace weight_recording_ui
{
    partial class aboutform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(aboutform));
            this.aboutwebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // aboutwebBrowser
            // 
            this.aboutwebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aboutwebBrowser.Location = new System.Drawing.Point(0, 0);
            this.aboutwebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.aboutwebBrowser.Name = "aboutwebBrowser";
            this.aboutwebBrowser.ScrollBarsEnabled = false;
            this.aboutwebBrowser.Size = new System.Drawing.Size(520, 374);
            this.aboutwebBrowser.TabIndex = 0;
            // 
            // aboutform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 374);
            this.Controls.Add(this.aboutwebBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "aboutform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "about";
            this.Load += new System.EventHandler(this.aboutform_Load);
            this.Leave += new System.EventHandler(this.aboutform_Leave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser aboutwebBrowser;
    }
}