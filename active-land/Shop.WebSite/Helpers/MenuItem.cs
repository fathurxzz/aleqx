using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.WebSite.Helpers
{
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
        /// <summary>
        /// 
        /// </summary>
        public List<MenuItem> Children { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MenuItem Parent { get; set; }

        public int Level { get; set; }

        public override string ToString()
        {
            return ContentName + " " + Title;
        }
    }
}