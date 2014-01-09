using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using NexiusFusion.DataAccess.Entities;

namespace AccuV.UI.Controllers.Odata
{
    public class HazardsController : ODataController
    {
        private NexiusFusionContext db = new NexiusFusionContext();

        // GET odata/Hazards
        [Queryable]
        public IQueryable<Hazard> GetHazards()
        {
            return db.Hazards;
        }

        // GET odata/Hazards(5)
        [Queryable]
        public SingleResult<Hazard> GetHazard([FromODataUri] int key)
        {
            return SingleResult.Create(db.Hazards.Where(hazard => hazard.HazardId == key));
        }

        // PUT odata/Hazards(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Hazard hazard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != hazard.HazardId)
            {
                return BadRequest();
            }

            db.Entry(hazard).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HazardExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(hazard);
        }

        // POST odata/Hazards
        public async Task<IHttpActionResult> Post(Hazard hazard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hazards.Add(hazard);
            await db.SaveChangesAsync();

            return Created(hazard);
        }

        // PATCH odata/Hazards(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Hazard> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Hazard hazard = await db.Hazards.FindAsync(key);
            if (hazard == null)
            {
                return NotFound();
            }

            patch.Patch(hazard);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HazardExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(hazard);
        }

        // DELETE odata/Hazards(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Hazard hazard = await db.Hazards.FindAsync(key);
            if (hazard == null)
            {
                return NotFound();
            }

            db.Hazards.Remove(hazard);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Hazards(5)/ReportHazards
        [Queryable]
        public IQueryable<ReportHazard> GetReportHazards([FromODataUri] int key)
        {
            return db.Hazards.Where(m => m.HazardId == key).SelectMany(m => m.ReportHazards);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HazardExists(int key)
        {
            return db.Hazards.Count(e => e.HazardId == key) > 0;
        }
    }
}
