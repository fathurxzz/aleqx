using System;
using System.Collections.Generic;

namespace NewVision.UI.Models
{
    public partial class Content
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MenuTitle { get; set; }
        public string ImageSrc { get; set; }
        public string Text { get; set; }
        public int SortOrder { get; set; }
    }
}
