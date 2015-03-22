using System;
using System.Collections.Generic;

namespace NewVision.UI.Models
{
    public partial class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public System.DateTime Date { get; set; }
        public int TitlePosition { get; set; }
        public string Text { get; set; }
        public int Size { get; set; }
        public string ImageSrc { get; set; }
        public string VideoSrc { get; set; }
        public virtual ICollection<ArticleImage> ArticleImages { get; set; }
    }
}
