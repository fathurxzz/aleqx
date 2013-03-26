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
                                  {"galleryImage",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 300,Width = 440}, CacheFolder = "galleryImage", ScaleMode = ScaleMode.Insert}},
                                  {"preview",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 200}, CacheFolder = "preview", ScaleMode = ScaleMode.FixedWidth}},
                                  {"discount",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 293, Height = 193}, CacheFolder = "discount", ScaleMode = ScaleMode.Insert, Offset = 8}},
                                  {"galleryPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 255, Height = 166}, CacheFolder = "galleryPreview", ScaleMode =  ScaleMode.Insert}},
                                  {"productThumb",new ThumbnailPicture{ PictureSize =new PictureSize {Width =70, Height = 70}, CacheFolder = "productThumb", ScaleMode =  ScaleMode.Crop}},
                                  {"productPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Width =378, Height = 245}, CacheFolder = "productPreview", ScaleMode =  ScaleMode.Crop}},
                                  {"workPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 255, Height = 166}, CacheFolder = "workPreview", ScaleMode =  ScaleMode.Crop}},
                                  {"bigPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 1200, Height = 630}, CacheFolder = "bigPreview", ScaleMode =  ScaleMode.Crop}},
                                  {"bannerPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 430, Height = 140}, CacheFolder = "bannerPreview", ScaleMode =  ScaleMode.Crop}},
                                  {"banner",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 857, Height = 273}, CacheFolder = "banner", ScaleMode =  ScaleMode.Crop}},
                                  {"articlePreview",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 200, Height = 135}, CacheFolder = "articlePreview", ScaleMode =  ScaleMode.Crop}}
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