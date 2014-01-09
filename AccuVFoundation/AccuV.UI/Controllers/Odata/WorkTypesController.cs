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
    public class WorkTypesController : ODataController
    {
        private NexiusFusionContext db = new NexiusFusionContext();

        // GET odata/WorkTypes
        [Queryable]
        public IQueryable<WorkType> GetWorkTypes()
        {
            return db.WorkTypes;
        }

        // GET odata/WorkTypes(5)
        [Queryable]
        public SingleResult<WorkType> GetWorkType([FromODataUri] int key)
        {
            return SingleResult.Create(db.WorkTypes.Where(worktype => worktype.WorkTypeId == key));
        }

        // PUT odata/WorkTypes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, WorkType worktype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != worktype.WorkTypeId)
            {
                return BadRequest();
            }

            db.Entry(worktype).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(worktype);
        }

        // POST odata/WorkTypes
        public async Task<IHttpActionResult> Post(WorkType worktype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkTypes.Add(worktype);
            await db.SaveChangesAsync();

            return Created(worktype);
        }

        // PATCH odata/WorkTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<WorkType> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            WorkType worktype = await db.WorkTypes.FindAsync(key);
            if (worktype == null)
            {
                return NotFound();
            }

            patch.Patch(worktype);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(worktype);
        }

        // DELETE odata/WorkTypes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            WorkType worktype = await db.WorkTypes.FindAsync(key);
            if (worktype == null)
            {
                return NotFound();
            }

            db.WorkTypes.Remove(worktype);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/WorkTypes(5)/Reports
        [Queryable]
        public IQueryable<Report> GetReports([FromODataUri] int key)
        {
            return db.WorkTypes.Where(m => m.WorkTypeId == key).SelectMany(m => m.Reports);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkTypeExists(int key)
        {
            return db.WorkTypes.Count(e => e.WorkTypeId == key) > 0;
        }
    }
}
