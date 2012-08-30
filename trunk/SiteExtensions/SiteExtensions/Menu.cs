using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public class Menu : List<MenuItem>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ContentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ContentName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Selected { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Current { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SortOrder { get; set; }
    }
}
