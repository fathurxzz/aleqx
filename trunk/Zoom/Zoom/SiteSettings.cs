using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions.Graphics;

namespace Zoom
{
    public static class SiteSettings
    {
        public static Dictionary<string, ThumbnailPicture> Thumbnails { get; private set; }

        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, ThumbnailPicture>
                              {
                                  //{ "adminCategoryThumbnail", new ThumbnailPicture{ PictureSize = new PictureSize {Height = 100,Width = 100}, CacheFolder = "adminCategoryThumbnail", ScaleMode = ScaleMode.Crop, UseBackgroundImage = false}},
                                  //{ "catalogueThumbnail", new ThumbnailPicture{ PictureSize = new PictureSize {Height = 202,Width = 202}, CacheFolder = "catalogueThumbnail", ScaleMode = ScaleMode.Crop, UseBackgroundImage = false}},
                                  //{ "projectDetailsPreviewThumbnail", new ThumbnailPicture{ PictureSize = new PictureSize {Height = 79,Width = 79}, CacheFolder = "projectDetailsPreviewThumbnail", ScaleMode = ScaleMode.Crop, UseBackgroundImage = false}},
                                  //{ "projectBigImage", new ThumbnailPicture{ PictureSize = new PictureSize {Height = 580,Width = 774}, CacheFolder = "projectBigImage", ScaleMode = ScaleMode.Crop, UseBackgroundImage = false}},
                                  //{ "catalogueMainImage", new ThumbnailPicture{ PictureSize = new PictureSize {Height = 415,Width = 702}, CacheFolder = "catalogueMainImage", ScaleMode = ScaleMode.Insert, UseBackgroundImage = true}},
                                  //{ "mainPageImage", new ThumbnailPicture{ PictureSize = new PictureSize {Height = 350,Width = 1164}, CacheFolder = "mainPageImage", ScaleMode = ScaleMode.Crop, UseBackgroundImage = false}},
                                  { "preview", new ThumbnailPicture{ PictureSize = new PictureSize {Height = 600,Width = 800}, CacheFolder = "preview", ScaleMode = ScaleMode.Crop, UseBackgroundImage = false}}
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