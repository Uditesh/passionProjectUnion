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
using PassionProjUditesh.Models;

namespace PassionProjUditesh.Controllers
{
    public class SectorDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SectorData/ListSectors
        [ResponseType(typeof(SectorDto))]
        [HttpGet]
        public IHttpActionResult ListSectors()
        {
            List<Sector> Sectors = db.Sectors.ToList();
            List<SectorDto> SectorDtos = new List<SectorDto>();

            Sectors.ForEach(o => SectorDtos.Add(new SectorDto()
            {
                SectorID = o.SectorID,
                SectorName = o.SectorName
            }));

            return Ok(SectorDtos);
        }

        // GET: api/SectorData/FindSector/5
        [ResponseType(typeof(SectorDto))]
        [HttpGet]
        public IHttpActionResult FindSector(int id)
        {
            Sector sector = db.Sectors.Find(id);
            SectorDto sectorDto = new SectorDto()
            {
                SectorID = sector.SectorID,
                SectorName = sector.SectorName
            };
            if (sector == null)
            {
                return NotFound();
            }

            return Ok(sectorDto);
        }

        // POST: api/SectorData/UpdateSector/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateSector(int id, Sector sector)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sector.SectorID)
            {
                return BadRequest();
            }

            db.Entry(sector).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SectorExists(id))
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

        // POST: api/SectorData/AddSector
        [ResponseType(typeof(Sector))]
        [HttpPost]
        public IHttpActionResult AddSector(Sector sector)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sectors.Add(sector);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sector.SectorID }, sector);
        }

        // POST: api/SectorData/DeleteSector/5
        [ResponseType(typeof(Sector))]
        [HttpPost]
        public IHttpActionResult DeleteSector(int id)
        {
            Sector sector = db.Sectors.Find(id);
            if (sector == null)
            {
                return NotFound();
            }

            db.Sectors.Remove(sector);
            db.SaveChanges();

            return Ok(sector);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SectorExists(int id)
        {
            return db.Sectors.Count(e => e.SectorID == id) > 0;
        }
    }
}