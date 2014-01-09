using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AccuV.DataAccess.Contracts;
using AccuV.DataAccess.Entities;
using AccuVAPI.Operations;
using Newtonsoft.Json;

namespace AccuV.UI.Controllers.Api
{
    [RoutePrefix("api")]
    public class MarketManagersController: ApiController
    {
        private readonly IMarketManagersOperations _marketManagersOperations;

        public MarketManagersController(IMarketManagersOperations marketManagersOperations)
        {
            _marketManagersOperations = marketManagersOperations;
        }

        public IEnumerable<Manager> Get()
        {
            var identity = User.Identity as ClaimsIdentity;
            var activities = identity.Claims
                .Where(c => c.Type == "Activity")
                .Select(c => JsonConvert.DeserializeObject<Activity>(c.Value));
            var entitySet =_marketManagersOperations.GetMarketManagers(activities, "NDPATT");

            var result = entitySet;
            return result;
        }
    }
}