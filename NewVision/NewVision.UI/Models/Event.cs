using System;
using System.Collections.Generic;

namespace NewVision.UI.Models
{
    public partial class Event
    {
        public Event()
        {
            this.ContentImages = new List<ContentImage>();
            this.PreviewContentImages = new List<PreviewContentImage>();
        }

        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public string Title { get; set; }
        public string TitleDescription { get; set; }
        public string HighlightedText { get; set; }
        public bool IsHighlighted { get; set; }
        public string LocationAddress { get; set; }
        public string LocationTitle { get; set; }
        public string Description { get; set; }
        public int TicketOrderType { get; set; }
        public int PreviewContentType { get; set; }
        public string PreviewContentSrc { get; set; }
        public string Action { get; set; }
        public string Location { get; set; }
        public string ArtGroup { get; set; }
        public string Duration { get; set; }
        public string IntervalQuantity { get; set; }
        public string Price { get; set; }
        public virtual ICollection<ContentImage> ContentImages { get; set; }
        public virtual ICollection<PreviewContentImage> PreviewContentImages { get; set; }
    }
}
