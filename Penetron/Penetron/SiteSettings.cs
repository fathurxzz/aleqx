using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions.Graphics;

namespace Penetron
{
    public static class SiteSettings
    {
        public static Dictionary<string, ThumbnailPicture> Thumbnails { get; private set; }


        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, ThumbnailPicture>
                         {
                             {
                                 "carusel",
                                 new ThumbnailPicture
                                 {
                                     PictureSize = new PictureSize {Height = 359, Width = 610},
                                     CacheFolder = "carusel",
                                     ScaleMode = ScaleMode.Crop
                                 }
                             },
                             {
                                 "slider",
                                 new ThumbnailPicture
                                 {
                                     PictureSize = new PictureSize {Height = 337, Width = 1200},
                                     CacheFolder = "slider",
                                     ScaleMode = ScaleMode.Crop
                                 }
                             },
                             {
                                 "sliderPreview",
                                 new ThumbnailPicture
                                 {
                                     PictureSize = new PictureSize {Height = 85, Width = 300},
                                     CacheFolder = "sliderPreview",
                                     ScaleMode = ScaleMode.Crop
                                 }
                             }
                         };
        }


        public static ThumbnailPicture GetThumbnail(string cacheFolder)
        {
            if (Thumbnails.ContainsKey(cacheFolder))
                return Thumbnails[cacheFolder];
            throw new Exception("Can't find thumbnail " + cacheFolder);
        }

    }
}