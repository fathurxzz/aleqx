using System;
using System.Collections.Generic;
using SiteExtensions.Graphics;

namespace Mayka
{
    public static class SiteSettings
    {
        public static Dictionary<string, ThumbnailPicture> Thumbnails { get; private set; }


        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, ThumbnailPicture>
                              {
                                  {"galleryImage",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 680,Width = 680}, CacheFolder = "galleryImage", ScaleMode = ScaleMode.Crop}},
                                  //{"brandImagePreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 150,Width = 215}, CacheFolder = "brandImagePreview", ScaleMode = ScaleMode.Crop}},
                                  //{"factoryCatalogueCategoryPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 212,Width = 213}, CacheFolder = "factoryCatalogueCategoryPreview", ScaleMode = ScaleMode.Crop}},
                                  //{"articleImage",new ThumbnailPicture{ PictureSize = new PictureSize {Width = 740}, CacheFolder = "articleImage", ScaleMode = ScaleMode.FixedWidth}},
                                  //{"designerAdminPreview",new ThumbnailPicture{ PictureSize = new PictureSize {Width = 100,Height = 100}, CacheFolder = "designerAdminPreview", ScaleMode = ScaleMode.Crop}},
                                  //{"portfolioPreview",new ThumbnailPicture{ PictureSize = new PictureSize {Width = 213,Height = 213}, CacheFolder = "portfolioPreview", ScaleMode = ScaleMode.Crop}}
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