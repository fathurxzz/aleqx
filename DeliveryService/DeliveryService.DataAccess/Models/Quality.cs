using System;
using System.Collections.Generic;

namespace DeliveryService.DataAccess.Models
{
    public partial class Quality
    {
        public Quality()
        {
            this.Companies = new List<Company>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }
}
