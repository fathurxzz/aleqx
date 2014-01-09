using System;
using System.Collections.Generic;

namespace AccuV.DataAccess.Entities
{
    public partial class Activity
    {
        public Activity()
        {
            this.UserActivities = new List<UserActivity>();
            this.Sites = new List<Site>();
            this.Tasks = new List<Task>();
        }

        public int ActivityId { get; set; }
        public string ActivityDescription { get; set; }
        public int ModuleId { get; set; }
        public int ActivityTypeId { get; set; }
        public bool Active { get; set; }
        public virtual ActivityType ActivityType { get; set; }
        public virtual Module Module { get; set; }
        public virtual ICollection<UserActivity> UserActivities { get; set; }
        public virtual ICollection<Site> Sites { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
