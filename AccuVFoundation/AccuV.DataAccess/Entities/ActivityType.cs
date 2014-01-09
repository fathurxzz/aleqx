using System.Collections.Generic;

namespace AccuV.DataAccess.Entities
{
    public partial class ActivityType
    {
        public ActivityType()
        {
            this.Activities = new List<Activity>();
            this.Sites = new List<Site>();
            this.Tasks = new List<Task>();
        }

        public int ActivityTypeId { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Site> Sites { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
