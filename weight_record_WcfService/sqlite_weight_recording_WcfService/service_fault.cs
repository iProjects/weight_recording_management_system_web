using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace sqlite_weight_recording_WcfService
{
    [DataContract]
    public class service_fault
    {
        private string _message;
        public service_fault(string message)
        {
            _message = message;
        }
        [DataMember]
        public string message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }


    }
}
