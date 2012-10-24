using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteExtensions.Graphics
{
    /// <summary>
    /// 
    /// </summary>
    public class ThumbnailPicture
    {
        public PictureSize PictureSize { get; set; }
        
        public string CacheFolder { get; set; }

        public int Offset { get; set; }

        public ScaleMode ScaleMode { get; set; }

        public bool UseBackgroundImage { get; set; }

    }
}
