using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions.Graphics;

namespace Listelli
{
    public static class SiteSettings
    {
        public static Dictionary<string, ThumbnailPicture> Thumbnails { get; private set; }


        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, ThumbnailPicture>
                              {
                                  {"brandPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 175,Width = 175}, CacheFolder = "brandPreview", ScaleMode = ScaleMode.Crop}},
                                  {"brandImagePreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 150,Width = 215}, CacheFolder = "brandImagePreview", ScaleMode = ScaleMode.Crop}}
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