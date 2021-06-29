using BusBooking.Business.Authenticate;
using BusBooking.Data.Context;
using BusBooking.Data.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BusBookingAppAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            /*services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BusBookingAppAPI", Version = "v1" });
            });*/
            
            services.AddDbContext<EntityContext>(options => options.UseSqlServer
            (Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<EntityContext>();

            services.AddTransient<IAccount, Account>();
            services.AddTransient<IHome, Home>();
            services.AddTransient<IBusBooking, BusBooking.Business.Authenticate.BusBooking>();
            services.AddTransient<IBusOperator, BusOperator>();
            services.AddTransient<IReadData, ReadData>();
            services.AddTransient<IWriteData, WriteData>();


            services.AddCors(options =>
                {
                    options.AddPolicy(name:"CorsPolicy", builder =>
                    {
                        builder.AllowAnyHeader().
                        AllowAnyMethod().
                        AllowAnyOrigin();
                    });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BusBookingAppAPI v1"));
            }
            app.UseExceptionHandlerMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
