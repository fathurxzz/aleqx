using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions.Graphics;

namespace EM2013
{
    public static class SiteSettings
    {
        public static Dictionary<string, PictureSize> Thumbnails { get; private set; }

        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, PictureSize>
                              {
                                  {"productItem", new PictureSize {Width = 712}}
                              };

        }

        public static ThumbnailPicture GetThumbnail(string cacheFolder)
        {
            if (Thumbnails.ContainsKey(cacheFolder))
                return new ThumbnailPicture { CacheFolder = cacheFolder, PictureSize = Thumbnails[cacheFolder] };
            throw new Exception("Cannot fount thumbnail " + cacheFolder);
        }


    }
}