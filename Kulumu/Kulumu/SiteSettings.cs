using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions.Graphics;

namespace Kulumu
{
    public static class SiteSettings
    {
        public static Dictionary<string, PictureSize> Thumbnails { get; private set; }

        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, PictureSize>
                              {
                                  {"galleryImage",new PictureSize {Height = 300,Width = 451}},
                                  {"preview",new PictureSize {Width = 200}},
                                  {"discount",new PictureSize {Width = 293, Height = 193}}
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