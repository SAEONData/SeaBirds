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
    public class VesselsController : ApiController
    {
        private SEABIRDSEntities2 db = new SEABIRDSEntities2();

        // GET: api/Vessels
        public IQueryable<Vessel> GetVessels()
        {
            return db.Vessels;
        }

        // GET: api/Vessels/5
        [ResponseType(typeof(Vessel))]
        public IHttpActionResult GetVessel(int id)
        {
            Vessel vessel = db.Vessels.Find(id);
            if (vessel == null)
            {
                return NotFound();
            }

            return Ok(vessel);
        }

        // PUT: api/Vessels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVessel(int id, String Name)
        {
            Vessel vessel = db.Vessels.Find(id);
            if (vessel == null)
            {
                return NotFound();
            }
            vessel.Name = Name;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vessel.VesselID)
            {
                return BadRequest();
            }

            db.Entry(vessel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VesselExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(vessel);
        }

        // POST: api/Vessels
        [ResponseType(typeof(Vessel))]
        public IHttpActionResult PostVessel(String Name)
        {
            Vessel vessel = new Vessel();
            vessel.Name = Name;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vessels.Add(vessel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vessel.VesselID }, vessel);
        }

        // DELETE: api/Vessels/5
        [ResponseType(typeof(Vessel))]
        public IHttpActionResult DeleteVessel(int id)
        {
            Vessel vessel = db.Vessels.Find(id);
            if (vessel == null)
            {
                return NotFound();
            }

            db.Vessels.Remove(vessel);
            db.SaveChanges();

            return Ok(vessel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VesselExists(int id)
        {
            return db.Vessels.Count(e => e.VesselID == id) > 0;
        }
    }
}