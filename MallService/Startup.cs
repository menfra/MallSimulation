using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAcess.DataModels;
using DataAcess.Interfaces;
using DataAcess.DataServices;
using MallService.MallBusinessLayer;
using MallService.Extensions;
using DataAcess.Enums;
using System.Timers;

namespace MallService
{
    public class Startup
    {
        private readonly Timer timer;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigValues.MallOpenCloseDuration = Configuration["MallService:Settings:OpenCloseDuration"];
            ConfigValues.MallOpenedStatus = Configuration["MallService:Settings:OpenedStatus"].Equals("Opened") ? States.Opened : States.Closed;
            ConfigValues.InMallCustomers = Configuration["MallService:Settings:Capacity"].Equals("UnLimited") ? Capacity.UnLimited : Capacity.Limited;
            
            int.TryParse(ConfigValues.MallOpenCloseDuration, out int duration);
            timer = new Timer(duration);
            timer.Elapsed += OnTimedEvent;
            timer.Start();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (ConfigValues.FirstTime)
            {
                ConfigValues.FirstTime = false;
                return;
            }

            if (ConfigValues.MallOpenedStatus == States.Opened)
                ConfigValues.MallOpenedStatus = States.Closed;
            else
                ConfigValues.MallOpenedStatus = States.Opened;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IEntity, Mall>();
            services.AddScoped<IDataServices, MongoDataServices>();
            services.AddTransient<IMallBusiness, MallBusiness>();

            // Register the Swagger Generator service. This service is responsible for genrating Swagger Documents.
            // Note: Add this service at the end after AddMvc() or AddMvcCore().
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Jedox Mall API (Mall Service)",
                    Version = "v1",
                    Description = "An API to Simulate a virtual Mall",
                    Contact = new OpenApiContact
                    {
                        Name = "Jedox Mall API Test",
                        Email = "info@jedox.com",
                        Url = new Uri("https://jedox.com"),
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mall Service of the Mall API API V1");

                //// To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
                //c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
