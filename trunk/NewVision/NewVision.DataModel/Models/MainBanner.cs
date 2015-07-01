using System;
using System.Collections.Generic;

namespace NewVision.DataModel.Models
{
    public partial class MainBanner
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TitleEn { get; set; }
        public string TitleUa { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionUa { get; set; }
    }
}
