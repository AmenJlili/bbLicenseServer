using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LicenceApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Log4net.createlog("Register", "Start");
            //var cors = new System.Web.Http.Cors.EnableCorsAttribute("http://localhost:6009", "*", "*");
           // var cors = new System.Web.Http.Cors.EnableCorsAttribute("http://localhost:9000", "*", "*");
           // var cors = new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);

            // Log4net.createlog("Register", "EnableCors");
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
