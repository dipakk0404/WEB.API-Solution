using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace NetWebApi_01
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes

            JsonMediaTypeFormatter jsonformat = config.Formatters.JsonFormatter;
            config.Formatters.Insert(0,jsonformat);
            

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:57692","*","*");
            //config.EnableCors(cors);

            //JsonMediaTypeFormatter jsonFormat = config.Formatters.JsonFormatter;

        }
    }
}
