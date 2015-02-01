using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Security.Cookies;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using Hourglass.Models;
using System.Threading.Tasks;

namespace Hourglass
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Setup configuration sources.
            Configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add EF services to the services container.
            services.AddEntityFramework(Configuration)
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>();

            // Add Identity services to the services container.
            services.AddDefaultIdentity<ApplicationDbContext, ApplicationUser, IdentityRole>(Configuration);

            // Add MVC services to the services container.
            services.AddMvc();

            // Uncomment the following line to add Web API servcies which makes it easier to port Web API 2 controllers.
            // You need to add Microsoft.AspNet.Mvc.WebApiCompatShim package to project.json
            // services.AddWebApiConventions();

        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            // Configure the HTTP request pipeline.
            // Add the console logger.
            loggerfactory.AddConsole();

            // Add the following to the request pipeline only in development environment.
            if (string.Equals(env.EnvironmentName, "Development", StringComparison.OrdinalIgnoreCase))
            {
                app.UseBrowserLink();
                app.UseErrorPage(ErrorPageOptions.ShowAll);
                app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // send the request to the following path or controller action.
                app.UseErrorHandler("/Home/Error");
            }

            // Add static files to the request pipeline.
            app.UseStaticFiles();

            // Add cookie-based authentication to the request pipeline.
            app.UseIdentity();

            
            //IApplicationBuilder IApplicationBuilder.Use(Func<RequestDelegate,RequestDelegate> middleware)
            //all three are equivlent- Use takes one argument of type Func<>. That Func<> has one input of type
            //RequestDelegate and that Func<> returns a result of type RequestDelegate . In the first case
            //I am returning a null RequestDelegate.
            //In the second case I am implicitly returning the RequestDelegate: ctx => { return null; }
            //In the third case I am explicitely returning the RequestDelegate via: return ctx => { return null; };
            //Note in the second and thord case the RequestDelegate iteslf is returning null.
            app.Use(ctx => { return null; });
            app.Use(next => ctx => { return null; });
            app.Use(next => { return ctx => { return null; }; });

            //a Func<> that takes no arguments and returns a a result that is set to null
            Func<Task> fn = () => { return null; };
            //IApplicationBuilder IApplicationBuilder.Use(Func<HttpContext,Func<Task>,Task> middleware)
            //note ctx and next types of HttpContext and Func<Task> are inferred by the signature of Use()
            app.Use((ctx, next) => { return null; });

            //void IApplicationBuilder.Run(RequestDelegate handler)
            //Run takes an input of type RequestDelegate specified as: ctx => { return null; }
            app.Run(ctx => { return null; });



            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });
        }
    }
}
