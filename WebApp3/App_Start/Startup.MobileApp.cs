using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using Owin;
using LightInject;
using System.Web.Http.Cors;

namespace WebApp3
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            httpConfiguration.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            var container = new ServiceContainer();

            container.RegisterApiControllers();
            container.EnableWebApi(httpConfiguration);

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .MapApiControllers()
                .ApplyTo(httpConfiguration);

            app.UseWebApi(httpConfiguration);
        }
    }
}

