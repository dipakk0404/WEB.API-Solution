using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrearerTokenDemo.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Update()
        {
            return View();
        }

        public ActionResult Create()
        {
            return view();
        }

        public ActionResult Delete()
        {
            return view();
        }
    }
}