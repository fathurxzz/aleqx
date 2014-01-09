using System.Collections.Generic;

namespace AccuV.DataAccess.Entities
{
    public partial class Manager
    {
        public Manager()
        {
            this.Sites = new List<Site>();
            this.Markets = new List<Market>();
        }

        public string ManagerId { get; set; }
        public string ManagerName { get; set; }
        public virtual ICollection<Site> Sites { get; set; }
        public virtual ICollection<Market> Markets { get; set; }
    }
}
