using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityMvcCrud.Models;

namespace EntityMvcCrud.Controllers
{
    public class HomeController : Controller
    {
        DU2022Context Du = new DU2022Context();
        // GET: Home
        public ActionResult Index()
        {
            List<Student> list=Du.Students.ToList();
            return View(list);
        }
        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_Get()
        {

            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post(Student s)
        {
            Du.Students.Add(s);
            Du.SaveChanges();

            return View();
        }
        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit_Get(int id)
        {
            Student s1=Du.Students.FirstOrDefault(s=>s.id==id);

            return View(s1);
        }
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(int id,Student ss)
        {
            Student s1 = Du.Students.FirstOrDefault(s => s.id == id);
            s1.Name = ss.Name;
            s1.Gender = ss.Gender;
            s1.Age = ss.Age;
            s1.DateOfBirth = ss.DateOfBirth;
            Du.SaveChanges();
            return View();
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            Student s1 = Du.Students.FirstOrDefault(s => s.id == id);

            return View(s1);
        }
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult Delete_Get(int id)
        {
            Student s1 = Du.Students.FirstOrDefault(s => s.id == id);

            return View(s1);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_Post(int id)
        {
            Student s1 = Du.Students.FirstOrDefault(s => s.id == id);
            Du.Students.Remove(s1);
            Du.SaveChanges();
            return View();
        }



    }
}