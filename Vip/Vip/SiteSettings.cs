using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using SiteExtensions.Graphics;

namespace Vip
{
    public static class SiteSettings
    {
        public static Dictionary<string, PictureSize> Thumbnails { get; private set; }

        public static int PageSize { get; private set; }

        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, PictureSize>
                              {
                                  {"adminCategoryThumbnail", new PictureSize {Height = 100,Width = 100}},
                                  {"catalogueThumbnail", new PictureSize {Height = 202,Width = 202}},
                                  {"projectDetailsPreviewThumbnail", new PictureSize {Height = 79,Width = 79}},
                                  {"projectBigImage", new PictureSize {Height = 580,Width = 774}}
                              };

            PageSize = GetPageSize();
        }

        private static int GetPageSize()
        {
            if (ConfigurationManager.AppSettings["PageSize"] == null)
            {
                throw new Exception("Невозможно получить кофигурацию пейджинга. В appsettings должен быть параметр с именем PageSize");
            }
            return Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
        }

        public static ThumbnailPicture GetThumbnail(string cacheFolder)
        {
            if (Thumbnails.ContainsKey(cacheFolder))
                return new ThumbnailPicture { CacheFolder = cacheFolder, PictureSize = Thumbnails[cacheFolder] };
            throw new Exception("Cannot fount thumbnail " + cacheFolder);
        }
    }
}