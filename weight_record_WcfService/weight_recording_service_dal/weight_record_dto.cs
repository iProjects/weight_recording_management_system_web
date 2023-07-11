using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks; 

namespace weight_recording_service_dal
{
    [DataContract]
    public class weight_record_dto
    {
        [DataMember]
        public string weight_id { get; set; }
        [DataMember]
        public string weight_weight { get; set; }
        [DataMember]
        public string weight_date { get; set; }
        [DataMember]
        public string weight_status { get; set; }
        [DataMember]
        public string created_date { get; set; }
        [DataMember]
        public string weight_app { get; set; }

    }
}
