using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using SiteExtensions.Graphics;

namespace EM2013
{
    public static class SiteSettings
    {
        public static Dictionary<string, PictureSize> Thumbnails { get; private set; }

        public static int PageSize { get; private set; }
        public static int PageRange { get; private set; }

        static SiteSettings()
        {
            Thumbnails = new Dictionary<string, PictureSize>
                              {
                                  {"productItem", new PictureSize {Width = 712}}
                              };
            PageSize = GetPageSize();
            PageRange = GetPageRange();
        }

        public static ThumbnailPicture GetThumbnail(string cacheFolder)
        {
            if (Thumbnails.ContainsKey(cacheFolder))
                return new ThumbnailPicture { CacheFolder = cacheFolder, PictureSize = Thumbnails[cacheFolder] };
            throw new Exception("Cannot fount thumbnail " + cacheFolder);
        }

        private static int GetPageSize()
        {
            if (ConfigurationManager.AppSettings["PageSize"] == null)
            {
                throw new Exception("Невозможно получить кофигурацию пейджинга. В appsettings должен быть параметр с именем PageSize");
            }
            return Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
        }

        private static int GetPageRange()
        {
            if (ConfigurationManager.AppSettings["PageRange"] == null)
            {
                throw new Exception("Невозможно получить кофигурацию пейджинга. В appsettings должен быть параметр с именем PageRange");
            }
            return Convert.ToInt32(ConfigurationManager.AppSettings["PageRange"]);
        }
    }
}