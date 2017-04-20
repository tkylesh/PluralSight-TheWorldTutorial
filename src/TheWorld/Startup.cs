using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TheWorld
{
    public class Startup
    {
        //CANT GET APPLICATIONENVIRONMENT TO WORK THE WAY IT DOES IN TUTORIAL
        //configuration object will be used to fill in the arguments of mail service sendmail method
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            //this will take the configuration information and give us a dictionary of name and value pairs we can use
            Configuration = builder.Build();

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //add mvc
            services.AddMvc();

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<WorldContext>();


#if DEBUG
            services.AddScoped<IMailService, DebugMailService>();
#else
            services.AddScoped<IMailService, RealMailService>();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //removing because mvc will handle routing for us
            //app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller="App", action="Index"}
                    );
            });

            //loggerFactory.AddConsole();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync($"Hello World: {context.Request.Path}");
            //});
        }
    }
}
