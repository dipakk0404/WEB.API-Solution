using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Routing;

namespace WebApi01
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.EnableCors();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            IHttpRoute CustomRoute =config.Routes.CreateRoute("api/{controller}/{id}",new {id=RouteParameter.Optional },null);

            config.Routes.Add("Default",CustomRoute);

            JsonMediaTypeFormatter JsonFormat = config.Formatters.JsonFormatter;

            




        }

    }
}
