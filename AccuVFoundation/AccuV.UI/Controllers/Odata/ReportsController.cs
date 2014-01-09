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
    public class ReportsController : ODataController
    {
        private readonly NexiusFusionContext db = new NexiusFusionContext();

        // GET odata/Reports
        [Queryable]
        public IQueryable<Report> GetReports()
        {
            return db.Reports;
        }

        // GET odata/Reports(5)
        [Queryable]
        public SingleResult<Report> GetReport([FromODataUri] int key)
        {
            return SingleResult.Create(db.Reports.Where(report => report.ReportId == key));
        }

        // PUT odata/Reports(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Report report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != report.ReportId)
            {
                return BadRequest();
            }

            db.Entry(report).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(report);
        }

        // POST odata/Reports
        public async Task<IHttpActionResult> Post(Report report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reports.Add(report);
            await db.SaveChangesAsync();

            return Created(report);
        }

        // PATCH odata/Reports(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Report> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Report report = await db.Reports.FindAsync(key);
            if (report == null)
            {
                return NotFound();
            }

            patch.Patch(report);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(report);
        }

        // DELETE odata/Reports(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Report report = await db.Reports.FindAsync(key);
            if (report == null)
            {
                return NotFound();
            }

            db.Reports.Remove(report);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Reports(5)/ReportEmployees
        [Queryable]
        public IQueryable<ReportEmployee> GetReportEmployees([FromODataUri] int key)
        {
            return db.Reports.Where(m => m.ReportId == key).SelectMany(m => m.ReportEmployees);
        }

        // GET odata/Reports(5)/ReportHazards
        [Queryable]
        public IQueryable<ReportHazard> GetReportHazards([FromODataUri] int key)
        {
            return db.Reports.Where(m => m.ReportId == key).SelectMany(m => m.ReportHazards);
        }

        // GET odata/Reports(5)/Site
        [Queryable]
        public SingleResult<Site> GetSite([FromODataUri] int key)
        {
            return SingleResult.Create(db.Reports.Where(m => m.ReportId == key).Select(m => m.Site));
        }

        // GET odata/Reports(5)/StructureType
        [Queryable]
        public SingleResult<StructureType> GetStructureType([FromODataUri] int key)
        {
            return SingleResult.Create(db.Reports.Where(m => m.ReportId == key).Select(m => m.StructureType));
        }

        // GET odata/Reports(5)/Weather
        [Queryable]
        public SingleResult<Weather> GetWeather([FromODataUri] int key)
        {
            return SingleResult.Create(db.Reports.Where(m => m.ReportId == key).Select(m => m.Weather));
        }

        // GET odata/Reports(5)/WorkType
        [Queryable]
        public SingleResult<WorkType> GetWorkType([FromODataUri] int key)
        {
            return SingleResult.Create(db.Reports.Where(m => m.ReportId == key).Select(m => m.WorkType));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReportExists(int key)
        {
            return db.Reports.Count(e => e.ReportId == key) > 0;
        }
    }
}
