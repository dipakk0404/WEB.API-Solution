using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JwtAuthenticationMvc.Models;
using System.Net.Http.Formatting;

namespace JwtAuthenticationMvc.Controllers
{
    public class DefaultController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string UserName, string Password)
        {
            string token = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri("http://localhost:64957/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Account/Login?UserName=" + UserName + "&Password=" + Password);

                if (response.IsSuccessStatusCode)
                {
                    var resultmessage = response.Content.ReadAsStringAsync().Result;

                    token = JsonConvert.DeserializeObject<string>(resultmessage);

                    Session["Token"] = token;

                    ViewBag.Result = token;

                }
            }

            return View();
        }

        [HttpGet]       
        public async Task<ActionResult> Load()
        {
            IEnumerable<Employee> Emp = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();

                client.BaseAddress = new Uri("http://localhost:64957/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["Token"].ToString());
                var response = await client.GetAsync("Account/Get");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<Employee>>();
                    Emp = result.AsEnumerable<Employee>();

                }
                else
                {
                    Emp = Enumerable.DefaultIfEmpty<Employee>(null);
                    ModelState.AddModelError("Failed", "There is Ocurrence of Error Please Look into it");
                }

                return View(Emp);

            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            IEnumerable<Employee> Emp = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();

                client.BaseAddress = new Uri("http://localhost:64957/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["Token"].ToString());
                var response = await client.GetAsync("Account/Get");
                if (response.IsSuccessStatusCode)
                {
                    var Result =await response.Content.ReadAsAsync<List<Employee>>();
                    Emp = Result.AsEnumerable<Employee>();
                }
                else
                {
                    Emp =Enumerable.DefaultIfEmpty<Employee>(null);
                    ModelState.AddModelError("Loading Failed", "There is not available with this Id");
                }

                Employee em=Emp.FirstOrDefault(s=>s.Id==id);
                return View(em);

            }

            
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View(); 
        }
        [HttpPost]
        public async Task<ActionResult> Create(Employee Emp)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri("http://localhost:64957/api/Account");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Session["Token"].ToString());
                var Response =await client.PostAsJsonAsync<Employee>("Account",Emp);

                if (Response.IsSuccessStatusCode)
                {
                    ViewBag.Result = "Created SuccessFully";
                }
                else
                {
                    ViewBag.Result = "Create Failed";
                }




            }
                return View();
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            List<Employee> Emp = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();

                client.BaseAddress = new Uri("http://localhost:64957/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["Token"].ToString());
                var response = await client.GetAsync("Account/Put");
                if (response.IsSuccessStatusCode)
                {

                    var Result = response.Content.ReadAsStringAsync().Result;
                    Emp = JsonConvert.DeserializeObject<List<Employee>>(Result);
                }
                else
                {
                    Emp = null;
                    ModelState.AddModelError("Loading Failed", "There is not available with this Id");
                }
                Employee em = Emp.FirstOrDefault(s => s.Id == id);
                return View(em);

            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(int id,Employee emp)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri("http://localhost:64957/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["Token"].ToString());
                var Response = await client.PutAsJsonAsync("Account/Put/Id="+id.ToString(), emp);

                if (Response.IsSuccessStatusCode)
                {
                    ViewBag.Result = "Edited SuccessFully";
                }
                else
                {
                    ViewBag.Result = "Edited Failed";
                }


                return View();

            }


        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<ActionResult> Delete_Get(int id)
        {
            List<Employee> Emp = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri("http://localhost:64957/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["Token"].ToString());
                var response = await client.GetAsync("Account/Delete");
                if (response.IsSuccessStatusCode)
                {

                    var Result = response.Content.ReadAsAsync<List<Employee>>();
                    Emp=Result.Result;
                    
                    
                }
                else
                {
                    Emp = null;
                    ModelState.AddModelError("Loading Failed", "There is not available with this Id");
                }

                Employee em = Emp.FirstOrDefault(s => s.Id == id);
                return View(em);

            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> Delete_Post(int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri("http://localhost:64957/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["Token"].ToString());
                var Response = await client.DeleteAsync("Account/Delete/Id="+id.ToString());

                if (Response.IsSuccessStatusCode)
                {
                    ViewBag.Result = "Deleted SuccessFully";
                }
                else
                {
                    ViewBag.Result = "Delete Failed";
                }

                return View();


            }


        }



    }
}