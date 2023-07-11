using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlite_weight_recording_WcfService
{
    /// <summary>
    /// Description of utilzsingleton.
    /// </summary>
    public sealed class utilzsingleton
    {
        // Because the _instance member is made private, the only way to get the single
        // instance is via the static Instance property below. This can also be similarly
        // achieved with a GetInstance() method instead of the property. 
        private static utilzsingleton singleInstance;

        public static utilzsingleton getInstance(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            // The first call will create the one and only instance.
            if (singleInstance == null)
                singleInstance = new utilzsingleton(notificationmessageEventname);
            // Every call afterwards will return the single instance created above.
            return singleInstance;
        }

        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        public string TAG;

        private utilzsingleton(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            TAG = this.GetType().Name;
            _notificationmessageEventname = notificationmessageEventname;
        }

        private utilzsingleton()
        {

        }
		

        public string getappsettinggivenkey(string key = "", string defaultvalue = "")
        {
            try
            {

                string configvalue = "";

                configvalue = System.Configuration.ConfigurationManager.AppSettings[key];

                if (configvalue == null || String.IsNullOrEmpty(configvalue))
                {
                    return defaultvalue;
                }
                else
                {
                    return configvalue;
                }

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, this.TAG));
                return defaultvalue;
            }
        }

        public weight_record_dto buildweightdtogivendatatable(DataTable dt, int _index)
        {
            weight_record_dto _weight_record_dto = new weight_record_dto();
            _weight_record_dto.weight_id = Convert.ToString(dt.Rows[_index][DBContract.weightsentitytable.WEIGHT_ID]);
            _weight_record_dto.weight_weight = Convert.ToString(dt.Rows[_index][DBContract.weightsentitytable.WEIGHT_WEIGHT]);
            _weight_record_dto.weight_date = Convert.ToString(dt.Rows[_index][DBContract.weightsentitytable.WEIGHT_DATE]);
            _weight_record_dto.weight_status = Convert.ToString(dt.Rows[_index][DBContract.weightsentitytable.WEIGHT_STATUS]);
            _weight_record_dto.created_date = Convert.ToString(dt.Rows[_index][DBContract.weightsentitytable.CREATED_DATE]);
            _weight_record_dto.weight_app = Convert.ToString(dt.Rows[_index][DBContract.weightsentitytable.WEIGHT_APP]);

            return _weight_record_dto;
        }
		



    }
}
