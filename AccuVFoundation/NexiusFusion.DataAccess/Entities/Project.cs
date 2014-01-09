using System.Collections.Generic;

namespace NexiusFusion.DataAccess.Entities
{
    public partial class Project
    {
        public Project()
        {
            this.Sites = new List<Site>();
        }

        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public virtual ICollection<Site> Sites { get; set; }
    }
}
