using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SeabirdsAPI.Models;

namespace SeabirdsAPI.Controllers
{
    public class TransectsController : ApiController
    {
        private SEABIRDSEntities2 db = new SEABIRDSEntities2();

        // GET: api/Transects
        public IQueryable<Transect> GetTransects()
        {
            return db.Transects;
        }

        // GET: api/Transects/5
        [ResponseType(typeof(Transect))]
        public IHttpActionResult GetTransect(int id)
        {
            Transect transect = db.Transects.Find(id);
            if (transect == null)
            {
                return NotFound();
            }

            return Ok(transect);
        }

        // PUT: api/Transects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTransect(int id, string TransectRef = null, int CruiseID = 0, string TransectNo = null, string Date = null, string TimeStart = null, string TimeEnd = null, int Observer1ID = 0, int Observer2ID = 0, string CountType = null, string WindDirection = null, double WindDirection_deg = -1000, string WindStrength_kmph = null, string CloudCover = null, string Visibility_m = null, string Precipitation = null, string CountingShipFollowers = null, double SwellHeight_m = -1000, string CoordinateSystem = null, double LatitudeStart = -1000, double LongitudeStart = -1000, double LatitudeEnd = -1000, double LongitudeEnd = -1000, int CountArea = 0, int ArcFromBow = 0, string PositionOnVessel = null)
        {
            Transect transect = db.Transects.Find(id);
            if (transect == null)
            {
                return NotFound();
            }
            transect.TransectRef = TransectRef != null ? TransectRef : transect.TransectRef;
            transect.CruiseID = CruiseID != 0 ? CruiseID : transect.CruiseID;
            transect.TransectNo = TransectNo != null ? TransectNo : transect.TransectNo;
            transect.Date = Date != null ? DateTime.Parse(Date) : transect.Date;
            transect.TimeStart = TimeStart != null ? DateTime.Parse(TimeStart) : transect.TimeStart;
            transect.TimeEnd = TimeEnd != null ? DateTime.Parse(TimeEnd) : transect.TimeEnd;
            transect.Observer1ID = Observer1ID != 0 ? Observer1ID : transect.Observer1ID;
            transect.Observer2ID = Observer2ID != 0 ? Observer2ID : transect.Observer2ID;
            transect.CountType = CountType != null ? CountType : transect.CountType;
            transect.WindDirection = WindDirection != null ? WindDirection : transect.WindDirection;
            transect.WindDirection_deg = WindDirection_deg != -1000 ? WindDirection_deg : transect.WindDirection_deg;
            transect.WindStrength_kmph = WindStrength_kmph != null ? WindStrength_kmph : transect.WindStrength_kmph;
            transect.CloudCover = CloudCover != null ? CloudCover : transect.CloudCover;
            transect.Visibility_m = Visibility_m != null ? Visibility_m : transect.Visibility_m;
            transect.Precipitation = Precipitation != null ? Precipitation : transect.Precipitation;
            transect.CountingShipFollowers = CountingShipFollowers != null ? CountingShipFollowers : transect.CountingShipFollowers;
            transect.SwellHeight_m = SwellHeight_m != -1000 ? SwellHeight_m : transect.SwellHeight_m;
            transect.CoordinateSystem = CoordinateSystem != null ? CoordinateSystem : transect.CoordinateSystem;
            transect.LatitudeStart = LatitudeStart != -1000 ? LatitudeStart : transect.LatitudeStart;
            transect.LongitudeStart = LongitudeStart != -1000 ? LongitudeStart : transect.LongitudeStart;
            transect.LatitudeEnd = LatitudeEnd != -1000 ? LatitudeEnd : transect.LatitudeEnd;
            transect.LongitudeEnd = LongitudeEnd != -1000 ? LongitudeEnd : transect.LongitudeEnd;
            transect.CountArea = CountArea != -1000 ? CountArea : transect.CountArea;
            transect.ArcFromBow = ArcFromBow != -1000 ? ArcFromBow : transect.ArcFromBow;
            transect.PositionOnVessel = PositionOnVessel != null ? PositionOnVessel : transect.PositionOnVessel;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transect.TransectID)
            {
                return BadRequest();
            }

            db.Entry(transect).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(transect);
        }

        // POST: api/Transects
        [ResponseType(typeof(Transect))]
        public IHttpActionResult PostTransect(string TransectRef, int CruiseID, string TransectNo, string Date, string TimeStart, string TimeEnd, string CountType, string WindDirection, double WindDirection_deg, string WindStrength_kmph, string CloudCover, string Visibility_m, string Precipitation, double SwellHeight_m, double LatitudeStart, double LongitudeStart, double LatitudeEnd, double LongitudeEnd, int CountArea, int ArcFromBow, string PositionOnVessel, int Observer1ID, int Observer2ID = 0, string CoordinateSystem = null, string CountingShipFollowers = null)
        {
            Transect transect = new Transect();
            transect.TransectRef = TransectRef;
            transect.CruiseID = CruiseID;
            transect.TransectNo = TransectNo;
            transect.DataSourceID = 2;

            transect.Date = DateTime.Parse(Date);
            transect.TimeStart = DateTime.Parse(TimeStart);
            transect.TimeEnd = DateTime.Parse(TimeEnd);
            transect.Observer1ID = Observer1ID;
            transect.Observer2ID = Observer2ID;
            transect.CountType = CountType;
            transect.WindDirection = WindDirection;
            transect.WindDirection_deg = WindDirection_deg;
            transect.WindStrength_kmph = WindStrength_kmph;
            transect.CloudCover = CloudCover;
            transect.Visibility_m = Visibility_m;
            transect.Precipitation = Precipitation;
            transect.CountingShipFollowers = CountingShipFollowers;
            transect.SwellHeight_m = SwellHeight_m;
            transect.CoordinateSystem = CoordinateSystem;
            transect.LatitudeStart = LatitudeStart;
            transect.LongitudeStart = LongitudeStart;
            transect.LatitudeEnd = LatitudeEnd;
            transect.LongitudeEnd = LongitudeEnd;
            transect.CountArea = CountArea;
            transect.ArcFromBow = ArcFromBow;
            transect.PositionOnVessel = PositionOnVessel;
            transect.InsertTimeStamp = DateTime.Now;

            if (transect.Observer2ID == 0)
                transect.Observer2ID = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Transects.Add(transect);

            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return CreatedAtRoute("DefaultApi", new { id = transect.TransectID }, transect);
        }

        // DELETE: api/Transects/5
        [ResponseType(typeof(Transect))]
        public IHttpActionResult DeleteTransect(int id)
        {
            Transect transect = db.Transects.Find(id);
            if (transect == null)
            {
                return NotFound();
            }

            db.Transects.Remove(transect);
            db.SaveChanges();

            return Ok(transect);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransectExists(int id)
        {
            return db.Transects.Count(e => e.TransectID == id) > 0;
        }
    }
}