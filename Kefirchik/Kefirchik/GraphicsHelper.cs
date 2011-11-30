using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Kefirchik
{
    public static class GraphicsHelper
    {

        private static Rectangle CalculateDestRect(int limWidth, int limHeight)
        {
            return new Rectangle(0, 0, limWidth, limHeight);
        }

        private static Rectangle CalculateSourceRect(Size sourceImage, int previewWidth, int previewHeight)
        {
            int resultWidth;
            int resultHeight;

            double wRatio = (double)sourceImage.Width / (double)previewWidth;
            double hRatio = (double)sourceImage.Height / (double)previewHeight;

            double coef = (double)previewHeight / (double)previewWidth;

            if (wRatio < hRatio)
            {
                resultWidth = sourceImage.Width;
                resultHeight = (int)Math.Truncate(sourceImage.Width * coef);
            }
            else
            {
                resultHeight = sourceImage.Height;
                resultWidth = (int)Math.Truncate(sourceImage.Height / coef);
            }

            return new Rectangle(0, 0, resultWidth, resultHeight);
        }

        private static void ScaleImage(Bitmap image, int limWidth, int limHeight, Stream saveTo)
        {
            Rectangle sourceRect = CalculateSourceRect(image.Size,limWidth,limHeight);
            Rectangle destRect = CalculateDestRect(limWidth, limHeight);
            Bitmap thumbnailImage = new Bitmap(destRect.Width, destRect.Height);
            var graphics = System.Drawing.Graphics.FromImage(thumbnailImage);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(image, destRect, sourceRect, GraphicsUnit.Pixel);
            thumbnailImage.Save(saveTo, System.Drawing.Imaging.ImageFormat.Jpeg);
            saveTo.Position = 0;
        }

        private static string GetCachedImage(string originalPath, string fileName, string cacheFolder, int width,int height)
        {
            if (string.IsNullOrEmpty(fileName) ||
                !File.Exists(Path.Combine(HttpContext.Current.Server.MapPath(originalPath), fileName)))
            {
                fileName = "NoImage.jpg";
                if (!File.Exists(Path.Combine(HttpContext.Current.Server.MapPath(originalPath), fileName)))
                {
                    return null;
                }
            }

            string result = Path.Combine("/ImageCache/" + cacheFolder + "/", fileName);
            string cachePath = HttpContext.Current.Server.MapPath("~/ImageCache/" + cacheFolder);

            if (!Directory.Exists(cachePath)) Directory.CreateDirectory(cachePath);

            string cachedImagePath = Path.Combine(cachePath, fileName);
            if (File.Exists(cachedImagePath))
            {
                return result;
            }
            else
            {
                try
                {
                    CacheImage(originalPath, fileName, cacheFolder,width,height);
                }
                catch
                {
                    return GetCachedImage(originalPath, "nophoto.gif", cacheFolder,width,height);
                }
                return result;
            }
        }

        private static void CacheImage(string originalPath, string fileName, string cacheFolder, int width, int height)
        {
            string sourcePath = Path.Combine(HttpContext.Current.Server.MapPath(originalPath), fileName);
            Bitmap image;
            using (var stream = new FileStream(sourcePath, FileMode.Open))
            {
                image = new Bitmap(stream);
            }

            string cachePath = HttpContext.Current.Server.MapPath("~/ImageCache/" + cacheFolder);
            string cachedImagePath = Path.Combine(cachePath, fileName);

            using (var stream = new FileStream(cachedImagePath, FileMode.CreateNew))
            {
                ScaleImage(image, width, height, stream);
            }
        }

        /// <summary>
        /// Gets picture from the cache folder
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="options"></param>
        /// <param name="fileName"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        public static string CachedImage(this HtmlHelper helper, ThumbnailOptions options, string fileName, string alt)
        {
            return CachedImage(helper, options.OriginalPath, options.Width, options.Height, options.CacheFolder, fileName, alt);
        }

        /// <summary>
        /// Gets picture from the cache folder
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="originalPath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="cacheFolder"></param>
        /// <param name="fileName"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        public static string CachedImage(this HtmlHelper helper, string originalPath, int width, int height,  string cacheFolder, string fileName, string alt)
        {
            var sb = new StringBuilder();
            string formatString = "<img src=\"{0}\" alt=\"{1}\" />";
            sb.AppendFormat(formatString, GetCachedImage(originalPath, fileName, cacheFolder,width,height), alt);
            return sb.ToString();
        }
    }
}
