using System.Collections.Generic;

namespace AccuV.DataAccess.Entities
{
    public partial class Market
    {
        public Market()
        {
            this.Sites = new List<Site>();
        }

        public string MarketId { get; set; }
        public string Region { get; set; }
        public string ManagerId { get; set; }
        public string Project_ID { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual ICollection<Site> Sites { get; set; }
        public virtual Project Project { get; set; }
    }
}
