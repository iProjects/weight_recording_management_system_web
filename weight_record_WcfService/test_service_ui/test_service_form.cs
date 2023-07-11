
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using weight_recording_service_dal;
using weight_recording_WcfServiceLibrary;

namespace test_service_ui
{
    public partial class test_service_form : Form
    {
        public string TAG;
        public List<notificationdto> _lstnotificationdto = new List<notificationdto>();
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;

        public test_service_form()
        {
            InitializeComponent();
            TAG = this.GetType().Name;

            _notificationmessageEventname += notificationmessageHandler;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("initialized weight recording test Service app", TAG));
        }
        //Event handler declaration:
        public void notificationmessageHandler(object sender, notificationmessageEventArgs args)
        {
            /* Handler logic */
            try
            {
                notificationdto _notificationdto = new notificationdto();

                DateTime currentDate = DateTime.Now;
                String dateTimenow = currentDate.ToString("dd-MM-yyyy HH:mm:ss");

                String _logtext = Environment.NewLine + "[ " + dateTimenow + " ]   " + args.message;

                _notificationdto._notification_message = _logtext;
                _notificationdto._created_datetime = dateTimenow;
                _notificationdto.TAG = args.TAG;

                Log.WriteToErrorLogFile(new Exception(args.message));

                _lstnotificationdto.Add(_notificationdto);

                var _lstmsgdto = from msgdto in _lstnotificationdto
                                 orderby msgdto._created_datetime ascending
                                 select msgdto._notification_message;

                String[] _logflippedlines = _lstmsgdto.ToArray();

                foreach (string log in _logflippedlines)
                {
                    //log_to_textbox(args.message);
                    txtlogs.Text += log.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        private void log_to_textbox(string message)
        {
            try
            {
                string a = txtlogs.Text;

                string[] b = a.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
                StringBuilder sb = new StringBuilder();
                sb.Append(message);
                sb.Append(System.Environment.NewLine);
                for (int i = b.Length - 1; i >= 0; i--)
                {
                    // Do something ...  
                }
                foreach (string ss in b.Reverse())
                {
                    sb.Append(ss);
                    sb.Append(System.Environment.NewLine);
                }

                txtlogs.Text = sb.ToString();

                //StringBuilder sb = new StringBuilder();
                //sb.AppendLine(message);
                //txtlogs.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void test_service_from_Load(object sender, EventArgs e)
        {
            try
            {
                btnrefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                Console.WriteLine(ex.Message);
            }
        }
        private void btnrefresh_Click(object sender, EventArgs e)
        {
            try
            {
                _lstnotificationdto.Clear();
                //_lstnotificationdto = null;
                //_lstnotificationdto = new List<notificationdto>();
                txtlogs.Clear();
                btnrefresh.Text = "refreshing...";
                test_main_service_senarios();
                test_sqlite_service_senarios();
                //test_main_service_library_senarios();
                //test_sqlite_service_library_senarios();
                txtlogs.ScrollToCaret();

                //_process_database_tasks_in_background.Invoke(this, new notificationmessageEventArgs("execute", TAG));

                SplashScreen.CloseForm();

                btnrefresh.Text = "refresh";

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                Console.WriteLine(ex.Message);
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string get_virtual_weight()
        {
            var milliseconds = DateTime.Now.Millisecond.ToString();
            var virtual_weight = milliseconds.Substring(0, 2);
            return virtual_weight;
        }

        private void test_main_service_senarios()
        {
            try
            {
                weight_record_dto _weight_record_dto = new weight_record_dto
                {
                    weight_weight = get_virtual_weight(),
                    weight_date = DateTime.Now.ToString("dd-MM-yyyy"),
                    weight_status = "Active",
                    created_date = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss tt"),
                    weight_app = DBContract.APP
                };

                responsedto _dal_responsedto = test_main_service(_weight_record_dto);

                if (_dal_responsedto.responsesuccessmessage != null)
                    txtlogs.Text += _dal_responsedto.responsesuccessmessage;

                if (_dal_responsedto.responseerrormessage != null)
                    txtlogs.Text += _dal_responsedto.responseerrormessage;

                weight_record_service.iweight_record_serviceClient _service = new weight_record_service.iweight_record_serviceClient();

                _service.Open();

                List<weight_record_dto> lstdtos = _service.getallweightsinservice();

                txtlogs.Text += Environment.NewLine + "records count = " + lstdtos.Count;

                foreach (var dto in lstdtos)
                {
                    var _record = Environment.NewLine + "{ id = " + dto.weight_id + ", weight = " + dto.weight_weight + ", date = " + dto.weight_date + ", status = " + dto.weight_status + ", created date = " + dto.created_date + ", app = " + dto.weight_app + " }";

                    txtlogs.Text += _record;
                }

                int count = lstdtos.Count;
                groupBox2.Text = "mssql count [ " + count.ToString() + " ]";

                _service.Close();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                Console.WriteLine(ex.Message);
            }

        }

        private responsedto test_main_service(weight_record_dto _weight_record_dto)
        {
            weight_record_service.iweight_record_serviceClient _service = new weight_record_service.iweight_record_serviceClient();
            responsedto _responsedto = new responsedto();
            try
            {
                _service.Open();

                responsedto _service_responsedto = _service.createweightinservice(_weight_record_dto);

                _responsedto = _service_responsedto;

                return _responsedto;

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage += ex.Message;
                return _responsedto;
            }
            finally
            {
                _service.Close();
            }

        }

        //private void test_main_service_library_senarios()
        //{
        //    try
        //    {
        //        weight_record_dto _weight_record_dto = new weight_record_dto
        //        {
        //            weight_weight = get_virtual_weight(),
        //            weight_date = DateTime.Now.ToString("dd-MM-yyyy"),
        //            weight_status = "Active",
        //            created_date = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss tt"),
        //            weight_app = DBContract.APP
        //        };

        //        responsedto _dal_responsedto = test_main_service_library(_weight_record_dto);

        //        if (_dal_responsedto.responsesuccessmessage != null)
        //            txtlogs.Text += _dal_responsedto.responsesuccessmessage;

        //        if (_dal_responsedto.responseerrormessage != null)
        //            txtlogs.Text += _dal_responsedto.responseerrormessage;

        //        weight_record_service _weight_record_service = new weight_record_service();

        //        List<weight_record_dto> lstdtos = _weight_record_service.getallweightsinservice();

        //        txtlogs.Text += Environment.NewLine + "records count = " + lstdtos.Count;

        //        foreach (var dto in lstdtos)
        //        {
        //            var _record = Environment.NewLine + "{ id = " + dto.weight_id + ", weight = " + dto.weight_weight + ", date = " + dto.weight_date + ", status = " + dto.weight_status + ", created date = " + dto.created_date + ", app = " + dto.weight_app + " }";

        //            txtlogs.Text += _record;
        //        }
        //int count = lstdtos.Count;
        //groupBox2.Text = "mssql count [ " + count.ToString() + " ]";

        //    }
        //    catch (Exception ex)
        //    {
        //        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
        //        Console.WriteLine(ex.Message);
        //    }

        //}

        //private responsedto test_main_service_library(weight_record_dto _weight_record_dto)
        //{
        //    responsedto _responsedto = new responsedto();
        //    try
        //    {
        //        weight_record_service _weight_record_service = new weight_record_service();
        //        responsedto _service_responsedto = _weight_record_service.createweightinservice(_weight_record_dto);

        //        _responsedto = _service_responsedto;

        //        return _responsedto;

        //    }
        //    catch (Exception ex)
        //    {
        //        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
        //        _responsedto.isresponseresultsuccessful = false;
        //        _responsedto.responseerrormessage += ex.Message;
        //        return _responsedto;
        //    }

        //}

        private void test_sqlite_service_senarios()
        {
            try
            {
                sqlite_weight_recording_WcfService.weight_record_dto sqlite_weight_record_dto = new sqlite_weight_recording_WcfService.weight_record_dto
                {
                    weight_weight = get_virtual_weight(),
                    weight_date = DateTime.Now.ToString("dd-MM-yyyy"),
                    weight_status = "Active",
                    created_date = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss tt"),
                    weight_app = DBContract.APP
                };

                sqlite_weight_recording_WcfService.responsedto _dal_responsedto = test_sqlite_service(sqlite_weight_record_dto);

                if (_dal_responsedto.responsesuccessmessage != null)
                    txtlogs.Text += _dal_responsedto.responsesuccessmessage;

                if (_dal_responsedto.responseerrormessage != null)
                    txtlogs.Text += _dal_responsedto.responseerrormessage;

                sqlite_weight_recording_WcfService.isqlite_service_interfaceClient service = new sqlite_weight_recording_WcfService.isqlite_service_interfaceClient();

                service.Open();

                List<sqlite_weight_recording_WcfService.weight_record_dto> lstdtos = service.getallweightsinservice();

                if (lstdtos != null)
                {
                    txtlogs.Text += Environment.NewLine + "records count = " + lstdtos.Count;

                    foreach (var dto in lstdtos)
                    {
                        var _record = Environment.NewLine + "{ id = " + dto.weight_id + ", weight = " + dto.weight_weight + ", date = " + dto.weight_date + ", status = " + dto.weight_status + ", created date = " + dto.created_date + ", app = " + dto.weight_app + " }";

                        txtlogs.Text += _record;
                    }
                }

                int count = lstdtos.Count;
                groupBox1.Text = "sqlite count [ " + count.ToString() + " ]";

                service.Close();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                Console.WriteLine(ex.Message);
            }

        }

        private sqlite_weight_recording_WcfService.responsedto test_sqlite_service(sqlite_weight_recording_WcfService.weight_record_dto _weight_record_dto)
        {
            sqlite_weight_recording_WcfService.isqlite_service_interfaceClient service = new sqlite_weight_recording_WcfService.isqlite_service_interfaceClient();
            sqlite_weight_recording_WcfService.responsedto response_dto = new sqlite_weight_recording_WcfService.responsedto();
            try
            {
                service.Open();

                sqlite_weight_recording_WcfService.responsedto db_setup_responsedto = service.setup_database();

                response_dto = db_setup_responsedto;

                sqlite_weight_recording_WcfService.responsedto service_responsedto = service.createweightinservice(_weight_record_dto);

                response_dto = service_responsedto;
            }
            catch (Exception ex)
            {
                response_dto.responseerrormessage += ex.Message;
            }
            finally
            {
                service.Close();
            }
            return response_dto;
        }

        //private void test_sqlite_service_library_senarios()
        //{
        //    try
        //    {
        //        sqlite_weight_recording_WcfService.weight_record_dto sqlite_weight_record_dto = new sqlite_weight_recording_WcfService.weight_record_dto
        //        {
        //            weight_weight = get_virtual_weight(),
        //            weight_date = DateTime.Now.ToString("dd-MM-yyyy"),
        //            weight_status = "Active",
        //            created_date = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss tt"),
        //            weight_app = DBContract.APP
        //        };

        //        sqlite_weight_recording_WcfService.responsedto _dal_responsedto = test_sqlite_service_library(sqlite_weight_record_dto);

        //if (_dal_responsedto.responsesuccessmessage != null)
        //    txtlogs.Text += _dal_responsedto.responsesuccessmessage;

        //if (_dal_responsedto.responseerrormessage != null)
        //    txtlogs.Text += _dal_responsedto.responseerrormessage;

        //        sqlite_weight_recording_WcfService.sqlite_service_implementation service = new sqlite_weight_recording_WcfService.sqlite_service_implementation();


        //        List<sqlite_weight_recording_WcfService.weight_record_dto> lstdtos = service.getallweightsinservice();


        //        txtlogs.Text += Environment.NewLine + "records count = " + lstdtos.Count;

        //        foreach (var dto in lstdtos)
        //        {
        //            var _record = Environment.NewLine + "{ id = " + dto.weight_id + ", weight = " + dto.weight_weight + ", date = " + dto.weight_date + ", status = " + dto.weight_status + ", created date = " + dto.created_date + ", app = " + dto.weight_app + " }";

        //            txtlogs.Text += _record;
        //        }

        //int count = lstdtos.Count;
        //groupBox1.Text = "sqlite count [ " + count.ToString() + " ]";

        //    }
        //    catch (Exception ex)
        //    {
        //        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
        //        Console.WriteLine(ex.Message);
        //    }

        //}

        //private sqlite_weight_recording_WcfService.responsedto test_sqlite_service_library(sqlite_weight_recording_WcfService.weight_record_dto _weight_record_dto)
        //{
        //    sqlite_weight_recording_WcfService.responsedto response_dto = new sqlite_weight_recording_WcfService.responsedto();
        //    try
        //    {
        //        sqlite_weight_recording_WcfService.sqlite_service_implementation service = new sqlite_weight_recording_WcfService.sqlite_service_implementation();

        //        //sqlite_weight_recording_WcfService.weight_record_dto service_dto = convertuidtointoservicedto(dto);
        //        sqlite_weight_recording_WcfService.responsedto service_responsedto = service.createweightinservice(_weight_record_dto);
        //        //response_dto = convertserviceresponsedtointouiresponsedto(service_responsedto);
        //        response_dto = service_responsedto;
        //    }
        //    catch (Exception ex)
        //    {
        //        response_dto.responseerrormessage += ex.Message;
        //    }
        //    return response_dto;

        //}





    }
}
