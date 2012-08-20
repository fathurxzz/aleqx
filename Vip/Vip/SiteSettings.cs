using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions.Graphics;

namespace Vip
{
    public static class SiteSettings
    {
        public static Dictionary<string, PictureSize> Thumbnails { get; private set; }

        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, PictureSize>
                              {
                                  {"adminCategoryThumbnail", new PictureSize {Height = 100,Width = 100}},
                                  {"catalogueThumbnail", new PictureSize {Height = 202,Width = 202}},
                                  {"projectDetailsPreviewThumbnail", new PictureSize {Height = 79,Width = 79}},
                                  {"projectBigImage", new PictureSize {Height = 580,Width = 774}}
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