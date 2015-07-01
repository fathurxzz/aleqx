using System;
using System.Collections.Generic;

namespace NewVision.DataModel.Models
{
    public partial class Content
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MenuTitle { get; set; }
        public string ImageSrc { get; set; }
        public string Text { get; set; }
        public int SortOrder { get; set; }
        public string Name { get; set; }
        public string TitleEn { get; set; }
        public string TitleUa { get; set; }
        public string MenuTitleEn { get; set; }
        public string MenuTitleUa { get; set; }
        public string TextEn { get; set; }
        public string TextUa { get; set; }
    }
}
