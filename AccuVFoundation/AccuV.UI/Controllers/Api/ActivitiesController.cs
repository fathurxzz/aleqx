using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using AccuV.DataAccess.Entities;
using AccuV.UI.Dto;
using AccuVAPI.Operations;
using Omu.ValueInjecter;

namespace AccuV.UI.Controllers.Api
{
    [RoutePrefix("api")]
    public class ActivitiesController : ODataController
    {
        private readonly IActivityOperations _activityOperations;

        public ActivitiesController(IActivityOperations activityOperations)
        {
            _activityOperations = activityOperations;
        }

        // GET odata/Employees
        [Queryable]
        public async Task<IQueryable<ActivityDto>> GetActivities()
        {
            var entitySet = _activityOperations.Activities
                .Include(a => a.ActivityType);

            var orderedSet = entitySet.OrderBy(a => a.ActivityDescription);

            var result =
                (await orderedSet.ToListAsync()).Select(a =>
                {
                    var dto = new ActivityDto();
                    dto.InjectFrom(a);
                    dto.TypeName = a.ActivityType.Type;
                    dto.ActivityName = a.ActivityDescription;
                    return dto;
                });

            return result.AsQueryable();
        }

        public async Task<Activity> GetActivity(int id)
        {
            return await _activityOperations.Activities.SingleAsync(a => a.ActivityId == id);
        }

        [HttpPost]
        public async Task<ActivityDto> PostActivity(ActivityDto activityDto)
        {
            var activity = new Activity();
            activity.InjectFrom(activityDto);
            activity.Active = true;
            await _activityOperations.Create(activity);

            var result = new ActivityDto();
            result.InjectFrom(activity);
            return result;
        }

        [HttpPut]
        public async Task<ActivityDto> PutActivity(ActivityDto activityDto)
        {
            var activity = new Activity();
            activity.InjectFrom(activityDto);
            activity.Active = true;
            await _activityOperations.Create(activity);

            var result = new ActivityDto();
            result.InjectFrom(activity);
            return result;
        }

        [Route("Activities/Modules")]
        public async Task<IEnumerable<ModuleDto>> GetModules()
        {
            var modules = await _activityOperations.Modules.ToListAsync();
            return modules.Select(m => new ModuleDto { ModuleId = m.ModuleId, Description = m.Description });
        }

        [Route("Activities/ActivityTypes")]
        public async Task<IEnumerable<ActivityTypeDto>> GetActivityTypes()
        {
            var activityTypes = await _activityOperations.ActivityTypes.ToListAsync();
            return activityTypes.Select(m => new ActivityTypeDto { ActivityTypeId = m.ActivityTypeId, Type = m.Type });
        }
    }
}
