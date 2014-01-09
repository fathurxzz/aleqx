using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccuV.DataAccess;
using AccuV.DataAccess.Entities;
using Task = System.Threading.Tasks.Task;

namespace AccuVAPI.Operations
{
    public interface IUserOperations : IOperationStore
    {
        IQueryable<User> Users { get; }
        Task Create(User user);
        Task Disable(string userName);
        Task Update(User user);

        Task UpdateUserActivities(string userId, List<UserActivity> userActivities);
    }
    public class UserOperations :IUserOperations
    {
        private readonly IDataSession _session;

        public UserOperations(IDataSession session)
        {
            _session = session;
        }

        public IQueryable<User> Users
        {
            get
            {
                return _session.Users.AsNoTracking();
            }
        }

        public Task Create(User user)
        {
            _session.Users.Add(user);
            return _session.SaveChangesAsync();
        }

        public async Task Disable(string userName)
        {
            _session.Users.Single(u => u.Id == userName).Active = false;
            await _session.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            _session.Users.Attach(user);
            _session.Entry(user).Property(u => u.Active).IsModified = true;
            _session.Entry(user).Property(u => u.Company).IsModified = true;
            _session.Entry(user).Property(u => u.Email).IsModified = true;
            _session.Entry(user).Property(u => u.UserName).IsModified = true;
            await _session.SaveChangesAsync();
        }

        public async Task UpdateUserActivities(string userId, List<UserActivity> userActivities)
        {
            var user = await _session.Users.SingleAsync(u => u.Id == userId);
            user.UserActivities.Clear();
            foreach (var userActivity in userActivities)
            {
                user.UserActivities.Add(userActivity);
            }

            await _session.SaveChangesAsync();
        }
    }
}

