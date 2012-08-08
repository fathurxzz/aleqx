using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions.Graphics;

namespace Leo
{
    public static class SiteSettings
    {
        public static Dictionary<string, PictureSize> _thumbnails;

        static SiteSettings()
        {
            _thumbnails.Add("galleryThumbnail", new PictureSize { Height = 315 });
        }

        public static ThumbnailPicture GetThumbnail(string cacheFilder)
        {
            if (_thumbnails.ContainsKey(cacheFilder))
                return new ThumbnailPicture {CacheFolder = cacheFilder, PictureSize = _thumbnails[cacheFilder]};
            throw new Exception("Cannot fount thumbnail " + cacheFilder);
        }
    }
}