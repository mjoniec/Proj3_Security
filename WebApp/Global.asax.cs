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
            var httpConfiguration = new HttpConfiguration();
            var container = new ServiceContainer();

            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            container.RegisterApiControllers();
            container.EnableWebApi(httpConfiguration);



            //var container = new ServiceContainer();
            //container.RegisterApiControllers();
            //container.EnableWebApi(GlobalConfiguration.Configuration);





            //GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
