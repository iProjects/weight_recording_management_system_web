using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using weight_recording_dal;
using weight_recording_ui.reports;

namespace weight_recording_ui
{
    public partial class addweightform : Form
    {
        public string TAG;
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        public addweightform(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            InitializeComponent();
            _notificationmessageEventname = notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished addweightform initialization", TAG));

        }

        private void addweightform_Load(object sender, EventArgs e)
        {
            txtweight.Text = string.Empty;
            dtpweight.Value = DateTime.Now;

            txtweight.Text = get_random_weight();

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished addweightform load", TAG));

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            btnadd.Text = "creating record..";
            try
            {
                string weight = "";
                string date = "";
                string msg = "";

                if (!string.IsNullOrEmpty(txtweight.Text))
                {
                    weight = txtweight.Text;
                }
                else
                {
                    msg += "weight cannot be null." + Environment.NewLine;
                    errorProvider.SetError(txtweight, "weight cannot be null.");
                }
                if (dtpweight.Value == null)
                {
                    msg += "please select a date.";
                    errorProvider.SetError(dtpweight, "please select a date.");
                }
                else
                {
                    date = dtpweight.Value.ToString("dd-MM-yyyy");
                }

                if (msg.Length > 0)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("validation error...", TAG));

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(msg, TAG));

                    MessageBox.Show(msg, "validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                weight_ui_dto _weight_ui_dto = new weight_ui_dto();
                _weight_ui_dto.weight_weight = weight;
                _weight_ui_dto.weight_date = date;
                _weight_ui_dto.weight_app = "c-sharp";

                responsedto response_dto = dalutilz.createweightingservice(_weight_ui_dto);
                string response_msg = response_dto.responsesuccessmessage + "\n" + response_dto.responseerrormessage;

                if (response_dto.isresponseresultsuccessful)
                {
                    helper.NotifyMessage("success", response_msg);
                    helper.LogEventViewer(new Exception(response_msg));

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(response_msg, TAG));

                    MessageBox.Show(response_msg, "create record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    helper.NotifyMessage("error", response_msg);
                    helper.LogEventViewer(new Exception(response_msg));

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(response_msg, TAG));

                    MessageBox.Show(response_msg, "create record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                txtweight.Text = get_random_weight();

                //refresh_parent_grid();

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
            btnadd.Text = "add";
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            refresh_parent_grid();
            this.Close();
        }

        private string get_random_weight()
        {
            String virtual_weight = generate_random_integer();
            int number = int.Parse(virtual_weight);
            while (number < 10)
            {
                virtual_weight = generate_random_integer();
                number = int.Parse(virtual_weight);
                if (number >= 10)
                {
                    break;
                }
            }
            return virtual_weight;
        }
        private string generate_random_integer()
        {
            var milliseconds = DateTime.Now.Millisecond.ToString();
            var virtual_weight = milliseconds.Substring(0, 2);
            return virtual_weight;
        }
        private void refresh_parent_grid()
        {
            listweightform f = (listweightform)this.Owner;
            f.filter_lst_records();
        }



    }
}
