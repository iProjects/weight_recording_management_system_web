using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ServiceProcess;
using System.ServiceModel;

namespace weight_record_WindowsService_csharp
{
    class windows_services : ServiceBase
    {
        ServiceHost host;
        public windows_services()
        {
            this.ServiceName = "weight record service";
            this.EventLog.Log = "Application";

            //these flags set whether or not to handle that specific type of event.
            //set to true if you need it, false otherwise.
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanShutdown = true;

        }

        static void main()
        {
            ServiceBase.Run(new windows_services());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            host = new ServiceHost(typeof(weight_recording_WcfServiceLibrary.weight_record_service));
            host.Open();
        }
        protected override void OnStop()
        {
            base.OnStop();
            if (host != null)
                host.Close();
            host = null;
        }
        protected override void OnPause()
        {
            base.OnPause();
        }
        protected override void OnContinue()
        {
            base.OnContinue();
        }
        protected override void OnShutdown()
        {
            base.OnShutdown();
        }
        protected override void OnCustomCommand(int command)
        {
            base.OnCustomCommand(command);
        }
        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            return base.OnPowerEvent(powerStatus);
        }
        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            base.OnSessionChange(changeDescription);
        }


    }
}
