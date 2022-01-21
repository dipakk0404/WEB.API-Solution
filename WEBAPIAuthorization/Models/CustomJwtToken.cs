using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace WEBAPIAuthorization.Models
{
    public class CustomJwtToken : AuthorizationFilterAttribute, IAuthenticationFilter
    {
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            AuthenticationHeaderValue Authentication = context.Request.Headers.Authorization;

            if (Authentication == null)
            {
             context.ErrorResult = new AuthorizationFailure("Misssing Authentication Header",context.Request);
              return;
            }

            if (Authentication.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthorizationFailure("Invalid Token", context.Request);
                return; 
            }

            if (string.IsNullOrEmpty(Authentication.Parameter))
            {
                context.ErrorResult = new AuthorizationFailure("Token Missing", context.Request);
                return;
            }

            context.Principal = TokenManager.GetPrincipal(Authentication.Parameter);
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var Result = await context.Result.ExecuteAsync(cancellationToken);
            if (Result.StatusCode==HttpStatusCode.Unauthorized)
            {
                Result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic","realm=localhost"));
                context.Result = new ResponseMessageResult(Result);
            }
        }
    }
    public class AuthorizationFailure : IHttpActionResult
    {
        public string ReasonPhrase;
        public HttpRequestMessage Request;

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        public AuthorizationFailure(string reasonphrase,HttpRequestMessage request)
        {
            ReasonPhrase = reasonphrase;
            Request = request;
        }
        public HttpResponseMessage Execute()
        {
            HttpResponseMessage ResponseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);

            ResponseMessage.RequestMessage = Request;
            ResponseMessage.ReasonPhrase = ReasonPhrase;

            return ResponseMessage;

        }
    }
}
