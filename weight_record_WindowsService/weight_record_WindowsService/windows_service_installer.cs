using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace weight_record_WindowsService_csharp
{
    [RunInstaller(true)]
    public class windows_service_installer : Installer
    {
        public windows_service_installer()
        {
            ServiceProcessInstaller serviceprocessinstaller = new ServiceProcessInstaller();
            ServiceInstaller serviceinstaller = new ServiceInstaller();

            //account info
            serviceprocessinstaller.Account = ServiceAccount.LocalSystem;
            serviceprocessinstaller.Username = null;
            serviceprocessinstaller.Password = null;

            //service info
            serviceinstaller.DisplayName = "weight record windows service";
            serviceinstaller.StartType = ServiceStartMode.Automatic;
            //this must be indetical to windowsservice.servicebase name set in constructor of windowsservice.cs
            serviceinstaller.ServiceName = "weight record service";
            serviceinstaller.Description = "weight record service";

            this.Installers.Add(serviceprocessinstaller);
            this.Installers.Add(serviceinstaller);

        }
    }
}
