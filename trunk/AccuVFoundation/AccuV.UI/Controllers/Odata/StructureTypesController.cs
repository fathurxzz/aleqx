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
    public class StructureTypesController : ODataController
    {
        private NexiusFusionContext db = new NexiusFusionContext();

        // GET odata/StructureTypes
        [Queryable]
        public IQueryable<StructureType> GetStructureTypes()
        {
            return db.StructureTypes;
        }

        // GET odata/StructureTypes(5)
        [Queryable]
        public SingleResult<StructureType> GetStructureType([FromODataUri] int key)
        {
            return SingleResult.Create(db.StructureTypes.Where(structuretype => structuretype.StructureTypeId == key));
        }

        // PUT odata/StructureTypes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, StructureType structuretype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != structuretype.StructureTypeId)
            {
                return BadRequest();
            }

            db.Entry(structuretype).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StructureTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(structuretype);
        }

        // POST odata/StructureTypes
        public async Task<IHttpActionResult> Post(StructureType structuretype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StructureTypes.Add(structuretype);
            await db.SaveChangesAsync();

            return Created(structuretype);
        }

        // PATCH odata/StructureTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<StructureType> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StructureType structuretype = await db.StructureTypes.FindAsync(key);
            if (structuretype == null)
            {
                return NotFound();
            }

            patch.Patch(structuretype);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StructureTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(structuretype);
        }

        // DELETE odata/StructureTypes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            StructureType structuretype = await db.StructureTypes.FindAsync(key);
            if (structuretype == null)
            {
                return NotFound();
            }

            db.StructureTypes.Remove(structuretype);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/StructureTypes(5)/Reports
        [Queryable]
        public IQueryable<Report> GetReports([FromODataUri] int key)
        {
            return db.StructureTypes.Where(m => m.StructureTypeId == key).SelectMany(m => m.Reports);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StructureTypeExists(int key)
        {
            return db.StructureTypes.Count(e => e.StructureTypeId == key) > 0;
        }
    }
}
