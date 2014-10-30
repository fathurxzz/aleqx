using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions.Graphics;

namespace Leo
{
    public static class SiteSettings
    {
        public static Dictionary<string, ThumbnailPicture> Thumbnails { get; private set; }


        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, ThumbnailPicture>
                              {
                                  {"specialContentPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 150,Width = 200}, CacheFolder = "specialContentPreview", ScaleMode = ScaleMode.Crop}},
                                  {"productImagePreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 120,Width = 90}, CacheFolder = "productImagePreview", ScaleMode = ScaleMode.Crop}},
                                  {"productImageAdminPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 80,Width = 80}, CacheFolder = "productImageAdminPreview", ScaleMode = ScaleMode.Crop}},
                                  {"articleItemImagePreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 100,Width = 100}, CacheFolder = "articleItemImagePreview", ScaleMode = ScaleMode.Crop}},
                                  {"articleCaruselItem",new ThumbnailPicture{ PictureSize =new PictureSize {Height =391 ,Width = 522 }, CacheFolder = "articleCaruselItem", ScaleMode = ScaleMode.Crop}},
                                  {"documentPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height =66 ,Width = 66 }, CacheFolder = "documentPreview", ScaleMode = ScaleMode.Crop}},
                                  //{"adminPreviewProductImage",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 200,Width = 200}, CacheFolder = "adminPreviewProductImage", ScaleMode = ScaleMode.Crop}},

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

        public static string Version
        {
            get { return "0.0.15"; }
        }

    }
}