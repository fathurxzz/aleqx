using System;
using System.Collections.Generic;

namespace NewVision.UI.Models
{
    public partial class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string TitleUa { get; set; }
        public string Text { get; set; }
        public string TextEn { get; set; }
        public string TextUa { get; set; }
        public int SortOrder { get; set; }
        public string ImageSrc { get; set; }
        public string VideoSrc { get; set; }
        public int ContentType { get; set; }
    }
}
