using weight_recording_dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace weight_recording_ui.reports
{
    public class reportmodel
    {
        public double total_size
        {
            get
            {
                return weights.Average(t => double.Parse(t.weight_weight));
            }
        }
        public string logo { get; set; } 
        public DateTime printedon { get; set; }
         
        
        public string reportname
        {
            get
            {
                return "weights monitoring " + DateTime.Now.ToString("dd MM yyyy HH mm ss tt");
            }
        }
        public List<weight_ui_dto> weights { get; set; }
    }

    public class ReportsEngineCompleteEventArg : System.EventArgs
    {
        private int svalue;
        private string _template;

        public ReportsEngineCompleteEventArg(int value, string template)
        {
            svalue = value;
            _template = template;
        }

        public int StatusValue
        {
            get { return svalue; }
        }

        public string _Template
        {
            get { return _template; }
        }
    }
    public class ReportsProcessCompleteEventArg : System.EventArgs
    {
        private int svalue;

        public ReportsProcessCompleteEventArg(int value)
        {
            svalue = value;
        }

        public int StatusValue
        {
            get { return svalue; }
        }
    }



}
