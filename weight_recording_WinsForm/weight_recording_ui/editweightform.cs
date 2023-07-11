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
    public partial class editweightform : Form
    {
        public string TAG;
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        weight_ui_dto _weight_ui_dto;

        public editweightform(weight_ui_dto weight_ui_dto, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            InitializeComponent();

            _weight_ui_dto = weight_ui_dto;
            _notificationmessageEventname = notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished editweightform initialization", TAG));

        }

        private void editweightform_Load(object sender, EventArgs e)
        {
            txtweight.Text = _weight_ui_dto.weight_weight;
            dtpweight.Value = DateTime.Parse(_weight_ui_dto.weight_date);

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished editweightform load", TAG));
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            btnupdate.Text = "updating record...";
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

                _weight_ui_dto.weight_weight = weight;
                _weight_ui_dto.weight_date = date;

                responsedto response_dto = dalutilz.updateweightingservice(_weight_ui_dto);
                string response_msg = response_dto.responsesuccessmessage + "\n" + response_dto.responseerrormessage;

                if (response_dto.isresponseresultsuccessful)
                {
                    helper.NotifyMessage("success", response_msg);
                    helper.LogEventViewer(new Exception(response_msg));

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(response_msg, TAG));

                    MessageBox.Show(response_msg, "update record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    helper.NotifyMessage("error", response_msg);
                    helper.LogEventViewer(new Exception(response_msg));

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(response_msg, TAG));

                    MessageBox.Show(response_msg, "update record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                refresh_parent_grid();

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
            btnupdate.Text = "update";
        }
        private void refresh_parent_grid()
        {
            listweightform f = (listweightform)this.Owner;
            f.filter_lst_records();
        }
        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
