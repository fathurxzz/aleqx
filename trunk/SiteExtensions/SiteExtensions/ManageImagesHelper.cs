using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ManageImagesHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalImagePath"></param>
        /// <param name="cachedImagePath"></param>
        /// <param name="thumbnailsPath"></param>
        /// <param name="fileName"></param>
        public static void DeleteImages(string originalImagePath, string cachedImagePath, string[] thumbnailsPath, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;
            IOHelper.DeleteFile(originalImagePath, fileName);
            foreach (string thumbnail in thumbnailsPath)
            {
                IOHelper.DeleteFile(cachedImagePath + thumbnail, fileName);
            }
        }
    }
}
