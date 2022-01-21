using ApiConsumeDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ApiConsumeDemo.Controllers
{
    public class EnggController : Controller
    {
        // GET: Engg
        public ActionResult Index()
        {

            IEnumerable<ApiEngg> Engineers = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62413/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Engineers");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ApiEngg>>();
                    readTask.Wait();

                    Engineers = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    Engineers = Enumerable.Empty<ApiEngg>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(Engineers);
        }
    }
}