using MVC_API_Consumption.Models;
using MVCWEBAPICONSUM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVCWEBAPICONSUM.Controllers
{
    public class DefaultController : Controller
    {
        HttpClient htp = new HttpClient();
        // GET: Default
        
        public ActionResult CreateAjax()
        {
            return View();
        }

        public ActionResult AjaxAction()
        {

            return View();
        }
        public ActionResult Index()
        {

            IEnumerable<StudentData> stud = null;

            
            htp.BaseAddress = new Uri("http://localhost:56415/api/Students");
            var ResponseTask =htp.GetAsync("Students");
            ResponseTask.Wait();

            var result = ResponseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<StudentData>>();
                readTask.Wait();
                stud = readTask.Result.AsEnumerable<StudentData>();
            }
            else
            {
                stud = Enumerable.Empty<StudentData>();

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

            }

           

            return View(stud);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(StudentData s)
        {
            htp.BaseAddress = new Uri("http://localhost:56415/api/Students");
            var responseTask=htp.PostAsJsonAsync("Students",s);
            responseTask.Wait();

            var result = responseTask.Result;
            if(result.IsSuccessStatusCode)
            { 
         
                ViewBag.msg = "Created SuccessFully";
            }
            else
            {
                ViewBag.msg = "Creation Failed";
            }

            return View();
        }
        [HttpGet]
        public ActionResult Update(int ID)
        {
            StudentData Stud =null;
            htp.BaseAddress = new Uri("http://localhost:56415/api/Students");
            var ReadStatus=htp.GetAsync("Students"+"?ID="+ID.ToString());
            ReadStatus.Wait();
            var result = ReadStatus.Result;
            if (result.IsSuccessStatusCode)
            {
                var ReadResult = result.Content.ReadAsAsync<StudentData>();
                ReadResult.Wait();
                Stud=ReadResult.Result;
                ViewBag.msg = "Loaded SuccessFully";
            }
            else
            {
                ViewBag.msg = "Loading Failed";
            }


            return View(Stud);
        }
        [HttpPost]
        public ActionResult Update(int ID,StudentData ss)
        {
            htp.BaseAddress = new Uri("http://localhost:56415/api/Students?ID=");
            var responseTask = htp.PutAsJsonAsync("Students"+"?ID="+ID.ToString(), ss);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                ViewBag.msg = "Updated SuccessFully";
            }
            else
            {
                ViewBag.msg = "Updation Failed";
            }
            return View();

        }
        [ActionName("Delete")]
        [HttpGet]
        public ActionResult Delete_Get(int ID)
        {
            StudentData Stud = null;
            htp.BaseAddress = new Uri("http://localhost:56415/api/Students");
            var ReadStatus = htp.GetAsync("Students"+"?ID="+ID.ToString());
            ReadStatus.Wait();
            var result = ReadStatus.Result;
            if (result.IsSuccessStatusCode)
            {
                var ReadResult = result.Content.ReadAsAsync<StudentData>();
                ReadResult.Wait();
                Stud = ReadResult.Result;
                ViewBag.msg = "Loaded SuccessFully";
            }
            else
            {
                ViewBag.msg = "Loading Failed";
            }


            return View(Stud);
        }
        [ActionName("Delete")]
        public ActionResult Delete_Post(int ID)
        {
            htp.BaseAddress = new Uri("http://localhost:56415/api/Students");
            var ReadStatus = htp.DeleteAsync("Students"+"?ID="+ID.ToString());
            ReadStatus.Wait();
            var result = ReadStatus.Result;
            if (result.IsSuccessStatusCode)
            {
                ViewBag.msg = "Deleted SuccessFully";
            }
            else
            {
                ViewBag.msg = "Deleted Failed";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Details(int ID)
        {
            StudentData Stud = null;
            htp.BaseAddress = new Uri("http://localhost:56415/api/Students");
            var ReadStatus = htp.GetAsync("Students"+"?ID="+ID.ToString());
            ReadStatus.Wait();
            var result = ReadStatus.Result;
            if (result.IsSuccessStatusCode)
            {
                var ReadResult = result.Content.ReadAsAsync<StudentData>();
                ReadResult.Wait();
                Stud = ReadResult.Result;
                ViewBag.msg = "Loaded SuccessFully";
            }
            else
            {
                ViewBag.msg = "Loading Failed";
            }


            return View(Stud);

            
        }
    }
}