using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCWEBAPICONSUM
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default123",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "CreateAjax", id = UrlParameter.Optional },
                namespaces: new[] { "MVCWEBAPICONSUM.Controllers" }
            );
        }
    }
}
