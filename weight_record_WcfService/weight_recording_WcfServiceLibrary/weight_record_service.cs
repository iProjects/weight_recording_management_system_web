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
using weight_recording_service_dal;

namespace weight_recording_WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(Namespace = "url")]
    public class weight_record_service : iweight_record_service
    {
        public string TAG;
        public List<notificationdto> _lstnotificationdto = new List<notificationdto>();
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;

        public weight_record_service()
        {

            TAG = this.GetType().Name;

            _notificationmessageEventname += notificationmessageHandler;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("initialized weight recording Wcf Service Library", TAG));
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

                //Log.WriteToErrorLogFile(new Exception(args.message));

                _lstnotificationdto.Add(_notificationdto);

                var _lstmsgdto = from msgdto in _lstnotificationdto
                                 orderby msgdto._created_datetime descending
                                 select msgdto._notification_message;

                String[] _logflippedlines = _lstmsgdto.ToArray();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        DataTable getallrecordsfrommssql()
        {
            try
            {

                DataTable mssql_dt = mssqlapisingleton.getInstance(_notificationmessageEventname).getallrecordsglobal();
                if (mssql_dt != null && mssql_dt.Rows.Count != 0)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("mssql records count: " + mssql_dt.Rows.Count, TAG));
                }
                return mssql_dt;

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                return null;
            }
        }
        DataTable getallrecordsfrommysql()
        {
            try
            {

                DataTable mysql_dt = mysqlapisingleton.getInstance(_notificationmessageEventname).getallrecordsglobal();
                if (mysql_dt != null && mysql_dt.Rows.Count != 0)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("mysql records count: " + mysql_dt.Rows.Count, TAG));
                }
                return mysql_dt;

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                return null;
            }
        }
        DataTable getallrecordsfrompostgresql()
        {
            try
            {

                DataTable postgresql_dt = postgresqlapisingleton.getInstance(_notificationmessageEventname).getallrecordsglobal();
                if (postgresql_dt != null && postgresql_dt.Rows.Count != 0)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("postgresql records count: " + postgresql_dt.Rows.Count, TAG));
                }
                return postgresql_dt;

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

                var mssql_dt = getallrecordsfrommssql();
                var mysql_dt = getallrecordsfrommysql();
                var postgresql_dt = getallrecordsfrompostgresql();

                var _recordscount = mssql_dt.Rows.Count;

                for (int i = 0; i < _recordscount; i++)
                {
                    weight_record_dto weight_dto = utilzsingleton.getInstance(_notificationmessageEventname).buildweightdtogivendatatable(mssql_dt, i);
                    lstdtos.Add(weight_dto);
                }

                lstdtos.Reverse();

                //return lstdtos.Take(100).ToList();
                return lstdtos.ToList();

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                //throw new FaultException<service_fault>(new service_fault(ex.Message));
                return null;
            }

        }
        public List<weight_record_dto> get_all_active_weights_in_service()
        {
            try
            {
                List<weight_record_dto> lstdtos = getallweightsinservice();
                lstdtos = lstdtos.Where(b => b.weight_status.Equals("active", StringComparison.OrdinalIgnoreCase)).ToList();

                //return lstdtos.Take(100).ToList();
                return lstdtos.ToList();

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                //throw new FaultException<service_fault>(new service_fault(ex.Message));
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
                //throw new FaultException<service_fault>(new service_fault(ex.Message));
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage += ex.Message;
                return _responsedto;
            }

        }
        private responsedto global_insert(weight_record_dto service_dto)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                //mssql
                try
                {
                    responsedto _mssql_responsedto = mssqlapisingleton.getInstance(_notificationmessageEventname).createweightindatabase(service_dto);

                    if (_mssql_responsedto.isresponseresultsuccessful)
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_mssql_responsedto.responsesuccessmessage, TAG));
                        _responsedto.responsesuccessmessage += (Environment.NewLine + _mssql_responsedto.responsesuccessmessage);
                    }
                    else
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_mssql_responsedto.responseerrormessage, TAG));
                        _responsedto.responseerrormessage += (Environment.NewLine + _mssql_responsedto.responseerrormessage);
                    }
                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                    //throw new FaultException<service_fault>(new service_fault(ex.Message));
                    _responsedto.isresponseresultsuccessful = false;
                    _responsedto.responseerrormessage += ex.Message;
                }

                //mysql
                try
                {
                    responsedto _mysql_responsedto = mysqlapisingleton.getInstance(_notificationmessageEventname).createweightindatabase(service_dto);

                    if (_mysql_responsedto.isresponseresultsuccessful)
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_mysql_responsedto.responsesuccessmessage, TAG));
                        _responsedto.responsesuccessmessage += (Environment.NewLine + _mysql_responsedto.responsesuccessmessage);
                    }
                    else
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_mysql_responsedto.responseerrormessage, TAG));
                        _responsedto.responseerrormessage += (Environment.NewLine + _mysql_responsedto.responseerrormessage);
                    }

                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                    //throw new FaultException<service_fault>(new service_fault(ex.Message));
                    _responsedto.isresponseresultsuccessful = false;
                    _responsedto.responseerrormessage += ex.Message;
                }

                //postgresql
                try
                {
                    responsedto _postgresql_responsedto = postgresqlapisingleton.getInstance(_notificationmessageEventname).createweightindatabase(service_dto);

                    if (_postgresql_responsedto.isresponseresultsuccessful)
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_postgresql_responsedto.responsesuccessmessage, TAG));
                        _responsedto.responsesuccessmessage += (Environment.NewLine + _postgresql_responsedto.responsesuccessmessage);
                    }
                    else
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_postgresql_responsedto.responseerrormessage, TAG));
                        _responsedto.responseerrormessage += (Environment.NewLine + _postgresql_responsedto.responseerrormessage);
                    }


                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                    //throw new FaultException<service_fault>(new service_fault(ex.Message));
                    _responsedto.isresponseresultsuccessful = false;
                    _responsedto.responseerrormessage += ex.Message;
                }

                //sqlite
                //try
                //{
                //    responsedto _sqlite_responsedto = sqliteapisingleton.getInstance(_notificationmessageEventname).createweightindatabase(service_dto);

                //    if (_sqlite_responsedto.isresponseresultsuccessful)
                //    {
                //        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_sqlite_responsedto.responsesuccessmessage, TAG));
                //        _responsedto.responsesuccessmessage += (Environment.NewLine + _sqlite_responsedto.responsesuccessmessage);
                //    }
                //    else
                //    {
                //        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_sqlite_responsedto.responseerrormessage, TAG));
                //        _responsedto.responseerrormessage += (Environment.NewLine + _sqlite_responsedto.responseerrormessage);
                //    }

                //_responsedto.isresponseresultsuccessful = _mssql_responsedto.isresponseresultsuccessful;

                //}
                //catch (Exception ex)
                //{
                //    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                //    //throw new FaultException<service_fault>(new service_fault(ex.Message));
                //    _responsedto.isresponseresultsuccessful = false;
                //    _responsedto.responseerrormessage += ex.Message;
                //}

                return _responsedto;

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                //throw new FaultException<service_fault>(new service_fault(ex.Message));
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage += ex.Message;
                return _responsedto;
            }
        }

        public responsedto updateweightinservice(weight_record_dto service_dto)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                responsedto _mssql_responsedto = mssqlapisingleton.getInstance(_notificationmessageEventname).updateweightindatabase(service_dto);

                if (_mssql_responsedto.isresponseresultsuccessful)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_mssql_responsedto.responsesuccessmessage, TAG));
                    _responsedto.responsesuccessmessage += (Environment.NewLine + _mssql_responsedto.responsesuccessmessage);
                }
                else
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_mssql_responsedto.responseerrormessage, TAG));
                    _responsedto.responseerrormessage += (Environment.NewLine + _mssql_responsedto.responseerrormessage);
                }

                _responsedto.isresponseresultsuccessful = _mssql_responsedto.isresponseresultsuccessful;

                return _responsedto;

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                //throw new FaultException<service_fault>(new service_fault(ex.Message));
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage += ex.Message;
                return _responsedto;
            }

        }
        public responsedto deleteweightinservice(weight_record_dto service_dto)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                responsedto _mssql_responsedto = mssqlapisingleton.getInstance(_notificationmessageEventname).deleteweightindatabase(service_dto);

                if (_mssql_responsedto.isresponseresultsuccessful)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_mssql_responsedto.responsesuccessmessage, TAG));
                    _responsedto.responsesuccessmessage += (Environment.NewLine + _mssql_responsedto.responsesuccessmessage);
                }
                else
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_mssql_responsedto.responseerrormessage, TAG));
                    _responsedto.responseerrormessage += (Environment.NewLine + _mssql_responsedto.responseerrormessage);
                }

                _responsedto.isresponseresultsuccessful = _mssql_responsedto.isresponseresultsuccessful;

                return _responsedto;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                //throw new FaultException<service_fault>(new service_fault(ex.Message));
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage += ex.Message;
                return _responsedto;
            }

        }
        public responsedto delete_weight_by_changing_status_in_service(weight_record_dto service_dto)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                responsedto _mssql_responsedto = mssqlapisingleton.getInstance(_notificationmessageEventname).delete_weight_by_changing_status_in_database(service_dto);

                if (_mssql_responsedto.isresponseresultsuccessful)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_mssql_responsedto.responsesuccessmessage, TAG));
                    _responsedto.responsesuccessmessage += (Environment.NewLine + _mssql_responsedto.responsesuccessmessage);
                }
                else
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_mssql_responsedto.responseerrormessage, TAG));
                    _responsedto.responseerrormessage += (Environment.NewLine + _mssql_responsedto.responseerrormessage);
                }

                _responsedto.isresponseresultsuccessful = _mssql_responsedto.isresponseresultsuccessful;

                return _responsedto;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                //throw new FaultException<service_fault>(new service_fault(ex.Message));
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage += ex.Message;
                return _responsedto;
            }

        }




    }
}
