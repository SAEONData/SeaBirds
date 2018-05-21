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
    public class SpeciesController : ApiController
    {
        private SEABIRDSEntities2 db = new SEABIRDSEntities2();

        // GET: api/Species
        public IQueryable<Species> GetSpecies()
        {
            return db.Species;
        }

        // GET: api/Species/5
        [ResponseType(typeof(Species))]
        public IHttpActionResult GetSpecies(int id)
        {
            Species species = db.Species.Find(id);
            if (species == null)
            {
                return NotFound();
            }

            return Ok(species);
        }

        // PUT: api/Species/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSpecies(int id, String Taxonomic_Genus = null, String Taxonomic_Species = null, String English_Genus = null, String English_Species = null, String Taxon_order = null, String Taxon_family = null, String Taxon_group = null, String FamilyName = null, String Continent = null, String Region = null, String Notes = null, String Project = null, String LegendColor = null, String BirdLifeID = null, String IUCNRedListCat = null, int SAFRING_No = 0)
        {
            Species species = db.Species.Find(id);
            if (species == null)
            {
                return NotFound();
            }
            species.SAFRING_No = SAFRING_No != 0 ? SAFRING_No : species.SAFRING_No;
            species.Taxonomic_Genus = Taxonomic_Genus != null ? Taxonomic_Genus : species.Taxonomic_Genus;
            species.Taxonomic_Species = Taxonomic_Species != null ? Taxonomic_Species : species.Taxonomic_Species;
            species.English_Genus = English_Genus != null ? English_Genus : species.English_Genus;
            species.English_Species = English_Species != null ? English_Species : species.English_Species;
            species.Taxon_order = Taxon_order != null ? Taxon_order : species.Taxon_order;
            species.Taxon_family = Taxon_family != null ? Taxon_family : species.Taxon_family;
            species.Taxon_group = Taxon_group != null ? Taxon_group : species.Taxon_group;
            species.Continent = Continent != null ? Continent : species.Continent;
            species.Region = Region != null ? Region : species.Region;
            species.Notes = Notes != null ? Notes : species.Notes;
            species.Project = Project != null ? Project : species.Project;
            species.LegendColor = LegendColor != null ? LegendColor : species.LegendColor;
            species.FamilyName = FamilyName != null ? FamilyName : species.FamilyName;
            species.BirdLifeID = BirdLifeID != null ? BirdLifeID : species.BirdLifeID;
            species.IUCNRedListCat = IUCNRedListCat != null ? IUCNRedListCat : species.IUCNRedListCat;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != species.SpeciesID)
            {
                return BadRequest();
            }

            db.Entry(species).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpeciesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(species);
        }

        // POST: api/Species
        [ResponseType(typeof(Species))]
        public IHttpActionResult PostSpecies(String Taxonomic_Genus, String Taxonomic_Species, String English_Genus, String English_Species, String Taxon_order, String Taxon_family, String Taxon_group, String FamilyName, String Continent, String Region, String Notes, String Project, String LegendColor, String BirdLifeID = null, String IUCNRedListCat = null, int SAFRING_No = 0)
        {
            Species species = new Species();
            species.SAFRING_No = SAFRING_No;
            species.Taxonomic_Genus = Taxonomic_Genus;
            species.Taxonomic_Species = Taxonomic_Species;
            species.English_Genus = English_Genus;
            species.English_Species = English_Species;
            species.Taxon_order = Taxon_order;
            species.Taxon_family = Taxon_family;
            species.Taxon_group = Taxon_group;
            species.Continent = Continent;
            species.Region = Region;
            species.Notes = Notes;
            species.Project = Project;
            species.LegendColor = LegendColor;
            species.FamilyName = FamilyName;
            species.BirdLifeID = BirdLifeID;
            species.IUCNRedListCat = IUCNRedListCat;
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Species.Add(species);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = species.SpeciesID }, species);
        }

        // DELETE: api/Species/5
        [ResponseType(typeof(Species))]
        public IHttpActionResult DeleteSpecies(int id)
        {
            Species species = db.Species.Find(id);
            if (species == null)
            {
                return NotFound();
            }

            db.Species.Remove(species);
            db.SaveChanges();

            return Ok(species);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SpeciesExists(int id)
        {
            return db.Species.Count(e => e.SpeciesID == id) > 0;
        }
    }
}