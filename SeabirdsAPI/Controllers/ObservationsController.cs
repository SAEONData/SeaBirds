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
    public class ObservationsController : ApiController
    {
        private SEABIRDSEntities2 db = new SEABIRDSEntities2();

        // GET: api/Observations
        public IQueryable<Observation> GetObservations()
        {
            return db.Observations;
        }

        // GET: api/Observations/5
        [ResponseType(typeof(Observation))]
        public IHttpActionResult GetObservation(int id)
        {
            Observation observation = db.Observations.Find(id);
            if (observation == null)
            {
                return NotFound();
            }

            return Ok(observation);
        }

        // PUT: api/Observations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutObservation(int id, int CruiseID = 0, int TransectID = 0, int SpeciesID = 0, bool Present = false, int Flying = -1, int Sitting = -1)
        {
            Observation observation = db.Observations.Find(id);
            if (observation == null)
            {
                return NotFound();
            }
            observation.CruiseID = CruiseID != 0 ? CruiseID : observation.CruiseID;
            observation.TransectID = TransectID != 0 ? TransectID : observation.TransectID;
            observation.SpeciesID = SpeciesID != 0 ? SpeciesID : observation.SpeciesID;
            observation.Present = Present ? "yes" : null;
            observation.Flying = Flying != -1 ? Flying : observation.Flying;
            observation.Sitting = Sitting != -1 ? Sitting : observation.Sitting;


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != observation.ObservationID)
            {
                return BadRequest();
            }

            db.Entry(observation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(observation);
        }

        // POST: api/Observations
        [ResponseType(typeof(Observation))]
        public IHttpActionResult PostObservation(int CruiseID, int TransectID, int SpeciesID, bool Present, int Flying, int Sitting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Observation observation = new Observation();
            observation.CruiseID = CruiseID;
            observation.TransectID = TransectID;
            observation.SpeciesID = SpeciesID;
            observation.Present = Present ? "yes" : null;
            observation.Flying = Flying;
            observation.Sitting = Sitting;
            observation.InsertTimeStamp = DateTime.Now;

            db.Observations.Add(observation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = observation.ObservationID }, observation);
        }

        // DELETE: api/Observations/5
        [ResponseType(typeof(Observation))]
        public IHttpActionResult DeleteObservation(int id)
        {
            Observation observation = db.Observations.Find(id);
            if (observation == null)
            {
                return NotFound();
            }

            db.Observations.Remove(observation);
            db.SaveChanges();

            return Ok(observation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ObservationExists(int id)
        {
            return db.Observations.Count(e => e.ObservationID == id) > 0;
        }
    }
}