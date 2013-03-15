using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions.Graphics;

namespace Kulumu
{
    public static class SiteSettings
    {
        public static Dictionary<string, ThumbnailPicture> Thumbnails { get; private set; }


        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, ThumbnailPicture>
                              {
                                  {"galleryImage",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 300,Width = 451}, CacheFolder = "galleryImage", ScaleMode = ScaleMode.Insert, Offset = 12}},
                                  {"preview",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 200}, CacheFolder = "preview", ScaleMode = ScaleMode.FixedWidth}},
                                  {"discount",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 293, Height = 193}, CacheFolder = "discount", ScaleMode = ScaleMode.Insert, Offset = 8}},
                                  //{"galleryPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 256, Height = 166}, CacheFolder = "galleryPreview", ScaleMode =  ScaleMode.Insert, Offset = 5}}
                                  {"galleryPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 380, Height = 245}, CacheFolder = "galleryPreview", ScaleMode =  ScaleMode.Insert, Offset = 5}}
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