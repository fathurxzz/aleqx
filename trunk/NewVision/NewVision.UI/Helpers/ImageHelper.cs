using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewVision.UI.Helpers
{
    public static class ImageHelper
    {
        public static void DeleteImage(string fileName, string filePath = null)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            IOHelper.DeleteFile(!string.IsNullOrEmpty(filePath) ? filePath : "~/Content/Images", fileName);

            foreach (var thumbnail in SiteSettings.Thumbnails)
            {
                IOHelper.DeleteFile("~/ImageCache/" + thumbnail.Key, fileName);
            }
        }
    }
}