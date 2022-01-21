using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using HttpRestFulServices.Models;

namespace HttpRestFulServices.Controllers
{
    public class MVCController : Controller
    {
        // GET: MVC
        public ActionResult Index()
        {
            using (HttpClient HT = new HttpClient())
            {
                IEnumerable<Emp> Em = null;

                HT.BaseAddress = new Uri("http://localhost:49173/api/WebApi");
                var response = HT.GetAsync("WebApi");
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var OutResult = result.Content.ReadAsAsync<List<Emp>>();
                    OutResult.Wait();
                    Em = OutResult.Result;
                }
                else
                {
                    Em = Enumerable.Empty<Emp>();
                    ModelState.AddModelError(string.Empty, "Please Contact With Administrator");
                }



                return View(Em);
            }
        }
        public ActionResult Details(int id)
        {
            using (HttpClient HT = new HttpClient())
            {
                Emp Stud = new Emp();
                HT.BaseAddress = new Uri("http://localhost:49173/api/WebApi");
                var Response = HT.GetAsync("WebApi/"+id.ToString());
                Response.Wait();
                var result = Response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var OutResult = result.Content.ReadAsAsync<Emp>();
                    OutResult.Wait();
                    Stud = OutResult.Result;

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please Contact to Administrator");

                }

                return View(Stud);
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (HttpClient HT = new HttpClient())
            {
                Emp Stud = new Emp();
                HT.BaseAddress = new Uri("http://localhost:49173/api/WebApi");
                var Response = HT.GetAsync("WebApi/" + id.ToString());
                Response.Wait();
                var result = Response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var OutResult = result.Content.ReadAsAsync<Emp>();
                    OutResult.Wait();
                    Stud = OutResult.Result;

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please Contact to Administrator");

                }

                return View(Stud);
            }
        }
        [HttpPost]
        public ActionResult Edit(int ID,Emp model)
        {
            using (HttpClient HT = new HttpClient())
            {
                HT.BaseAddress = new Uri("http://localhost:49173/api/WebApi");
               var Response= HT.PutAsJsonAsync<Emp>("WebApi?id="+ID.ToString(),model);
                Response.Wait();
                var result=Response.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Status = "Edit Success";
                }
                else
                {
                    ViewBag.Status = "Edit Failed";
                }

                return View();
            }

        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Emp Model)
        {
            using (HttpClient Ht = new HttpClient())
            {
                Ht.BaseAddress = new Uri("http://localhost:49173/api/WebApi");
               var Response= Ht.PostAsJsonAsync("WebApi",Model);
                Response.Wait();
                var result=Response.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Status = "Post Action Successful";
                }
                else
                {

                    ViewBag.Status = "Post Action Failed";
                }



            }
                return View();
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult Delete_Get(int id)
        {

            using (HttpClient HT = new HttpClient())
            {
                Emp Stud = new Emp();
                HT.BaseAddress = new Uri("http://localhost:49173/api/WebApi");
                var Response = HT.GetAsync("WebApi/"+id.ToString());
                Response.Wait();
                var result = Response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var OutResult = result.Content.ReadAsAsync<Emp>();
                    OutResult.Wait();
                    Stud = OutResult.Result;

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please Contact to Administrator");

                }

                return View(Stud);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_Post(int ID)
        {
            using (HttpClient HT = new HttpClient())
            {
                HT.BaseAddress = new Uri("http://localhost:49173/api/WebApi");
                var response=HT.DeleteAsync("WebApi/"+ID.ToString());
                response.Wait();
                var result=response.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Status = "Delete SuccessFul";
                }
                else
                {
                    ViewBag.Status = "Delete Failed";
                }
            }
                return View();
        }
    }
}