using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions.Graphics;

namespace Shop
{
    public static class SiteSettings
    {
        public static Dictionary<string, ThumbnailPicture> Thumbnails { get; private set; }

        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, ThumbnailPicture>
                              {
                                  {"productPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 173,Width = 175}, CacheFolder = "productPreview", ScaleMode = ScaleMode.Crop}},
                                  {"productImage",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 492,Width = 365}, CacheFolder = "productImage", ScaleMode = ScaleMode.Crop}},
                                  {"thumb0",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 100,Width = 100}, CacheFolder = "thumb0", ScaleMode = ScaleMode.Crop}}
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