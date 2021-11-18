using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using WarsztatAPI.Tools;

namespace WarsztatAPI
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
            

            services.AddCookiePolicy(opt =>
            {
                opt.MinimumSameSitePolicy = SameSiteMode.Strict;
                opt.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
                opt.ConsentCookie = new CookieBuilder
                {
                    Expiration = TimeSpan.FromMinutes(15),
                    HttpOnly = true,
                    IsEssential = true,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest,
                    SameSite = SameSiteMode.Strict
                };
            });
            services.AddCors();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WarsztatAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WarsztatAPI v1"));
            }
            //app.UseHttpsRedirection();


            app.UseRouting();

            app.UseAuthorization();

            app.UseCookiePolicy();
            app.UseCors(opt => opt.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(ori => true).AllowCredentials());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
