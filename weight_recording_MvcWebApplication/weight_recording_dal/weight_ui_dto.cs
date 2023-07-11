using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weight_recording_dal
{
    public class weight_ui_dto
    {
        public string weight_id { get; set; } 
        public string weight_weight { get; set; } 
        public string weight_date { get; set; } 
        public string weight_status { get; set; }
        public string created_date { get; set; }
        public string weight_app { get; set; }
    }
}
