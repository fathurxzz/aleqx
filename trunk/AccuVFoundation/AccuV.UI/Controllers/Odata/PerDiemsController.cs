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
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using NexiusFusion.DataAccess.Entities;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<PerDiem>("PerDiems");
    builder.EntitySet<ReportEmployee>("ReportEmployee"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PerDiemsController : ODataController
    {
        private NexiusFusionContext db = new NexiusFusionContext();

        // GET odata/PerDiems
        [Queryable]
        public IQueryable<PerDiem> GetPerDiems()
        {
            return db.PerDiems;
        }

        // GET odata/PerDiems(5)
        [Queryable]
        public SingleResult<PerDiem> GetPerDiem([FromODataUri] int key)
        {
            return SingleResult.Create(db.PerDiems.Where(perdiem => perdiem.PerDiemId == key));
        }

        // PUT odata/PerDiems(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, PerDiem perdiem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != perdiem.PerDiemId)
            {
                return BadRequest();
            }

            db.Entry(perdiem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerDiemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(perdiem);
        }

        // POST odata/PerDiems
        public async Task<IHttpActionResult> Post(PerDiem perdiem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PerDiems.Add(perdiem);
            await db.SaveChangesAsync();

            return Created(perdiem);
        }

        // PATCH odata/PerDiems(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<PerDiem> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PerDiem perdiem = await db.PerDiems.FindAsync(key);
            if (perdiem == null)
            {
                return NotFound();
            }

            patch.Patch(perdiem);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerDiemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(perdiem);
        }

        // DELETE odata/PerDiems(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            PerDiem perdiem = await db.PerDiems.FindAsync(key);
            if (perdiem == null)
            {
                return NotFound();
            }

            db.PerDiems.Remove(perdiem);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/PerDiems(5)/ReportEmployees
        [Queryable]
        public IQueryable<ReportEmployee> GetReportEmployees([FromODataUri] int key)
        {
            return db.PerDiems.Where(m => m.PerDiemId == key).SelectMany(m => m.ReportEmployees);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PerDiemExists(int key)
        {
            return db.PerDiems.Count(e => e.PerDiemId == key) > 0;
        }
    }
}
