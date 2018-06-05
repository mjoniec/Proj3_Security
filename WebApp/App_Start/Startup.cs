using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using LightInject;
using LightInject.WebApi;
using Owin;

namespace WebApp.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            var container = new ServiceContainer();
            container.RegisterApiControllers();
            container.EnableWebApi(config);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            app.Use(config);
        }
    }
}