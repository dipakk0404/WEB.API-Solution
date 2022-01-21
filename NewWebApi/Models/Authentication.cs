using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Text;
using System.Security.Claims;
using NewWebApi.Controllers;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Net.Http.Headers;
using System.Web.Http.Results;

namespace NewWebApi.Models
{
    public class Authentication:AuthorizationFilterAttribute,IAuthenticationFilter
    {
        string UserName, Password;
        ClaimsPrincipal claims;
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else if (string.IsNullOrEmpty(actionContext.Request.Headers.Authorization.Parameter))
            {
                actionContext.Response = actionContext.Request.CreateResponse( HttpStatusCode.NotFound);
            }
            else
            {
                string Auth=actionContext.Request.Headers.Authorization.Parameter;


                claims=TokenManager.GetPrincipal(Auth);
                
            }

        }
        public async Task AuthenticateAsync(HttpAuthenticationContext context,CancellationToken cancellationToken)
        {
            context.Principal = claims;

        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var result = await context.Result.ExecuteAsync(cancellationToken);
            if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic", "realm=localhost"));
                context.Result = new ResponseMessageResult(result);
            }
        }

    }
}