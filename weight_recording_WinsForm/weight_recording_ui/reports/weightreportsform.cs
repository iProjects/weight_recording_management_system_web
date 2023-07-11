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

namespace weight_recording_ui.reports
{
    public partial class weightreportsform : Form
    {
        public string TAG;
        public List<notificationdto> _lstnotificationdto = new List<notificationdto>();
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        private event EventHandler<notificationmessageEventArgs> _parent_notificationmessageEventname;
		
        public weightreportsform(EventHandler<notificationmessageEventArgs> parent_notificationmessageEventname)
        {
            InitializeComponent();

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname += notificationmessageHandler;
            _parent_notificationmessageEventname = parent_notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished weightreportsform initialization", TAG));

        }
        //Event handler declaration:
        public void notificationmessageHandler(object sender, notificationmessageEventArgs args)
        {
            try
            {
                /* Handler logic */

                //Invoke(new Action(() =>
                //{

                notificationdto _notificationdto = new notificationdto();

                DateTime currentDate = DateTime.Now;
                String dateTimenow = currentDate.ToString("dd-MM-yyyy HH:mm:ss");

                String _logtext = Environment.NewLine + "[ " + dateTimenow + " ]   " + args.message;

                _notificationdto._notification_message = _logtext;
                _notificationdto._created_datetime = dateTimenow;
                _notificationdto.TAG = args.TAG;

                Log.WriteToErrorLogFile(new Exception(args.message));

                _parent_notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(args.message, TAG));

                _lstnotificationdto.Add(_notificationdto);

                var _lstmsgdto = from msgdto in _lstnotificationdto
                                 orderby msgdto._created_datetime descending
                                 select msgdto._notification_message;

                String[] _logflippedlines = _lstmsgdto.ToArray();

                txtlog.Lines = _logflippedlines;
                txtlog.ScrollToCaret();

                //}));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        private void weightreportsform_Load(object sender, EventArgs e)
        {
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished weightreportsform load", TAG));
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnpdfreport_Click(object sender, EventArgs e)
        {
            pdfviewer pdfviewer = new pdfviewer(_notificationmessageEventname) { Owner = this };
            pdfviewer.Show();
        }

        private void btnchartreport_Click(object sender, EventArgs e)
        {
            chartform chartform = new chartform(_notificationmessageEventname) { Owner = this };
            chartform.Show();
        }

        private void btncrystalreport_Click(object sender, EventArgs e)
        {
            crystal_report_form crystal_report_form = new crystal_report_form(_notificationmessageEventname) { Owner = this };
            crystal_report_form.Show();
        }
    }
}
