using Microsoft.Owin.Diagnostics;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katana
{
   
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:3000"))
            {
                Console.WriteLine("starting katana host");
                Console.ReadKey();

            }
        }
    }
 
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
            //app.Use() - (extension)void IAppBuilder.Use(Func<Microsoft.Owin.IOwinContext,Func<Task>,Task> handler)
            //app.Use((oc, nextFunc) => nextFunc());
            //app.Use(async (oc, nextFunc) => await nextFunc());
            app.Use((oc, nextFunc) =>
            {
                //Preprocess
                return nextFunc();
            });

            app.Use((oc, nextFunc) =>
            {
                //Preprocess
                Task t = nextFunc();
                //Postprocess
                return t;
            });

            app.Use(async (oc, nextFunc) =>
            {
                //Preprocess
                await nextFunc();
                //Postprocess
            });

            app.Use<IdentityPage>();
            app.UseIdentityPage();

            ErrorPageOptions options = new ErrorPageOptions();
            options.ShowCookies = true;
            options.ShowEnvironment = true;
            options.ShowHeaders = true;
            options.ShowSourceCode = true;
            options.ShowExceptionDetails = true;
            app.Properties["host.AppMode"] = "development";
            app.UseErrorPage(options);



            //app.Run() - (extension)void IAppBuilder.Run(Func<Microsoft.Owin.IOwinContext,Task> handler)    
            app.Run(oc => oc.Response.WriteAsync("Hello Katana post"));
            app.Run(async oc => await oc.Response.WriteAsync("Hello Katana post"));
            app.Run(oc =>
            {
                //Preprocess

                //string token = c.Authentication.User.Identity.Name;
                return oc.Response.WriteAsync("Hello Katana");
            });

            app.Run(oc =>
            {
                //Preprocess

                //string token = c.Authentication.User.Identity.Name;
                Task t = oc.Response.WriteAsync("Hello Katana");
                //Postprocess
                return t;
            });
            app.Run(async oc =>
            {
                //Preprocess

                //string token = c.Authentication.User.Identity.Name;
                await oc.Response.WriteAsync("Hello Katana");
                //Postprocess
            });

       
            //app.Build();
            //app.New();
            //app.UseCookieAuthentication();
            //app.UseCors();
            //app.UseOAuthAuthorizationServer();
            //app.UseOAuthBearerAuthentication();
            //app.UseOAuthBearerTokens();
            //app.UseTwoFactorRememberBrowserCookie();
            //app.UseTwoFactorSignInCookie();

        }
    }

    public static class AppBuilderExtensions
    {
        public static void UseIdentityPage(this IAppBuilder app)
        {
            app.Use<IdentityPage>();
        }
    }

    public class IdentityPage
    {
        //Func<IDictionary<string, object>, int> returnsInt = c => 2;
        //Func<string, string> returnsString = c => c.ToString();
        //Func<IDictionary<string, object>, Task> appFunc = c => TaskFactory.ReturnTask();



        Func<IDictionary<string, object>, Task> _nextAppFunc;

        public IdentityPage(Func<IDictionary<string, object>, Task> nextAppFunc)
        {
            _nextAppFunc = nextAppFunc;
        }

        public async Task Invoke(IDictionary<string, object> owinEnv)
        {
            _preNextAppFunc();
            await _nextAppFunc(owinEnv);
            _postNextAppFunc();
        }


        public void _preNextAppFunc()
        {
            //write user identity output stream
        }
        public void _postNextAppFunc()
        {
            //write user identity output stream
        }

    }
}
