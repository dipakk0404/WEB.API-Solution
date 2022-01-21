using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdoMvcCrud.Models;

namespace AdoMvcCrud.Controllers
{
    public class HomeController : Controller
    {
        StudentHelper help = new StudentHelper();
        // GET: Home
        public ActionResult Index()
        {
            bool b;
            //List<Students>list=help.LoadAll(out b);
            List<Students> list =help.LoadAllWithAdaper(out b);
            if (b)
            {
                ViewBag.Status = "Record Loading SuccessFul";
            }
            else
            {
                ViewBag.Status = "Record Loading Failed";
            }
            return View(list);
        }
        public ActionResult Details(int Id)
        {
            Students stud=help.Details(Id);
            return View(stud);
        }
        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_Get()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post(Students s)
        {
            string stat;
            if (help.CreateAdapter(s,out stat))
            {
                ViewBag.Status = "Student Creation Successful Your Id is="+stat.ToString();
            }
            else
            {
                ViewBag.Status = "Student Creation Failed";
            }
            return View();
        }
        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit_Get(int id)
        {
            Students stud = help.Details(id);
            return View(stud);
        }
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(int id,Students s)
        {
            if (help.Update(id, s))
            {
                ViewBag.Status = "Update SuccessFul";
            }
            else
            {
                ViewBag.Status = "Update Failed";
            }
            return View();
        }
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult Delete_Get(int id)
        {
            Students stud = help.Details(id);
            return View(stud);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_Post(int id)
        {
            if (help.Delete(id))
            {
                ViewBag.Status = "Update SuccessFul";
            }
            else
            {
                ViewBag.Status = "Update Failed";
            } 
            return View();
        }


    }
}