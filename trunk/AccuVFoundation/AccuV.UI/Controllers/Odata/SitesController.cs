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
    public class SitesController : ODataController
    {
        private NexiusFusionContext db = new NexiusFusionContext();

        // GET odata/Sites
        [Queryable]
        public IQueryable<Site> GetSites()
        {
            return db.Sites;
        }

        // GET odata/Sites(5)
        [Queryable]
        public SingleResult<Site> GetSite([FromODataUri] string key)
        {
            return SingleResult.Create(db.Sites.Where(site => site.SiteNumber == key));
        }

        // PUT odata/Sites(5)
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Site site)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != site.SiteNumber)
            {
                return BadRequest();
            }

            db.Entry(site).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(site);
        }

        // POST odata/Sites
        public async Task<IHttpActionResult> Post(Site site)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sites.Add(site);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SiteExists(site.SiteNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(site);
        }

        // PATCH odata/Sites(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<Site> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Site site = await db.Sites.FindAsync(key);
            if (site == null)
            {
                return NotFound();
            }

            patch.Patch(site);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(site);
        }

        // DELETE odata/Sites(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            Site site = await db.Sites.FindAsync(key);
            if (site == null)
            {
                return NotFound();
            }

            db.Sites.Remove(site);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Sites(5)/Project
        [Queryable]
        public SingleResult<Project> GetProject([FromODataUri] string key)
        {
            return SingleResult.Create(db.Sites.Where(m => m.SiteNumber == key).Select(m => m.Project));
        }

        // GET odata/Sites(5)/Reports
        [Queryable]
        public IQueryable<Report> GetReports([FromODataUri] string key)
        {
            return db.Sites.Where(m => m.SiteNumber == key).SelectMany(m => m.Reports);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SiteExists(string key)
        {
            return db.Sites.Count(e => e.SiteNumber == key) > 0;
        }
    }
}
