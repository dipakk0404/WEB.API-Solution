using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using APIDEMO_01.Models;


namespace APIDEMO_01.Controllers
{
    [RequireHttpsAttribute]
    [BasicAuthentication]
    public class EmpController : ApiController
    {
        static DUKContext Db = new DUKContext();

        [HttpGet]
        public HttpResponseMessage Index()
        {
            
            var Entity=Db.Employees.ToList();

            if (Entity != null)
            {
               var msg=Request.CreateResponse(HttpStatusCode.OK, Entity);
                msg.Headers.Add("Location",Request.RequestUri.ToString());

                return msg;

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"No Employees Found Under this method");
            }
        }

        [HttpGet]
        public HttpResponseMessage Index(int id)
        {
           var Employee=Db.Employees.FirstOrDefault(s=>s.id==id);

            if (Employee != null)
            {
                var msg=Request.CreateResponse(HttpStatusCode.OK, Employee);
                msg.Headers.Add("Location",Request.RequestUri.ToString());

                return msg;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"No Employee Found with id="+id.ToString());
            }
        }
        public HttpResponseMessage PostEmp(Employee emp)
        {
            if (emp != null)
            {
                Db.Employees.Add(emp);
                Db.SaveChanges();

                var response=Request.CreateResponse(HttpStatusCode.Created, emp);

                response.Headers.Add("Location",Request.RequestUri.ToString());

                return response;
            }
            else
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Not Created");
            }

        }
        public IHttpActionResult PutEmp(int id)
        {

            var Entity = Db.Employees.FirstOrDefault(s=>s.id==id);

            if (Entity != null)
            {
                Db.Entry(Entity).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();

                return Ok();
            }
            else
            {
                return BadRequest("Entity Not Found");
            }
        }
        public IHttpActionResult DeleteEmp(int id)
        {
            var Entity = Db.Employees.FirstOrDefault(s => s.id == id);

            if (Entity != null)
            {
                Db.Employees.Remove(Entity);
                Db.SaveChanges();

                return Ok(Entity);
            }
            else
            {
                return BadRequest("Entity to be deleted Not found");
            }

        }
    }
}