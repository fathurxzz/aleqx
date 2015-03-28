using System;
using System.Collections.Generic;

namespace FashionIntention.UI.Models
{
    public partial class Tag
    {
        public Tag()
        {
            this.Posts = new List<Post>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
