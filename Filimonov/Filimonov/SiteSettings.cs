using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions.Graphics;

namespace Filimonov
{
    public static class SiteSettings
    {
        public static Dictionary<string, ThumbnailPicture> Thumbnails { get; private set; }

        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, ThumbnailPicture>
                              {
                                  //{"galleryImage",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 300,Width = 451}, CacheFolder = "galleryImage", ScaleMode = ScaleMode.Insert, Offset = 12}},
                                  //{"preview",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 200}, CacheFolder = "preview", ScaleMode = ScaleMode.FixedWidth}},
                                  //{"discount",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 293, Height = 193}, CacheFolder = "discount", ScaleMode = ScaleMode.Insert, Offset = 8}},
                                  //{"galleryPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 256, Height = 166}, CacheFolder = "galleryPreview", ScaleMode =  ScaleMode.Insert, Offset = 5}}
                                  {"projectPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 150, Height = 150}, CacheFolder = "projectPreview", ScaleMode =  ScaleMode.Crop}},
                                  {"caruselPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 73, Height = 73}, CacheFolder = "caruselPreview", ScaleMode =  ScaleMode.Crop}},
                                  {"projectImage",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 528, Height = 528}, CacheFolder = "projectImage", ScaleMode =  ScaleMode.Crop}},
                                  {"categoryPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 300, Height = 225}, CacheFolder = "categoryPreview", ScaleMode =  ScaleMode.Crop}}
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