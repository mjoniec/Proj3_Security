using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Data.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Data.Repositories;
using Mqtt.Client;
using Swashbuckle.AspNetCore.Swagger;

namespace Gold.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Gold API",
                    Description = "ASP.NET Core Web API Service providing daily gold prices",
                    TermsOfService = "None",
                    Contact = new Contact()
                    {
                        Name = "Marcin Joniec",
                        Email = "marcin_joniec@hotmail.com",
                        Url = @"https://github.com/mjoniec/GoldBackend"
                    }
                });
            });


            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<GoldService>().As<IGoldService>().SingleInstance();
            containerBuilder.RegisterType<GoldRepository>().As<IGoldRepository>();
            containerBuilder.Register(ctx =>
            {
                return new MqttDualTopicClient(new MqttDualTopicData(
                    Configuration["Mqtt:Ip"],
                    int.Parse(Configuration["Mqtt:Port"]),
                    Configuration["Mqtt:TopicReceiver"],
                    Configuration["Mqtt:TopicSender"]));

            }).As<IMqttDualTopicClient>();

            containerBuilder.Populate(services);

            var container = containerBuilder.Build();

            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gold API V1");
            });
        }
    }
}
