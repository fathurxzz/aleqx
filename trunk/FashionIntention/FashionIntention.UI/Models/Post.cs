using System;
using System.Collections.Generic;

namespace FashionIntention.UI.Models
{
    public partial class Post
    {
        public Post()
        {
            this.PostItems = new List<PostItem>();
            this.Tags = new List<Tag>();
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
