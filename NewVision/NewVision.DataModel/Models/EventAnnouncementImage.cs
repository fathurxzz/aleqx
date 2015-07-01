using System;
using System.Collections.Generic;

namespace NewVision.DataModel.Models
{
    public partial class EventAnnouncementImage
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
        public int EventAnnouncementId { get; set; }
        public virtual EventAnnouncement EventAnnouncement { get; set; }
    }
}
