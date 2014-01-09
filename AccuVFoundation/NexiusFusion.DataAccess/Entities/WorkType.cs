using System.Collections.Generic;

namespace NexiusFusion.DataAccess.Entities
{
    public partial class WorkType
    {
        public WorkType()
        {
            this.Reports = new List<Report>();
        }

        public int WorkTypeId { get; set; }
        public string WorkTypeDescription { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
