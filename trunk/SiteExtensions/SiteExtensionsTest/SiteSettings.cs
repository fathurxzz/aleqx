using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions.Graphics;

namespace SiteExtensionsTest
{
    public static class SiteSettings
    {
        public static Dictionary<string, PictureSize> Thumbnails { get; private set; }

        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, PictureSize>
                              {
                                  {"thumb", new PictureSize {Height = 200,Width = 500}},
                                  {"thumb2", new PictureSize {Height = 200,Width = 100}},
                                  {"thumb3", new PictureSize {Height = 100,Width = 200}},
                                  {"thumb4", new PictureSize {Height = 100,Width = 200}},
                                  {"thumb5", new PictureSize {Height = 100,Width = 200}},
                                  {"thumb6", new PictureSize {Height = 200,Width = 100}},
                                  {"thumb7", new PictureSize {Height = 100,Width = 200}},
                                  {"thumb8", new PictureSize {Height = 200,Width = 100}},
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