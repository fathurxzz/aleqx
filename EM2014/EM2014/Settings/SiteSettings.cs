using System;
using System.Collections.Generic;
using SiteExtensions.Graphics;

namespace EM2014.Settings
{
    public static class SiteSettings
    {
        public static Dictionary<string, ThumbnailPicture> Thumbnails { get; private set; }


        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, ThumbnailPicture>
                              {
                                  {"sitePreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 172,Width = 305}, CacheFolder = "sitePreview", ScaleMode = ScaleMode.Crop}}
                              };
        }


        public static ThumbnailPicture GetThumbnail(string cacheFolder)
        {
            if (Thumbnails.ContainsKey(cacheFolder))
                return Thumbnails[cacheFolder];
            throw new Exception("Can't find thumbnail " + cacheFolder);
        }

        public static string Version {
            get { return "0.0.1"; }
        }
    }

}