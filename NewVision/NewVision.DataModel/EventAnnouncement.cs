//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewVision.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class EventAnnouncement
    {
        public EventAnnouncement()
        {
            this.EventAnnouncementImages = new HashSet<EventAnnouncementImage>();
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
