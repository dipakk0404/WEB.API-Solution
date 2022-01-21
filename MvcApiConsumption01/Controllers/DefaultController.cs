using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using MvcApiConsumption01.Models;


namespace MvcApiConsumption01.Controllers
{
    
    public class DefaultController : Controller
    {
        public ActionResult AjaxIndex()
        {
            return View();

        }
        // GET: Default
        public ActionResult Index()
        {
           IEnumerable<Student> stud = null;

            HttpClient http = new HttpClient();
            
            http.BaseAddress = new Uri("http://localhost:57487/api/Students");
            //HTTP GET
            var responseTask = http.GetAsync("Students");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Student>>();
                readTask.Wait();

                stud = readTask.Result;
            }
            else //web api sent error response 
            {
                //log response status here..

                stud = Enumerable.Empty<Student>();

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
        
              return View(stud);
            
        }
        public ActionResult Create(Student s)
        {

            HttpClient http = new HttpClient();

            http.BaseAddress = new Uri("http://localhost:57487/api/Students");
            //HTTP GET
            var responseTask = http.PostAsJsonAsync("Students",s);
            responseTask.Wait();

            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                ViewBag.result = "Data insert Succeed";

            }
            else
            {
                ViewBag.result = "Data insert Failed";
            }



            return View();

        }
        public ActionResult Details(int id)
        {

            IEnumerable<Student> stud = null;

            HttpClient http = new HttpClient();

            http.BaseAddress = new Uri("http://localhost:57487/api/Students");
            //HTTP GET
            var responseTask = http.GetAsync("Students");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Student>>();
                readTask.Wait();

                stud = readTask.Result;
            }
            else //web api sent error response 
            {
                //log response status here..

                stud = Enumerable.Empty<Student>();

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }

            Student ss = stud.FirstOrDefault(s => s.Id == id);

            return View(ss);

        }

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit_Get(int id)
        {
            IEnumerable<Student> stud = null;

            HttpClient http = new HttpClient();

            http.BaseAddress = new Uri("http://localhost:57487/api/Students");
            //HTTP GET
            var responseTask = http.GetAsync("Students");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Student>>();
                readTask.Wait();

                stud = readTask.Result;
            }
            else //web api sent error response 
            {
                //log response status here..

                stud = Enumerable.Empty<Student>();

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }

            Student ss = stud.FirstOrDefault(s => s.Id == id);


            return View(ss);

        }
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(int Id,Student s)
        {

            HttpClient http = new HttpClient();

            http.BaseAddress = new Uri("http://localhost:57487/api/Students");
            //HTTP GET
            var responseTask = http.PutAsJsonAsync<Student>("Students?Id="+Id.ToString(),s);
            responseTask.Wait();

            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                ViewBag.result = "Data updated Successfully";

            }
            else
            {
                ViewBag.result = "Data updated Successfully";
            }
            return View();

        }

        [ActionName("Delete")]
        [HttpGet]
        public ActionResult Delete(int Id)
        {

            IEnumerable<Student> stud = null;

            HttpClient http = new HttpClient();

            http.BaseAddress = new Uri("http://localhost:57487/api/Students");
            //HTTP GET
            var responseTask = http.GetAsync("Students");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Student>>();
                readTask.Wait();

                stud = readTask.Result;
            }
            else //web api sent error response 
            {
                //log response status here..

                stud = Enumerable.Empty<Student>();

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }

            Student ss = stud.FirstOrDefault(s => s.Id == Id);

            return View(ss);

        }

        [ActionName("Delete")]
        [HttpPost]
        public ActionResult Delete_Post(int Id)
        {

            HttpClient http = new HttpClient();

            http.BaseAddress = new Uri("http://localhost:57487/api/Students");
            //HTTP GET
            var responseTask = http.DeleteAsync("Students?Id="+Id.ToString());
            responseTask.Wait();

            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                ViewBag.result = "Record Deleted Successfully";

            }
            else
            {
                ViewBag.result = "Record Deleted Successfully";
            }


            return View();
        }
    }
}