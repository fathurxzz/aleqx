//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewVision.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Category
    {
        public Category()
        {
            this.AuthorCategories = new HashSet<AuthorCategory>();
            this.CategoryLangs = new HashSet<CategoryLang>();
            this.Tags = new HashSet<Tag>();
        }
    
        public int Id { get; set; }
        public int SortOrder { get; set; }
    
        public virtual ICollection<AuthorCategory> AuthorCategories { get; set; }
        public virtual ICollection<CategoryLang> CategoryLangs { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
