using CrystalDecisions.CrystalReports.Engine;
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

namespace weight_recording_ui.reports
{
    public partial class crystal_report_form : Form
    {
        public string TAG;
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
		
        public crystal_report_form(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            InitializeComponent();
            TAG = this.GetType().Name;

            _notificationmessageEventname = notificationmessageEventname;
        }

        private void crystal_report_form_Load(object sender, EventArgs e)
        {
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("loaded crystal_report_form", TAG));
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            btnprint.Text = "printing...";
            print_report();
            btnprint.Text = "print";
        }
        private void print_report()
        {
            try
            {
                DataSet dataset = new DataSet();
                dataset = new weightsDataSet();
                DataTable weightsDataTable = dataset.Tables["weightsDataTable"];
                DataRow row;
                List<weight_ui_dto> lst_records = dalutilz.getallweightsforui();
                int i;
                int count = 0;
				
                for (i = 0; i < lst_records.Count(); i++)
                {
                    row = weightsDataTable.NewRow();
					
                    row["Id"] = lst_records[i].weight_id;
                    row["Weight"] = lst_records[i].weight_weight;
                    row["Date"] = lst_records[i].weight_date;
                    row["Status"] = lst_records[i].weight_status;
                    row["Created Date"] = lst_records[i].created_date;
                    row["App"] = lst_records[i].weight_app;

                    weightsDataTable.Rows.Add(row);
                    count++;
                }

                groupBox2.Text = count.ToString();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + count.ToString() + " ] records for printing...", TAG));

                ReportDocument cryRpt = new ReportDocument();
                string datetime = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss_tt");
                string file_name = "Crystal_Report_" + datetime + ".rpt";
                string report_path = Path.Combine(Utils.get_application_path(), "reports");
                string report_file = Path.Combine(report_path, "CrystalReport.rpt");

                if (!File.Exists(report_file))
                    File.Create(report_file).Close();

                cryRpt.Load(report_file);
                cryRpt.SetDataSource(weightsDataTable);
                crystalReportViewer.ReportSource = cryRpt;
                crystalReportViewer.Refresh();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("created crystal report sucessfully...", TAG));

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }


    }
}
