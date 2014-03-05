using System.Collections.Generic;
using SiteExtensions;

namespace Mayka.Helpers
{
    public class ContentHeaderModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    public class UIMenuItem
    {

    }

    public class MenuItem
    {
        public string Title { get; set; }
        public int ContentId { get; set; }
        public string ContentName { get; set; }
        public bool Selected { get; set; }
        public bool Current { get; set; }
        public int SortOrder { get; set; }
        public ContentType ContentType { get; set; }
    }

    public enum ContentType
    {
        HomePage = 0,
        Content = 1,
        Gallery = 2,
        Products = 3
    }
}