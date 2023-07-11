using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace sqlite_weight_recording_WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(Namespace = "url")]
    public class sqlite_service_implementation : isqlite_service_interface
    {
        public string TAG;
        public List<notificationdto> _lstnotificationdto = new List<notificationdto>();
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;

        public sqlite_service_implementation()
        {

            TAG = this.GetType().Name;

            _notificationmessageEventname += notificationmessageHandler;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("initialized sqlite weight recording Wcf service", TAG));
        }
        //Event handler declaration:
        public void notificationmessageHandler(object sender, notificationmessageEventArgs args)
        {
            /* Handler logic */

            notificationdto _notificationdto = new notificationdto();

            DateTime currentDate = DateTime.Now;
            String dateTimenow = currentDate.ToString("dd-MM-yyyy HH:mm:ss");

            String _logtext = Environment.NewLine + "[ " + dateTimenow + " ]   " + args.message;

            _notificationdto._notification_message = _logtext;
            _notificationdto._created_datetime = dateTimenow;
            _notificationdto.TAG = args.TAG;

            _lstnotificationdto.Add(_notificationdto);

            var _lstmsgdto = from msgdto in _lstnotificationdto
                             orderby msgdto._created_datetime descending
                             select msgdto._notification_message;

            String[] _logflippedlines = _lstmsgdto.ToArray();


        }

        DataTable getallrecordsfromsqlite()
        {
            try
            {

                DataTable sqlite_dt = sqliteapisingleton.getInstance(_notificationmessageEventname).getallrecordsglobal();
                if (sqlite_dt != null && sqlite_dt.Rows.Count != 0)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("sqlite records count: " + sqlite_dt.Rows.Count, TAG));
                }
                return sqlite_dt;

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                return null;
            }
        }
        public List<weight_record_dto> getallweightsinservice()
        {
            try
            {
                List<weight_record_dto> lstdtos = new List<weight_record_dto>();

                var sqlite_dt = getallrecordsfromsqlite();

                var _recordscount = sqlite_dt.Rows.Count;

                for (int i = 0; i < _recordscount; i++)
                {

                    weight_record_dto weight_dto = utilzsingleton.getInstance(_notificationmessageEventname).buildweightdtogivendatatable(sqlite_dt, i);

                    lstdtos.Add(weight_dto);

                }

                lstdtos.Reverse();

                return lstdtos;

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG)); 
                return null;
            }

        }
        public responsedto createweightinservice(weight_record_dto service_dto)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                _responsedto = global_insert(service_dto);

                return _responsedto;

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG)); 
                return _responsedto;
            }

        }
        private responsedto global_insert(weight_record_dto service_dto)
        {
            responsedto _responsedto = new responsedto();
            try
            {

                try
                {
                    responsedto _sqlite_responsedto = sqliteapisingleton.getInstance(_notificationmessageEventname).createweightindatabase(service_dto);

                    if (_sqlite_responsedto.isresponseresultsuccessful)
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_sqlite_responsedto.responsesuccessmessage, TAG));
                        _responsedto.responsesuccessmessage += (Environment.NewLine + _sqlite_responsedto.responsesuccessmessage);
                    }
                    else
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_sqlite_responsedto.responseerrormessage, TAG));
                        _responsedto.responseerrormessage += (Environment.NewLine + _sqlite_responsedto.responseerrormessage);
                    }

                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG)); 
                    _responsedto.isresponseresultsuccessful = false;
                    _responsedto.responseerrormessage += ex.Message;
                }

                return _responsedto;

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG)); 
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage += ex.Message;
                return _responsedto;
            }
        }
        public responsedto setup_database()
        {
            responsedto _responsedto = new responsedto();
            try
            {
                try
                {
                    responsedto _sqlite_responsedto = sqliteapisingleton.getInstance(_notificationmessageEventname).setup_database();

                    if (_sqlite_responsedto.isresponseresultsuccessful)
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_sqlite_responsedto.responsesuccessmessage, TAG));
                        _responsedto.responsesuccessmessage += (Environment.NewLine + _sqlite_responsedto.responsesuccessmessage);
                    }
                    else
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_sqlite_responsedto.responseerrormessage, TAG));
                        _responsedto.responseerrormessage += (Environment.NewLine + _sqlite_responsedto.responseerrormessage);
                    }

                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                    _responsedto.isresponseresultsuccessful = false;
                    _responsedto.responseerrormessage += ex.Message;
                }

                return _responsedto;

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG)); 
                return _responsedto;
            }

        }



    }
}
