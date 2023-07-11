using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using weight_recording_dal;
using weight_recording_ui.reports;

namespace weight_recording_ui
{
    public partial class listweightform : Form
    {
        public string TAG;
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        dalutilz dalutilz = new dalutilz();

        public listweightform(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            InitializeComponent();
            _notificationmessageEventname = notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished listweightform initialization", TAG));

        }

        private void listweightform_Load(object sender, EventArgs e)
        {
            try
            {
                var today = DateTime.Now;
                dtpdateto.Value = today;

                var oneyearago = DateTime.Now.AddYears(-1);
                dtpdatefrom.Value = oneyearago;

                string base_directory = AppDomain.CurrentDomain.BaseDirectory;
                string dir = Directory.GetParent(Application.ExecutablePath).ToString();
                Console.WriteLine(base_directory);
                Console.WriteLine(dir);

                List<weight_ui_dto> _lst_records = dalutilz.getallweightsforui();

                List<string> _lst_apps = _lst_records.Where(t => t.weight_app != string.Empty).Select(f => f.weight_app).Distinct().ToList();

                IEnumerable<string> lst_sorted_apps =
                from app in _lst_apps
                orderby app descending
                select app;

                List<string> sorted_apps_list = lst_sorted_apps.ToList();

                sorted_apps_list.Add("All");
                sorted_apps_list.Reverse();

                cbo_filter_by_app.DataSource = sorted_apps_list;

                filter_lst_records();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished listweightform load", TAG));
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }

        public void refreshgrid()
        {
            try
            {
                bindingSourceweights.DataSource = null;
                dataGridViewweights.DataSource = null;

                List<weight_ui_dto> _lst_records = dalutilz.lstconvertservicedtointouidto(dalutilz.get_all_active_weights_from_service());

                bindingSourceweights.DataSource = _lst_records;
                dataGridViewweights.DataSource = bindingSourceweights;

                groupBox2.Text = bindingSourceweights.Count.ToString();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + bindingSourceweights.Count.ToString() + " ] records for grid...", TAG));

                dataGridViewweights.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewweights.MultiSelect = false;
                dataGridViewweights.ReadOnly = true;
                dataGridViewweights.AllowUserToAddRows = false;
                dataGridViewweights.AllowUserToDeleteRows = false;
                dataGridViewweights.AutoGenerateColumns = false;

                foreach (DataGridViewRow row in dataGridViewweights.Rows)
                {
                    dataGridViewweights.Rows[dataGridViewweights.Rows.Count - 1].Selected = true;
                    int nRowIndex = dataGridViewweights.Rows.Count - 1;
                    bindingSourceweights.Position = nRowIndex;
                }

                helper.LogEventViewer(new Exception("fetched [ " + bindingSourceweights.Count.ToString() + " ] records for grid..."));
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }

        private void btnfilter_Click(object sender, EventArgs e)
        {
            btnfilter.Text = "filtering...";
            filter_lst_records();
            btnfilter.Text = "filter";
        }
        public void filter_lst_records()
        {
            try
            {
                var _datefrom = dtpdatefrom.Value;
                var _dateto = dtpdateto.Value;
                string selected_app = cbo_filter_by_app.SelectedValue.ToString();

                List<weight_ui_dto> _lst_records = dalutilz.lstconvertservicedtointouidto(dalutilz.get_all_active_weights_from_service());
                List<weight_ui_dto> _lst_filtered_records = new List<weight_ui_dto>();

                if (selected_app == "All")
                {
                    _lst_filtered_records = _lst_records.ToList();
                }
                else
                {
                    _lst_filtered_records = _lst_records.Where(t => t.weight_app.Equals(selected_app)).ToList();
                }

                List<weight_ui_dto> _filtered_lst_records = new List<weight_ui_dto>();

                foreach (weight_ui_dto dto in _lst_filtered_records)
                {
                    string dateTime = dto.weight_date;

                    DateTime dt;
                    DateTime.TryParseExact(dateTime,
                                           "dd-MM-yyyy",
                                           CultureInfo.InvariantCulture,
                                           DateTimeStyles.None,
                                           out dt);

                    if (dt >= _datefrom && dt <= _dateto)
                    {
                        _filtered_lst_records.Add(dto);
                    }
                }

                bindingSourceweights.DataSource = null;
                dataGridViewweights.DataSource = null;

                bindingSourceweights.DataSource = _filtered_lst_records;
                dataGridViewweights.DataSource = bindingSourceweights;

                groupBox2.Text = bindingSourceweights.Count.ToString();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + bindingSourceweights.Count.ToString() + " ] records for grid...", TAG));

                dataGridViewweights.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewweights.MultiSelect = false;
                dataGridViewweights.ReadOnly = true;
                dataGridViewweights.AllowUserToAddRows = false;
                dataGridViewweights.AllowUserToDeleteRows = false;
                dataGridViewweights.AutoGenerateColumns = false;

                foreach (DataGridViewRow row in dataGridViewweights.Rows)
                {
                    dataGridViewweights.Rows[dataGridViewweights.Rows.Count - 1].Selected = true;
                    int nRowIndex = dataGridViewweights.Rows.Count - 1;
                    bindingSourceweights.Position = nRowIndex;
                }

                helper.LogEventViewer(new Exception("fetched [ " + bindingSourceweights.Count.ToString() + " ] records for grid..."));
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }

        private void cbofilterbyname_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _datefrom = dtpdatefrom.Value;
            var _dateto = dtpdateto.Value;

            errorProvider.Clear();

            if (_datefrom > _dateto)
            {
                errorProvider.SetError(dtpdatefrom, "date from cannot be greater than date to.");

                var oneyearago = DateTime.Now.AddYears(-1);
                dtpdatefrom.Value = oneyearago;
            }

            if (_dateto < _datefrom)
            {
                errorProvider.SetError(dtpdateto, "date to cannot be less than date from.");

                var today = DateTime.Now;
                dtpdateto.Value = today;
            }

            string selected_app = cbo_filter_by_app.SelectedValue.ToString();
            Console.WriteLine(selected_app);

        }

        private void dtpdatefrom_ValueChanged(object sender, EventArgs e)
        {
            var _datefrom = dtpdatefrom.Value;
            var _dateto = dtpdateto.Value;

            errorProvider.Clear();

            if (_datefrom > _dateto)
            {
                errorProvider.SetError(dtpdatefrom, "date from cannot be greater than date to.");

                var oneyearago = DateTime.Now.AddYears(-1);
                dtpdatefrom.Value = oneyearago;
            }
        }

        private void dtpdateto_ValueChanged(object sender, EventArgs e)
        {
            var _datefrom = dtpdatefrom.Value;
            var _dateto = dtpdateto.Value;

            errorProvider.Clear();

            if (_dateto < _datefrom)
            {
                errorProvider.SetError(dtpdateto, "date to cannot be less than date from.");

                var today = DateTime.Now;
                dtpdateto.Value = today;
            }
        }

        private void btnclearfilter_Click(object sender, EventArgs e)
        {
            refreshgrid();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            addweightform _addweightform = new addweightform(_notificationmessageEventname) { Owner = this };
            _addweightform.ShowDialog();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            weightreportsform _printweightform = new weightreportsform(_notificationmessageEventname) { Owner = this };
            _printweightform.Show();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnedit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewweights.SelectedRows.Count != 0)
                {
                    weight_ui_dto weight_ui_dto = (weight_ui_dto)bindingSourceweights.Current;

                    editweightform _editweightform = new editweightform(weight_ui_dto, _notificationmessageEventname) { Owner = this };
                    _editweightform.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewweights.SelectedRows.Count != 0)
                {
                    weight_ui_dto weight_ui_dto = (weight_ui_dto)bindingSourceweights.Current;

                    if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete record\n" + " [ " + weight_ui_dto.weight_weight + " : " + weight_ui_dto.weight_date + " ]", "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {

                        responsedto response_dto = dalutilz.deleteweightingservice(weight_ui_dto);
                        string response_msg = response_dto.responsesuccessmessage + "\n" + response_dto.responseerrormessage;

                        if (response_dto.isresponseresultsuccessful)
                        {
                            helper.NotifyMessage("success", response_msg);
                            helper.LogEventViewer(new Exception(response_msg));

                            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(response_msg, TAG));

                            MessageBox.Show(response_msg, "delete record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            helper.NotifyMessage("error", response_msg);
                            helper.LogEventViewer(new Exception(response_msg));

                            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(response_msg, TAG));

                            MessageBox.Show(response_msg, "delete record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        filter_lst_records();
                    }
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void dataGridViewweights_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewweights.SelectedRows.Count != 0)
                {
                    weight_ui_dto weight_ui_dto = (weight_ui_dto)bindingSourceweights.Current;

                    editweightform _editweightform = new editweightform(weight_ui_dto, _notificationmessageEventname) { Owner = this };
                    _editweightform.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }




    }
}
