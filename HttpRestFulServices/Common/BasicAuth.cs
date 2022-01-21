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
using System.Web.Mvc;
using HttpRestFulServices.Controllers;
namespace HttpRestFulServices.Common
{
    public class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public List<Users> GetUser()
        {
            List<Users> list = new List<Users>()
            {
                new Users(){Id=1,UserName="Dipak",Password="Dipak@123" },
                new Users(){Id=2,UserName="Amit",Password="Amit@123" },
                new Users(){Id=3,UserName="Rahul",Password="Rahul@123" },
                new Users(){Id=4,UserName="Ramesh",Password="Ramesh@123" },

            };

            return list;

        }
    }

    public class BasicAuth:AuthorizationFilterAttribute
    {
        private const string Realm = "My Realm";
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization==null)
            {
                actionContext.Response=actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);


                if (actionContext.Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    actionContext.Response.Headers.Add("WWW-Authenticate",
                           string.Format("Basic realm=\"{0}\"", Realm));

                }


            }
            else
            {
                string AuthToken=actionContext.Request.Headers.Authorization.Parameter;

                string deToken = Encoding.UTF8.GetString(Convert.FromBase64String(AuthToken));

                string[] Token = deToken.Split(':');
                string UserName=Token[0];
                string Password = Token[1];
                WebApiController c = new WebApiController();

                if (c.Login(UserName,Password))
                {
                    var identity = new GenericIdentity(UserName);

                    IPrincipal principal = new GenericPrincipal(identity,null);

                    Thread.CurrentPrincipal = principal;

                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;

                    }
                    else
                    {
                        actionContext.Response=actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

                    }

                }

            }
        }
    }
}