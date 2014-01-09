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
    public class EmployeeTypesController : ODataController
    {
        private NexiusFusionContext db = new NexiusFusionContext();

        // GET odata/EmployeeTypes
        [Queryable]
        public IQueryable<EmployeeType> GetEmployeeTypes()
        {
            return db.EmployeeTypes;
        }

        // GET odata/EmployeeTypes(5)
        [Queryable]
        public SingleResult<EmployeeType> GetEmployeeType([FromODataUri] int key)
        {
            return SingleResult.Create(db.EmployeeTypes.Where(employeetype => employeetype.EmployeeTypeId == key));
        }

        // PUT odata/EmployeeTypes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, EmployeeType employeetype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != employeetype.EmployeeTypeId)
            {
                return BadRequest();
            }

            db.Entry(employeetype).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employeetype);
        }

        // POST odata/EmployeeTypes
        public async Task<IHttpActionResult> Post(EmployeeType employeetype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeTypes.Add(employeetype);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeTypeExists(employeetype.EmployeeTypeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(employeetype);
        }

        // PATCH odata/EmployeeTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<EmployeeType> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmployeeType employeetype = await db.EmployeeTypes.FindAsync(key);
            if (employeetype == null)
            {
                return NotFound();
            }

            patch.Patch(employeetype);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(employeetype);
        }

        // DELETE odata/EmployeeTypes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            EmployeeType employeetype = await db.EmployeeTypes.FindAsync(key);
            if (employeetype == null)
            {
                return NotFound();
            }

            db.EmployeeTypes.Remove(employeetype);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/EmployeeTypes(5)/ReportEmployees
        [Queryable]
        public IQueryable<ReportEmployee> GetReportEmployees([FromODataUri] int key)
        {
            return db.EmployeeTypes.Where(m => m.EmployeeTypeId == key).SelectMany(m => m.ReportEmployees);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeTypeExists(int key)
        {
            return db.EmployeeTypes.Count(e => e.EmployeeTypeId == key) > 0;
        }
    }
}
