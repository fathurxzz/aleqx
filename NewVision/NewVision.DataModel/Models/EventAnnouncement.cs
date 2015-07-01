using System;
using System.Collections.Generic;

namespace NewVision.DataModel.Models
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
        public string TitleEn { get; set; }
        public string TitleUa { get; set; }
        public string TextEn { get; set; }
        public string TextUa { get; set; }
        public virtual ICollection<EventAnnouncementImage> EventAnnouncementImages { get; set; }
    }
}
