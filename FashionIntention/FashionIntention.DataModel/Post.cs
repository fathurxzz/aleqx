//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FashionIntention.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Post
    {
        public Post()
        {
            this.PostItems = new HashSet<PostItem>();
            this.Tags = new HashSet<Tag>();
        }
    
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageSrc { get; set; }
        public bool Published { get; set; }
    
        public virtual ICollection<PostItem> PostItems { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
