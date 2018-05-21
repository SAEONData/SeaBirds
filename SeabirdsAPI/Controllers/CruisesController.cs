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
    public class CruisesController : ApiController
    {
        private SEABIRDSEntities2 db = new SEABIRDSEntities2();

        // GET: api/Cruises
        public IQueryable<Cruise> GetCruises()
        {
            return db.Cruises;
        }

        // GET: api/Cruises/5
        [ResponseType(typeof(Cruise))]
        public IHttpActionResult GetCruise(int id)
        {
            Cruise cruise = db.Cruises.Find(id);
            if (cruise == null)
            {
                return NotFound();
            }

            return Ok(cruise);
        }

        // PUT: api/Cruises/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCruise(int id, string CruiseRef = null, int VesselID = 0, string PortLeave = null, string PortReturn = null, string DateLeave = null, string DateReturn = null)
        {
            Cruise cruise = db.Cruises.Find(id);
            if (cruise == null)
            {
                return NotFound();
            }
            cruise.CruiseRef = CruiseRef != null ? CruiseRef : cruise.CruiseRef;
            cruise.VesselID = VesselID != 0 ? VesselID : cruise.VesselID;
            cruise.PortLeave = PortLeave != null ? PortLeave : cruise.PortLeave;
            cruise.PortReturn = PortReturn != null ? PortReturn : cruise.PortReturn;
            cruise.DateLeave = DateLeave != null ? DateTime.Parse(DateLeave) : cruise.DateLeave;
            cruise.DateReturn = DateReturn != null ? DateTime.Parse(DateReturn) : cruise.DateReturn;


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cruise.CruiseID)
            {
                return BadRequest();
            }

            db.Entry(cruise).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CruiseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(cruise);
        }

        // POST: api/Cruises
        [ResponseType(typeof(Cruise))]
        public IHttpActionResult PostCruise(string CruiseRef, int VesselID, string PortLeave, string PortReturn, string DateLeave, string DateReturn)
        {
            Cruise cruise = new Cruise();
            cruise.CruiseRef = CruiseRef;
            cruise.VesselID = VesselID;
            cruise.PortLeave = PortLeave;
            cruise.PortReturn = PortReturn;
            cruise.DateLeave = DateTime.Parse(DateLeave);
            cruise.DateReturn = DateTime.Parse(DateReturn);
            cruise.InsertTimeStamp = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cruises.Add(cruise);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cruise.CruiseID }, cruise);
        }

        // DELETE: api/Cruises/5
        [ResponseType(typeof(Cruise))]
        public IHttpActionResult DeleteCruise(int id)
        {
            Cruise cruise = db.Cruises.Find(id);
            if (cruise == null)
            {
                return NotFound();
            }

            db.Cruises.Remove(cruise);
            db.SaveChanges();

            return Ok(cruise);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CruiseExists(int id)
        {
            return db.Cruises.Count(e => e.CruiseID == id) > 0;
        }
    }
}