using System.Collections.Generic;

namespace AccuV.DataAccess.Entities
{
    public partial class Task
    {
        public Task()
        {
            this.RolloutTracking = new List<RolloutTracking>();
        }

        public string TaskId { get; set; }
        public string ProjectId { get; set; }
        public string TaskName { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<RolloutTracking> RolloutTracking { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<ActivityType> ActivityTypes { get; set; }
    }
}
