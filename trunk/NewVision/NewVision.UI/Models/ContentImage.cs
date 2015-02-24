using System;
using System.Collections.Generic;

namespace NewVision.UI.Models
{
    public partial class ContentImage
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
