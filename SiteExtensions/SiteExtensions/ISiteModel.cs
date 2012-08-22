using System.Collections.Generic;

namespace SiteExtensions
{
    public interface ISiteModel
    {
        string Title { get; set; }
        string SeoDescription { get; set; }
        string SeoKeywords { get; set; }
        Menu Menu { get; set; }
    }
}
