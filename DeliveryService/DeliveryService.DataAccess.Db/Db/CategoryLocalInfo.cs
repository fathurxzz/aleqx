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
    
    public partial class CategoryLocalInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CityId { get; set; }
        public int CategoryId { get; set; }
    
        public virtual City City { get; set; }
        public virtual Category Category { get; set; }
    }
}
