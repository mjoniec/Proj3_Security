using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using LightInject;
using LightInject.WebApi;
using Owin;

namespace WebApp
{
    public partial class Startup
    {
        public static void ConfigureWebApp(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();
            var container = new ServiceContainer();

            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            container.RegisterApiControllers();
            container.EnableWebApi(httpConfiguration);

            app.Use(httpConfiguration);
            //swagger config
        }
    }
}