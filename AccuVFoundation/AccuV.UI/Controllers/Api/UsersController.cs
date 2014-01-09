using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AccuV.DataAccess.Entities;
using AccuV.UI.Dto;
using AccuV.UI.Models;
using AccuVAPI.Operations;
using Omu.ValueInjecter;

namespace AccuV.UI.Controllers.Api
{
    [RoutePrefix("api")]
    public class UsersController : ApiController
    {
        private readonly IUserOperations _operations;

        public UsersController(IUserOperations operations)
        {
            _operations = operations;
        }

        public async Task<UserResponseDto> GetUser(string id)
        {
            var user = await _operations.Users.Include("UserActivities.Activity.ActivityType")
                .SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return null;

            var response = new UserResponseDto();
            response.User = new UserDto();  
            response.User.InjectFrom(user);
            response.UserActivities = new List<UserActivityDto>();
            foreach (var userActivity in user.UserActivities)
            {
                var activity = new UserActivityDto();
                activity.InjectFrom(userActivity);
                activity.ActivityName = userActivity.Activity.ActivityDescription;
                activity.TypeName = userActivity.Activity.ActivityType.Type;
                response.UserActivities.Add(activity);
            }
            response.UserActivities = response.UserActivities.OrderBy(a => a.ActivityName).ToList();
            return response;
        }

        [HttpPost]
        public async Task<UserDto> PostUser(UserDto user)
        {
            if (string.IsNullOrEmpty(user.Id))
            {
                throw new HttpException(400, "User id should be supplied");
            }
            
            var newUser = new User();
            newUser.Active = true;
            newUser.InjectFrom(user);

            await _operations.Create(newUser);
            var result = new UserDto();
            result.InjectFrom(newUser);
            return result;
        }

        [HttpPut]
        public async Task<HttpResponseMessage> PutUser(UserDto userDto)
        {
            var user = new User();
            user.InjectFrom(userDto);
            await _operations.Update(user);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("Users/Activities")]
        public async Task<HttpResponseMessage> PostActivities(UserActivitiesUpdateRequest activitiesUpdateRequest)
        {
            var userActivities = new List<UserActivity>();
            foreach (var userActivityDto in activitiesUpdateRequest.UserActivities)
            {
                var userActivity = new UserActivity();
                userActivity.InjectFrom(userActivityDto);
                userActivities.Add(userActivity);
            }

            await _operations.UpdateUserActivities(activitiesUpdateRequest.UserId, userActivities);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
