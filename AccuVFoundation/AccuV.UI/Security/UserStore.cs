using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using AccuV.DataAccess;
using AccuV.DataAccess.Contracts;
using AccuV.DataAccess.Entities;
using AccuVAPI.Operations;
using Microsoft.AspNet.Identity;
using WebGrease.Css.Extensions;
using Task = System.Threading.Tasks.Task;
using Newtonsoft.Json;

namespace AccuV.UI.Security
{
    public class UserStore : IUserClaimStore<User>
    {
        private readonly IUserOperations _userOperations;
        private readonly IDataSession _session;

        private User _currentUser;

        public UserStore(IUserOperations userOperations, IDataSession session)
        {
            _userOperations = userOperations;
            _session = session;
        }

        public Task CreateAsync(User user)
        {

            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            _currentUser = await _userOperations.Users
                .Include("UserActivities.Activity").SingleOrDefaultAsync(u => u.Id == userId && u.Active);
            return _currentUser;
        }

        public Task<User> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Claim>> GetClaimsAsync(User user)
        {
            // Create Claims of type Activity 
            var acivityClaims = await Task.Run(() => user.UserActivities.Select(CreateActivityClaim));

            //Get The Project And Roles for this user And Create Claims of type Project
            var userProjectModules = await _session.GetUserProjectModules(user.Id);
            var projectClaims = await Task.Run(() => userProjectModules.Select(CreateProjectClaim));
            
            return acivityClaims.Union(projectClaims).ToList();
        }

        public Task AddClaimAsync(User user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(User user, Claim claim)
        {
            throw new NotImplementedException();
        }

        private Claim CreateActivityClaim(UserActivity userActivity)
        {
            var permissions = AccessPermissions.None;
            if(userActivity.AccessRead) 
                permissions |= AccessPermissions.Read;
            if(userActivity.AccessCreate) 
                permissions |= AccessPermissions.Create;
            if(userActivity.AccessUpdate) 
                permissions |= AccessPermissions.Update;
            if(userActivity.AccessApprove)
                permissions |= AccessPermissions.Approve;
            if(userActivity.AccessDelete)
                permissions |= AccessPermissions.Delete;
            if(userActivity.AccessAdmin)
                permissions |= AccessPermissions.Admin;            
            
            var activity = new ActivityClaim
            {
                ActivityId = userActivity.ActivityId,
                Permissions = permissions,
                Description = userActivity.Activity.ActivityDescription,
                ActivityType = userActivity.ActivityTypeId
            };
            var claim = new Claim("Activity", JsonConvert.SerializeObject(activity, Formatting.Indented));
       
            return claim;
        }

        private Claim CreateProjectClaim(UserProjectModuleClaim projectModuleClaim)
        {
            return new Claim("Project", JsonConvert.SerializeObject(projectModuleClaim, Formatting.Indented));
        }

        public void Dispose()
        {

        }
    }
}