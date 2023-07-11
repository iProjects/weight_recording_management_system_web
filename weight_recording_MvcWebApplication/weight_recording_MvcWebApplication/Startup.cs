using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(weight_recording_MvcWebApplication.Startup))]
namespace weight_recording_MvcWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
