using System.Collections.Generic;

namespace AccuV.DataAccess.Entities
{
    public partial class Project
    {
        public Project()
        {
            this.Markets = new List<Market>();
            this.Sites = new List<Site>();
            this.Tasks = new List<Task>();
        }

        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public virtual ICollection<Site> Sites { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Market> Markets { get; set; }
    }
}
