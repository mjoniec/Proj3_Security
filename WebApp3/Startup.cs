using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebAppMobile.Startup))]

namespace WebAppMobile
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}