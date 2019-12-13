using DutchTreat.Data;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System.Reflection;
using AutoMapper;

namespace DutchTreat
{ 
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DutchContext>(cfg => {
                cfg.UseSqlite(_config.GetConnectionString("DutchSqliteConnectionString"));
                //cfg.UseSqlServer(_config.GetConnectionString("DutchConnectionString"));
            });

            //services.AddTransient<DutchSeeder>();       
            //support for real mail service     
            services.AddTransient<IMailService, NullMailService>();
            // services.AddAutoMapper();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IDutchRepository, DutchRepository>();
            services.AddMvc(option => option.EnableEndpointRouting = false)
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //.SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                /*this takes a customized error page when something went wrong*/
                //app.UseExceptionHandler("/error");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();
            app.UseNodeModules();

            //app.UseRouting();

            app.UseMvc(cfg => {
                cfg.MapRoute("Default",
                "{controller}/{action}/{id?}",
                new {controller = "App", Action = "Index"});
            });

            // app.UseEndpoints(cfg =>
            // {
            //     cfg.MapControllerRoute("Default",
            //     "{controller}/{action}/{id?}",
            //     new { controller = "App", Action = "Index" });
            // });
        }
    }
}
