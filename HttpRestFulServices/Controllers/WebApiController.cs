using HttpRestFulServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using HttpRestFulServices.Common;

namespace HttpRestFulServices.Controllers
{
    [RoutePrefix("WebApi")]
    public class WebApiController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            using (STDContext st=new STDContext())
            {
               var Reply=st.Employees.ToList();
               return Request.CreateResponse(HttpStatusCode.OK,Reply);

            }

        }
       
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            using (STDContext st=new STDContext())
            {
               var Emp=st.Employees.Find(id);
                if (Emp != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Emp);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Employee with Id "+id.ToString()+"Does not Found");
                }
            }
        }
        [HttpGet]
        [Route("~/{id}/Course")]
        public IHttpActionResult GetCourse(int id)
        {
            return Ok(id);
        }

        public HttpResponseMessage Post(Employee emp)
        {
            try
            {
                using (STDContext st = new STDContext())
                {
                    st.Employees.Add(emp);
                    st.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, emp);
                    return message;
                }
            }
            catch (Exception Ex)
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Record Insert Failed"+Ex.Message);
                return message;
            }
        }

        public HttpResponseMessage Put(int id,Employee Emp)
        {
            try
            {
                using (STDContext st = new STDContext())
                {
                    Employee em = st.Employees.Find(id);
                    em.Name = Emp.Name;
                    em.Gender = Emp.Gender;
                    em.DOB = Emp.DOB;
                    em.Salary = Emp.Salary;
                    em.DpId = Emp.DpId;
                    st.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK,"Update Successful");

                }
            }
            catch (Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotModified,"Update Failed"+Ex.Message);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            using (STDContext st=new STDContext())
            {
                Employee emp=st.Employees.Find(id);
                st.Employees.Remove(emp);
                st.SaveChanges();

                if (st.Employees.Find(id) == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Deleted Successfully");
                }
                else
                {

                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,"Delete Failed");
                }

            }
        }
        [HttpGet]
        [Route("~/{UserName}/{Password}/Login")]
        public bool Login(string UserName,string Password)
        {
            Users u = new Users();

            return u.GetUser().Any(a => a.UserName == UserName && a.Password == Password);
        }
    }
}
