using System.Collections.Generic;

namespace Kiki.DataAccess.Entities
{
    public partial class Service
    {
        public Service()
        {
            this.ServiceItems = new List<ServiceItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string TitleEng { get; set; }
        public string Description { get; set; }
        public string DescriptionEng { get; set; }
        public string ImageSource { get; set; }
        public int SortOrder { get; set; }
        public virtual ICollection<ServiceItem> ServiceItems { get; set; }
    }
}
