﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.WebSite.Helpers.Graphics
{
    public class ThumbnailPicture
    {
        public PictureSize PictureSize { get; set; }

        public string CacheFolder { get; set; }

        public int Offset { get; set; }

        public ScaleMode ScaleMode { get; set; }

        public bool UseBackgroundImage { get; set; }

    }
}