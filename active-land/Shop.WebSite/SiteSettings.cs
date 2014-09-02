using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Razor.Text;
using Shop.WebSite.Helpers.Graphics;

namespace Shop.WebSite
{
    public static class SiteSettings
    {
        public static Dictionary<string, ThumbnailPicture> Thumbnails { get; private set; }


        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, ThumbnailPicture>
                              {
                                    {"adminProductPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height =200 ,Width = 200 }, CacheFolder = "adminProductPreview", ScaleMode = ScaleMode.Crop}},
                                    {"siteProductPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height =215 ,Width = 268 }, CacheFolder = "siteProductPreview", ScaleMode = ScaleMode.Insert}},
                                    {"siteProductDetailsMainImage",new ThumbnailPicture{ PictureSize =new PictureSize {Height =344 ,Width = 597 }, CacheFolder = "siteProductDetailsMainImage", ScaleMode = ScaleMode.Insert}},
                                    {"siteProductDetailsThumbnail",new ThumbnailPicture{ PictureSize =new PictureSize {Height =90 ,Width = 90 }, CacheFolder = "siteProductDetailsThumbnail", ScaleMode = ScaleMode.Insert}},
                                    
                                    {"articlePreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height =130 ,Width = 276 }, CacheFolder = "articlePreview", ScaleMode = ScaleMode.Crop}},
                                    {"articleSmallPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height =133 ,Width = 133 }, CacheFolder = "articleSmallPreview", ScaleMode = ScaleMode.Crop}},
                                    {"articleAdminPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height =50 ,Width = 100 }, CacheFolder = "articleAdminPreview", ScaleMode = ScaleMode.Crop}},
                                    {"articleItemImageAdminPreview",new ThumbnailPicture{ PictureSize =new PictureSize {Height =50 ,Width = 50 }, CacheFolder = "articleItemImageAdminPreview", ScaleMode = ScaleMode.Crop}},
                                    {"articleCaruselItem",new ThumbnailPicture{ PictureSize =new PictureSize {Height =391 ,Width = 522 }, CacheFolder = "articleCaruselItem", ScaleMode = ScaleMode.Crop}},
                                    {"cartProductImage",new ThumbnailPicture{ PictureSize =new PictureSize {Height =154 ,Width = 154 }, CacheFolder = "cartProductImage", ScaleMode = ScaleMode.Crop}},

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
            get { return "0.0.10"; }
        }

        public static string MailTo
        {
            get { return "mailto:miller.kak.miller@gmail.com"; }
        }

        public static int AdminProductsPageSize = 50;
        public static int ProductsPageSize = 15;
    }
}