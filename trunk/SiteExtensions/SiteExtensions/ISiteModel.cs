using System.Collections.Generic;

namespace SiteExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISiteModel
    {
        /// <summary>
        /// 
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string SeoDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string SeoKeywords { get; set; }
        /// <summary>
        /// 
        /// </summary>
        Menu Menu { get; set; }
    }
}
