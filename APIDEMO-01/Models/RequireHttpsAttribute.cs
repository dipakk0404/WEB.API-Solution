using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace APIDEMO_01.Models
{
    public class RequireHttpsAttribute:AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme!=Uri.UriSchemeHttps)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Found);
                actionContext.Response.Content = new StringContent("<b> Use Https instead of Http");

                UriBuilder builder = new UriBuilder(actionContext.Request.RequestUri);
                builder.Scheme = Uri.UriSchemeHttps;
                builder.Port = 44353;

                actionContext.Response.Headers.Location = builder.Uri;
                
            }

            base.OnAuthorization(actionContext);
        }
    }
}

