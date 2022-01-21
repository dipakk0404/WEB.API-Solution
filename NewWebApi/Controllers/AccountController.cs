using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewWebApi.Models;

namespace NewWebApi.Controllers
{
    public class AccountController : ApiController
    {
        DU2022Model Db = new DU2022Model();
        [HttpGet]
        [Authentication]
        public HttpResponseMessage Login(string username,string password)
        {
            bool Isvalid=Db.Users.Any(s=>s.UserName==username&&s.Password==password);
            if (Isvalid)
            {
                return Request.CreateResponse(HttpStatusCode.OK,TokenManager.GenerateToken(username));
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized,"Invalid Credential");
            }
        }

        [HttpGet]
        [Authentication]
        public string GetDetail()
        {
            return "I LOVE YOU";
        }

    }
   
}
