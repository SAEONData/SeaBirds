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
    public class ShipboardsController : ApiController
    {
        private SEABIRDSEntities2 db = new SEABIRDSEntities2();

        // GET: api/Shipboards
        public IQueryable<Shipboard> GetShipboards()
        {
            return db.Shipboards;
        }

        // GET: api/Shipboards/5
        [ResponseType(typeof(Shipboard))]
        public IHttpActionResult GetShipboard(int id)
        {
            Shipboard shipboard = db.Shipboards.Find(id);
            if (shipboard == null)
            {
                return NotFound();
            }

            return Ok(shipboard);
        }

        // PUT: api/Shipboards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShipboard(int id, int CruiseID = -1, int ObserverID = -1, int ObserverNo = -1, string ShipDuty = "")
        {
            Shipboard shipboard = db.Shipboards.Find(id);
            if (shipboard == null)
            {
                return NotFound();
            }
            shipboard.CruiseID = CruiseID != -1 ? CruiseID : shipboard.CruiseID;
            shipboard.ObserverID = ObserverID != -1 ? ObserverID : shipboard.ObserverID;
            shipboard.ObserverNo = ObserverNo != -1 ? ObserverNo : shipboard.ObserverNo;
            shipboard.ShipDuty = ShipDuty != "" ? ShipDuty : shipboard.ShipDuty;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shipboard.ID)
            {
                return BadRequest();
            }

            db.Entry(shipboard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipboardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(shipboard);
        }

        // POST: api/Shipboards
        [ResponseType(typeof(Shipboard))]
        public IHttpActionResult PostShipboard(int CruiseID, int ObserverID, int ObserverNo, string ShipDuty)
        {
            Shipboard shipboard = new Shipboard();
            shipboard.CruiseID = CruiseID;
            shipboard.ObserverID = ObserverID;
            shipboard.ObserverNo = ObserverNo;
            shipboard.ShipDuty = ShipDuty;
            shipboard.InsertTimeStamp = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Shipboards.Add(shipboard);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shipboard.ID }, shipboard);
        }

        // DELETE: api/Shipboards/5
        [ResponseType(typeof(Shipboard))]
        public IHttpActionResult DeleteShipboard(int id)
        {
            Shipboard shipboard = db.Shipboards.Find(id);
            if (shipboard == null)
            {
                return NotFound();
            }

            db.Shipboards.Remove(shipboard);
            db.SaveChanges();

            return Ok(shipboard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShipboardExists(int id)
        {
            return db.Shipboards.Count(e => e.ID == id) > 0;
        }
    }
}