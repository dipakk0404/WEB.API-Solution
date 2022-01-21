using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WEBAPIAuthorization.Models;

namespace WEBAPIAuthorization.Controllers
{
    public class AccountController : ApiController
    {
        EmpContext Db = new EmpContext();

        [HttpGet]
        public HttpResponseMessage Login(string UserName,string PassWord)
        {
            bool IsExist=Db.Users.Any(s=> s.UserName.ToLower()==UserName.ToLower()&&s.Password.ToLower()==PassWord.ToLower());
            if (IsExist)
            {
                return Request.CreateResponse(HttpStatusCode.OK,TokenManager.GenerateToken(UserName));
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway,"Invalid User");
            }
        }

        [HttpGet]
        [CustomJwtToken]
        public IHttpActionResult Get()
        {
            List<Employee>list=Db.Employees.ToList();

            return Ok(list);
        }

        [HttpGet]
        [CustomJwtToken]
        public IHttpActionResult Get(int id)
        {
            Employee Emp =Db.Employees.FirstOrDefault(s=>s.Id==id);

            return Ok(Emp);
        }

        
        
        [CustomJwtToken]
        public IHttpActionResult PostEmployee(Employee Emp)
        {
            Db.Employees.Add(Emp);
            Db.SaveChanges();

            return Created<Employee>("Created in Du2022 Database",Emp);
        }

        [HttpGet]
        [CustomJwtToken]
        public IHttpActionResult Put(int id)
        {
            Employee Emp = Db.Employees.FirstOrDefault(s => s.Id == id);

            return Ok(Emp);
        }
        [CustomJwtToken]
        public IHttpActionResult Put(int id,Employee emp)
        {
            Employee Emp = Db.Employees.FirstOrDefault(s => s.Id == id);
            Emp.EmpName = emp.EmpName;
            Emp.Gender = emp.Gender;
            Emp.Email = emp.Email;
            Emp.Age = emp.Age;
            Db.SaveChanges();

            return Content<Employee>(HttpStatusCode.NoContent,emp);
        }

        [ActionName("Delete")]
        [HttpGet]
        [CustomJwtToken]
        public IHttpActionResult Delete_Get(int id)
        {

            Employee Emp = Db.Employees.FirstOrDefault(s => s.Id == id);

            return Ok(Emp);
        }

        [ActionName("Delete")]
        [HttpPost]
        [CustomJwtToken]
        public IHttpActionResult Delete_Post(int id)
        {
            Employee Emp = Db.Employees.FirstOrDefault(s => s.Id == id);
            Db.Employees.Remove(Emp);

            return Ok();
            
        }

        
        [HttpPost]
        [CustomJwtToken]
        public IHttpActionResult MyDelete_Post(int id)
        {
            Employee Emp = Db.Employees.FirstOrDefault(s => s.Id == id);
            Db.Employees.Remove(Emp);

            return Ok();

        }


    }
}
