using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions.Graphics;

namespace Rakurs.Models
{
    public enum Language
    {
        En,
        Ru
    }
    public static class SiteSettings
    {
        public static string GetCurrentLanguage
        {
            get
            {
                if (HttpContext.Current.Session["lang"] == null)
                {
                    HttpContext.Current.Session["lang"] = "ru-RU";
                }
                return HttpContext.Current.Session["lang"].ToString();
            }
        }

        public static void SetCurrentLanguage(string lang)
        {
            HttpContext.Current.Session["lang"] = lang;
        }

        public static Language Language
        {
            get
            {
                return GetCurrentLanguage == "ru-RU" ? Language.Ru : Language.En;
            }
        }


        public static Dictionary<string, ThumbnailPicture> Thumbnails { get; private set; }

        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, ThumbnailPicture>
                              {
                                  {"galleryThumbnail",new ThumbnailPicture{ PictureSize =new PictureSize {Height = 156,Width = 157}, CacheFolder = "galleryThumbnail", ScaleMode = ScaleMode.Insert}},
                                  {"mainFrameThumbnail",new ThumbnailPicture{ PictureSize =new PictureSize {Width = 1000, Height = 514}, CacheFolder = "mainFrameThumbnail", ScaleMode = ScaleMode.Crop}}
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