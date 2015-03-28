using System;
using System.Collections.Generic;

namespace FashionIntention.UI.Models
{
    public partial class PostItem
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
        public string Text { get; set; }
        public int SortOrder { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
