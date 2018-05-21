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
using SeabirdsAPI.Models;

namespace SeabirdsAPI.Controllers
{
    public class ObserversController : ApiController
    {
        private SEABIRDSEntities2 db = new SEABIRDSEntities2();

        // GET: api/Observers
        public IQueryable<Observer> GetObservers()
        {
            return db.Observers;
        }

        // GET: api/Observers/5
        [ResponseType(typeof(Observer))]
        public IHttpActionResult GetObserver(int id)
        {
            Observer observer = db.Observers.Find(id);
            if (observer == null)
            {
                return NotFound();
            }

            return Ok(observer);
        }

        // PUT: api/Observers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutObserver(int id, string Fullname = null, string Experience = null)
        {
            Observer observer = db.Observers.Find(id);
            if (observer == null)
            {
                return NotFound();
            }
            observer.Fullname = Fullname != null ? Fullname : observer.Fullname;
            observer.Experience = Experience != null ? Experience : observer.Experience;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != observer.ObserverID)
            {
                return BadRequest();
            }

            db.Entry(observer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObserverExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(observer);
        }

        // POST: api/Observers
        [ResponseType(typeof(Observer))]
        public IHttpActionResult PostObserver(string Fullname, string Experience)
        {
            Observer observer = new Observer();
            observer.Fullname = Fullname;
            observer.Experience = Experience;
            observer.InsertTimeStamp = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Observers.Add(observer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = observer.ObserverID }, observer);
        }

        // DELETE: api/Observers/5
        [ResponseType(typeof(Observer))]
        public IHttpActionResult DeleteObserver(int id)
        {
            Observer observer = db.Observers.Find(id);
            if (observer == null)
            {
                return NotFound();
            }

            db.Observers.Remove(observer);
            db.SaveChanges();

            return Ok(observer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ObserverExists(int id)
        {
            return db.Observers.Count(e => e.ObserverID == id) > 0;
        }
    }
}