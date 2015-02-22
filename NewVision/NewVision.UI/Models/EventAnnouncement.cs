using System;
using System.Collections.Generic;

namespace NewVision.UI.Models
{
    public partial class EventAnnouncement
    {
        public EventAnnouncement()
        {
            this.EventAnnouncementImages = new List<EventAnnouncementImage>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public virtual ICollection<EventAnnouncementImage> EventAnnouncementImages { get; set; }
    }
}
