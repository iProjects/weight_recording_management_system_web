using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using weight_recording_dal;
using weight_recording_ui.reports;

namespace weight_recording_ui
{
    public partial class aboutform : Form
    {
        public aboutform()
        {
            InitializeComponent();
        }

        private void aboutform_Load(object sender, EventArgs e)
        {
            navigate_to_about_page();
        }
        private void navigate_to_about_page()
        {
            try
            {
                string dir = pathlookup("about");
                string sPath = Utils.build_file_path(dir, "about.html");

                for (int i = 0; i < 6; i++)
                {
                    if (VVX.File.Exists(sPath))
                    {
                        FileInfo fi = new FileInfo(sPath);
                        this.aboutwebBrowser.Navigate(fi.FullName);
                        break;
                    }
                    sPath = @"..\" + sPath;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        public string pathlookup(string folder)
        {
            try
            {
                string app_dir = Utils.get_application_path();
                var dir = Path.Combine(app_dir, folder);


                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                return dir;

            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }

        private void aboutform_Leave(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
