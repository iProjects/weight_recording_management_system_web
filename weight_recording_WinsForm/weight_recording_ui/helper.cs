using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace weight_recording_ui
{
   public static  class helper
    {
       public static bool NotifyMessage(string _Title, string _Text)
       {
           try
           {
               using (NotifyIcon appNotifyIcon = new NotifyIcon())
               {
                   appNotifyIcon.Text = "weight recorder";
                   appNotifyIcon.Icon = new Icon("resources/Dollar.ico");
                   ContextMenuStrip contextMenuStripSystemNotification = new ContextMenuStrip();
                   appNotifyIcon.ContextMenuStrip = contextMenuStripSystemNotification;
                   appNotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                   appNotifyIcon.BalloonTipTitle = _Title;
                   appNotifyIcon.BalloonTipText = _Text;
                   appNotifyIcon.Visible = true;
                   appNotifyIcon.ShowBalloonTip(900000);
               }
               return true;
           }
           catch (Exception ex)
           {
               helper.LogEventViewer(ex);
               return false;
           }
       }
       public static bool LogEventViewer(Exception ex)
       {
           try
           {
               int log_eventid = DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Minute;
               StringBuilder sb = new StringBuilder();
               sb.AppendLine("ERROR OCCOURED AT : " + DateTime.Now.ToString());
               sb.AppendLine("SOURCE : " + ex.Source);
               sb.AppendLine("MESSAGE : " + ex.Message);
               if (ex.InnerException != null)
               {
                   sb.AppendLine("INNER EXCEPTION : " + ex.InnerException.Message);
               }
               if (ex.StackTrace != null)
               {
                   sb.AppendLine("STACK TRACE : " + ex.StackTrace);
               }
               sb.AppendLine("WHOLE EXCEPTION : " + ex.ToString());
               string msg = sb.ToString();

               String eventsourceName = "weight recorder";
               String logName = "weight recording";
               if (!EventLog.SourceExists(eventsourceName))
               {
                   EventLog.CreateEventSource(eventsourceName, logName);
               }
               EventLog myLog = new EventLog();
               myLog.Source = eventsourceName;
               myLog.MachineName = Environment.MachineName;
               myLog.WriteEntry(msg, EventLogEntryType.Information, log_eventid);

               return true;
           }
           catch (Exception exc)
           {
               string msg = exc.Message;
               return false;
           }
       }



    }
}
