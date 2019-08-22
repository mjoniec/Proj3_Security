using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Data.Services;
using Data.Repositories;
using Microsoft.OpenApi.Models;
using Mqtt.Client;

namespace Gold.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Config providing related services url's and other data, debug/release version. 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Gold API",
                    Description = "ASP.NET Core Web API Service providing daily gold prices",
                    TermsOfService = new Uri(@"https://github.com/mjoniec/GoldBackend/wiki"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Marcin Joniec",
                        Email = "marcin_joniec@hotmail.com",
                        Url = new Uri(@"https://github.com/mjoniec/GoldBackend")
                    }
                });
                c.IncludeXmlComments(GetXmlCommentsPath());
            });

            services.AddSingleton<IGoldService, GoldService>();
            services.AddSingleton<IGoldRepository, GoldRepository>();
            services.AddSingleton<IMqttDualTopicClient, MqttDualTopicClient>();
            services.AddSingleton(s => new MqttDualTopicData(
                Configuration["Mqtt:Ip"],
                int.Parse(Configuration["Mqtt:Port"]),
                Configuration["Mqtt:TopicReceiver"],
                Configuration["Mqtt:TopicSender"]));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gold API V1");
            });
        }

        private string GetXmlCommentsPath()
        {
            var app = AppContext.BaseDirectory;

            return System.IO.Path.Combine(app, "Gold.Service.xml");
        }
    }
}
