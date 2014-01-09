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
    public class ProjectsController : ODataController
    {
        private NexiusFusionContext db = new NexiusFusionContext();

        // GET odata/Projects
        [Queryable]
        public IQueryable<Project> GetProjects()
        {
            return db.Projects;
        }

        // GET odata/Projects(5)
        [Queryable]
        public SingleResult<Project> GetProject([FromODataUri] string key)
        {
            return SingleResult.Create(db.Projects.Where(project => project.ProjectId == key));
        }

        // PUT odata/Projects(5)
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != project.ProjectId)
            {
                return BadRequest();
            }

            db.Entry(project).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(project);
        }

        // POST odata/Projects
        public async Task<IHttpActionResult> Post(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Projects.Add(project);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProjectExists(project.ProjectId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(project);
        }

        // PATCH odata/Projects(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<Project> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Project project = await db.Projects.FindAsync(key);
            if (project == null)
            {
                return NotFound();
            }

            patch.Patch(project);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(project);
        }

        // DELETE odata/Projects(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            Project project = await db.Projects.FindAsync(key);
            if (project == null)
            {
                return NotFound();
            }

            db.Projects.Remove(project);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Projects(5)/Sites
        [Queryable]
        public IQueryable<Site> GetSites([FromODataUri] string key)
        {
            return db.Projects.Where(m => m.ProjectId == key).SelectMany(m => m.Sites);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExists(string key)
        {
            return db.Projects.Count(e => e.ProjectId == key) > 0;
        }
    }
}
