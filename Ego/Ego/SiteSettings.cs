using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions.Graphics;

namespace Ego
{
    public static class SiteSettings
    {
        public static Dictionary<string, ThumbnailPicture> Thumbnails { get; private set; }

        public static int PageSize{ get { return 6; } }

        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, ThumbnailPicture>
                              {
                                  {"productPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height =168 , Width =118 }, CacheFolder = "productPreview", ScaleMode = ScaleMode.Crop}},
                                  {"product",new ThumbnailPicture{ PictureSize =new PictureSize {Height =446 , Width = 316}, CacheFolder = "product", ScaleMode = ScaleMode.Crop}},
                                  {"order",new ThumbnailPicture{ PictureSize =new PictureSize {Height =200 , Width = 200}, CacheFolder = "order", ScaleMode = ScaleMode.Crop}}
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