using APIDEMO_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace APIDEMO_01.Models
{
    public class BasicAuthentication:AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                var token = actionContext.Request.Headers.Authorization.Parameter;

                string Decoded=Encoding.UTF8.GetString(Convert.FromBase64String(token));

                string[] Creds=Decoded.Split(':');

                string username = Creds[0];
                string password = Creds[1];

                if (BasicAuth.Login(username, password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username),null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

            }


            base.OnAuthorization(actionContext);
        }
    }
}

public class BasicAuth
{
    static DUKContext du = new DUKContext();
    public static bool Login(string UserName, string Password)
    {
        var IsExist = du.Users.Any(s => s.UserName.ToLower() == UserName.ToLower() && s.Password.ToLower() == Password.ToLower());

        if (IsExist)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}