﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace Vip.Helpers
{
    public static class ImageHelper
    {
        public static void DeleteImage(string fileName)
        {
            if(string.IsNullOrEmpty(fileName))
                return;
            IOHelper.DeleteFile("~/Content/Images", fileName);
            foreach (var thumbnail in SiteSettings.Thumbnails)
            {
                IOHelper.DeleteFile("~/ImageCache/" + thumbnail.Key, fileName);
            }
        }
    }
}