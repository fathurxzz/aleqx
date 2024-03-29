//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeliveryService.DataAccess.Db.Db
{
    using System;
    using System.Collections.Generic;
    
    public partial class CompanyLocalInfo
    {
        public CompanyLocalInfo()
        {
            this.Criteria = new HashSet<Criteria>();
        }
    
        public int Id { get; set; }
        public string WorkTime { get; set; }
        public int CompanyId { get; set; }
        public int CityId { get; set; }
        public string Description { get; set; }
        public decimal DeliveryCost { get; set; }
        public string MinOrderAmount { get; set; }
        public string Title { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Criteria> Criteria { get; set; }
    }
}
