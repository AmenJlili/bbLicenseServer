using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


[assembly: OwinStartup(typeof(LicenceApi.Startup))]

namespace LicenceApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Log4net.createlog("Configuration", "Start of application");
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,

                //The Path For generating the Toekn
                TokenEndpointPath = new PathString("/token"),

                //Setting the Token Expired Time (24 hours)
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),

                //AuthorizationServerProvider class will validate the user credentials
                Provider = new AuthorizationServerProvider()
            };
            // Log4net.createlog("Configuration", "token genrated");
            //Token Generations
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            // Log4net.createlog("Configuration", "WebApiConfig.Register");
            WebApiConfig.Register(config);
        }

    }
}