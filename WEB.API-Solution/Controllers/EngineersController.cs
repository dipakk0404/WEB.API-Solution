using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WEB.API_Solution.Models;

namespace WEB.API_Solution.Controllers
{
    public class EngineersController : ApiController
    {
        private EngineersContext db = new EngineersContext();
        [HttpGet]
        // GET: api/Engineers
        public IQueryable<Engineer> GetEngineers()
        {
            return db.Engineers;
        }
        
        // GET: api/Engineers/5
        [ResponseType(typeof(Engineer))]
        public IHttpActionResult GetEngineer([FromUri]int id)
        {
            Engineer engineer = db.Engineers.Find(id);
            if (engineer == null)
            {
                return NotFound();
            }

            return Ok(engineer);
        }

        [HttpPut]
        // PUT: api/Engineers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEngineer([FromUri]int id,[FromBody] Engineer engineer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != engineer.Id)
            {
                return BadRequest();
            }

            db.Entry(engineer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EngineerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        // POST: api/Engineers
        [ResponseType(typeof(Engineer))]
        public IHttpActionResult PostEngineer([FromBody]Engineer engineer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Engineers.Add(engineer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = engineer.Id }, engineer);
        }

        [HttpDelete]
        // DELETE: api/Engineers/5
        [ResponseType(typeof(Engineer))]
        public IHttpActionResult DeleteEngineer([FromUri]int id)
        {
            Engineer engineer = db.Engineers.Find(id);
            if (engineer == null)
            {
                return NotFound();
            }

            db.Engineers.Remove(engineer);
            db.SaveChanges();

            return Ok(engineer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EngineerExists(int id)
        {
            return db.Engineers.Count(e => e.Id == id) > 0;
        }
    }
}