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
    public class WeatherController : ODataController
    {
        private NexiusFusionContext db = new NexiusFusionContext();

        // GET odata/Weather
        [Queryable]
        public IQueryable<Weather> GetWeather()
        {
            return db.Weathers;
        }

        // GET odata/Weather(5)
        [Queryable]
        public SingleResult<Weather> GetWeather([FromODataUri] int key)
        {
            return SingleResult.Create(db.Weathers.Where(weather => weather.WeatherId == key));
        }

        // PUT odata/Weather(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Weather weather)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != weather.WeatherId)
            {
                return BadRequest();
            }

            db.Entry(weather).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(weather);
        }

        // POST odata/Weather
        public async Task<IHttpActionResult> Post(Weather weather)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Weathers.Add(weather);
            await db.SaveChangesAsync();

            return Created(weather);
        }

        // PATCH odata/Weather(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Weather> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Weather weather = await db.Weathers.FindAsync(key);
            if (weather == null)
            {
                return NotFound();
            }

            patch.Patch(weather);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(weather);
        }

        // DELETE odata/Weather(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Weather weather = await db.Weathers.FindAsync(key);
            if (weather == null)
            {
                return NotFound();
            }

            db.Weathers.Remove(weather);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Weather(5)/Reports
        [Queryable]
        public IQueryable<Report> GetReports([FromODataUri] int key)
        {
            return db.Weathers.Where(m => m.WeatherId == key).SelectMany(m => m.Reports);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WeatherExists(int key)
        {
            return db.Weathers.Count(e => e.WeatherId == key) > 0;
        }
    }
}
