using System;
using System.Collections.Generic;

namespace FashionIntention.UI.Models
{
    public partial class ArticleItem
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
        public string Text { get; set; }
        public int SortOrder { get; set; }
        public int ArticleId { get; set; }
        public string VideoSrc { get; set; }
        public virtual Article Article { get; set; }
    }
}
