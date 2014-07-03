//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace leo_db
{
    using System;
    using System.Collections.Generic;
    
    public partial class Category
    {
        public Category()
        {
            this.CategoryLangs = new HashSet<CategoryLang>();
            this.Children = new HashSet<Category>();
            this.CategoryImages = new HashSet<CategoryImage>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public int SortOrder { get; set; }
    
        public virtual ICollection<CategoryLang> CategoryLangs { get; set; }
        public virtual ICollection<Category> Children { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<CategoryImage> CategoryImages { get; set; }
    }
}
