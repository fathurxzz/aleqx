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
        public string TitleEn { get; set; }
        public string TitleUa { get; set; }
        public string TitleDescription { get; set; }
        public string TitleDescriptionEn { get; set; }
        public string TitleDescriptionUa { get; set; }
        public string HighlightedText { get; set; }
        public string HighlightedTextEn { get; set; }
        public string HighlightedTextUa { get; set; }
        public bool IsHighlighted { get; set; }
        public string LocationAddress { get; set; }
        public string LocationAddressEn { get; set; }
        public string LocationAddressUa { get; set; }
        public string LocationTitle { get; set; }
        public string LocationTitleEn { get; set; }
        public string LocationTitleUa { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionUa { get; set; }
        public int TicketOrderType { get; set; }
        public int PreviewContentType { get; set; }
        public string PreviewContentImageSrc { get; set; }
        public string Action { get; set; }
        public string ActionEn { get; set; }
        public string ActionUa { get; set; }
        public string Location { get; set; }
        public string LocationEn { get; set; }
        public string LocationUa { get; set; }
        public string ArtGroup { get; set; }
        public string ArtGroupEn { get; set; }
        public string ArtGroupUa { get; set; }
        public string Duration { get; set; }
        public string IntervalQuantity { get; set; }
        public string Price { get; set; }
        public string PreviewContentVideoSrc { get; set; }
        public string LocationAddressMapUrl { get; set; }
        public virtual ICollection<ContentImage> ContentImages { get; set; }
        public virtual ICollection<PreviewContentImage> PreviewContentImages { get; set; }
    }
}
