using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using BrearerTokenDemo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BrearerTokenDemo.Controllers
{
    public class HomeController : Controller
    {
        
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterBindingModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55403/api/Account");
                var Response = client.PostAsJsonAsync("Account/Register", model);
                Response.Wait();
                var result = Response.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Register = "You Are Registered SuccessFul";
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Register = "Registration UnsuccessFul";
                    return View();
                }


                
            }
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName,string Password)
        {
            string Token = string.Empty;

            try
            {
                var keyvalues = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("username",UserName),
                    new KeyValuePair<string, string>("password",Password),
                    new KeyValuePair<string, string>("grant_type","password")
                };

                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:55403/" + "Token");
                request.Content = new FormUrlEncodedContent(keyvalues);

                HttpClient client = new HttpClient();
                var response = client.SendAsync(request).Result;

                using (HttpContent content = response.Content)
                {
                    var result = content.ReadAsStringAsync();
                    JObject jsonobject = JsonConvert.DeserializeObject<dynamic>(result.Result);
                    dynamic ExpirationTime = jsonobject.Value<DateTime>(".expires");
                    dynamic username = jsonobject.Value<string>("username");
                    Token = jsonobject.Value<string>("access_token");
                    var TokenExpiration = ExpirationTime;

                    Session["Token"] = Token;
                    
                    if (Token != null && Token != string.Empty)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Token","No User Found,Please Make Sure That Your UserName and Password Is Correct Or Not");
                        return View();
                    }
                    
                }
            }
            catch (Exception Ex)
            {
                ViewBag.Result = "Failed To Generate Token "+Ex.Message;
                return View();
            }           
        }
       
        public ActionResult Index()
        {
            IEnumerable<Employee> emp = null;
            using (HttpClient client =new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55403/");
                client.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("Bearer",Session["Token"].ToString());
                var response=client.GetAsync("api/Values/");
                response.Wait();
                var result=response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var EmpList = result.Content.ReadAsAsync<List<Employee>>();
                    EmpList.Wait();
                    emp = EmpList.Result;

                }
                else
                {
                    ModelState.AddModelError("UnAuthorized Response","Please make Sure Your Credentials Are Valid or Not");
                }
            }
                return View(emp);
        }
        public ActionResult Details(int id)
        {
            Employee emp = null;
            using (var client =new HttpClient())
            {
                client.BaseAddress=new Uri("http://localhost:55403/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Session["Token"].ToString());
                var response =client.GetAsync("api/Values?id="+id.ToString());
                response.Wait();
                var result=response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var Output = result.Content.ReadAsAsync<Employee>();
                    Output.Wait();
                    emp = Output.Result;
                }
                else
                {
                    ModelState.AddModelError("UnAuthorized Response","Inavlid UserName and Password");
                }

                return View(emp);
            }

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee model)
        {
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55403/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Session["Token"].ToString());
                var response=client.PostAsJsonAsync<Employee>("api/Values",model);
                response.Wait();
                var result=response.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.RSP = "Created SuccessFully";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.RSP = "Creation Failed";
                    return View();
                }

                
            }

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            Employee emp = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55403/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["Token"].ToString());
                var response = client.GetAsync("api/Values?id=" + id.ToString());
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var Output = result.Content.ReadAsAsync<Employee>();
                    Output.Wait();
                    emp = Output.Result;
                }
                else
                {
                    ModelState.AddModelError("UnAuthorized Response", "Inavlid UserName And Password");
                }

                return View(emp);
            }

            
        }
        [HttpPost]
        public ActionResult Edit(int id,Employee emp)
        {
            using (var client =new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55403/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Session["Token"].ToString());
                var resposne =client.PutAsJsonAsync<Employee>("api/Values?id="+id.ToString(),emp);
                resposne.Wait();
                var result=resposne.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.value = "Record Update Successful";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.value = "Record Update Failed";
                    return View();
                }
            }
                
        }
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult Delete_Get(int id)
        {

            Employee emp = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55403/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["Token"].ToString());
                var response = client.GetAsync("api/Values?id=" + id.ToString());
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var Output = result.Content.ReadAsAsync<Employee>();
                    Output.Wait();
                    emp = Output.Result;
                }
                else
                {
                    ModelState.AddModelError("UnAuthorized Response", "Inavlid UserName And Password");
                }

                return View(emp);
            }

            
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_Post(int id)
        {
            using (var Client =new HttpClient())
            {
                Client.BaseAddress = new Uri("http://localhost:55403/");
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Session["Token"].ToString());
                var Response = Client.DeleteAsync("api/Values?id="+id.ToString());
                Response.Wait();
                var result = Response.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Msg = "Delete Successful";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Msg = "Delete Failed";
                    return View();
                }

            }
        }
    }
}
