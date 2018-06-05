using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace WebApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new ServiceContainer();
            container.RegisterApiControllers();
            //register other services

            container.EnableWebApi(GlobalConfiguration.Configuration);

            //GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
