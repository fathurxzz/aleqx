using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccuV.DataAccess;
using AccuV.DataAccess.Entities;
using Task = System.Threading.Tasks.Task;

namespace AccuVAPI.Operations
{
    public interface IActivityOperations : IOperationStore
    {
        IQueryable<Activity> Activities { get; }
        IQueryable<ActivityType> ActivityTypes { get; }
        IQueryable<Module> Modules { get; }
        Task Create(Activity activity);
        Task Disable(int activityId);
        Task Update(Activity activity);
    }
    public class ActivityOperations : IActivityOperations
    {
        private readonly IDataSession _session;

        public ActivityOperations(IDataSession session)
        {
            _session = session;
        }

        public IQueryable<Activity> Activities
        {
            get
            {
                return _session.Activities.AsNoTracking();
            }
        }

        public IQueryable<ActivityType> ActivityTypes
        {
            get
            {
                return _session.ActivityTypes;
            }
        }

        public IQueryable<Module> Modules
        {
            get { return _session.Modules; }
        }

        public async Task Create(Activity activity)
        {
            _session.Activities.Add(activity);
            await _session.SaveChangesAsync();
        }

        public async Task Disable(int activityId)
        {
            _session.Activities.Single(a => a.ActivityId == activityId).Active = false;
            await _session.SaveChangesAsync();
        }

        public async Task Update(Activity activity)
        {
            _session.Entry(activity).Property(u => u.Active).IsModified = true;
            _session.Entry(activity).Property(u => u.ActivityDescription).IsModified = true;
            await _session.SaveChangesAsync();
        }
    }
}