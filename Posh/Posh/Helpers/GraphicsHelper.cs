using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Posh.Helpers
{
    public enum ScaleMode
    {
        Corp,
        Insert
    }

    public static class GraphicsHelper
    {
        private static Dictionary<string, int> limitHeight = new Dictionary<string, int>();
        private static Dictionary<string, int> limitWidth = new Dictionary<string, int>();

        public static string[] ThumbnailFolders = { "thumbnail0", "thumbnail1", "thumbnail2", "thumbnail3" };
        public static int[] ThumbnailDimentions = { 177, 150, 298, 53 };


        static GraphicsHelper()
        {
            limitWidth.Add(ThumbnailFolders[0], ThumbnailDimentions[0]);
            limitHeight.Add(ThumbnailFolders[0], ThumbnailDimentions[0]);

            limitWidth.Add(ThumbnailFolders[1], ThumbnailDimentions[1]);
            limitHeight.Add(ThumbnailFolders[1], ThumbnailDimentions[1]);

            limitWidth.Add(ThumbnailFolders[2], ThumbnailDimentions[2]);
            limitHeight.Add(ThumbnailFolders[2], ThumbnailDimentions[2]);

            limitWidth.Add(ThumbnailFolders[3], ThumbnailDimentions[3]);
            limitHeight.Add(ThumbnailFolders[3], ThumbnailDimentions[3]);
        }

        private static Rectangle CalculateSourceRect(string name, Size sourceImage, ScaleMode scaleMode)
        {
            int previewHeight = limitHeight.ContainsKey(name) ? limitHeight[name] : 0;
            int previewWidth = limitWidth.ContainsKey(name) ? limitWidth[name] : 0;

            int resultWidth;
            int resultHeight;

            if (scaleMode == ScaleMode.Corp)
            {

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
            else
            {
                if (sourceImage.Width > sourceImage.Height)
                {
                    int shift = (int)Math.Truncate((sourceImage.Width - sourceImage.Height) / (double)2);
                    return new Rectangle(0, -shift, sourceImage.Width, sourceImage.Height + shift * 2);
                }
                else
                {
                    int shift = (int)Math.Truncate((sourceImage.Height - sourceImage.Width) / (double)2);
                    return new Rectangle(-shift, 0, sourceImage.Width + shift * 2, sourceImage.Height);
                }
            }
        }

        public static void ScaleImage(string name, Bitmap image, int limWidth, int limHeight, Stream saveTo, ScaleMode scaleMode)
        {
            Rectangle sourceRect = CalculateSourceRect(name, image.Size, scaleMode);

            Rectangle destRect = new Rectangle(0, 0, limWidth, limHeight);

            Bitmap thumbnailImage = new Bitmap(destRect.Width, destRect.Height);

            Graphics graphics = Graphics.FromImage(thumbnailImage);
            graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, destRect.Width, destRect.Height);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(image, destRect, sourceRect, GraphicsUnit.Pixel);

            thumbnailImage.Save(saveTo, System.Drawing.Imaging.ImageFormat.Jpeg);
            saveTo.Position = 0;
        }

        public static string GetCachedImage(string originalPath, string fileName, string cacheFolder, ScaleMode scaleMode)
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

            try
            {
                CacheImage(originalPath, fileName, cacheFolder, scaleMode);
            }
            catch
            {
                return GetCachedImage(originalPath, "nophoto.gif", cacheFolder, scaleMode);
            }
            return result;
        }

        private static void CacheImage(string originalPath, string fileName, string cacheFolder, ScaleMode scaleMode)
        {
            string sourcePath = Path.Combine(HttpContext.Current.Server.MapPath(originalPath), fileName);
            Bitmap image;
            using (FileStream stream = new FileStream(sourcePath, FileMode.Open))
            {
                image = new Bitmap(stream);
            }

            string cachePath = HttpContext.Current.Server.MapPath("~/ImageCache/" + cacheFolder);
            string cachedImagePath = Path.Combine(cachePath, fileName);

            using (FileStream stream = new FileStream(cachedImagePath, FileMode.CreateNew))
            {
                ScaleImage(cacheFolder, image, limitWidth[cacheFolder], limitHeight[cacheFolder], stream, scaleMode);
            }
        }

        public static string CachedImage(this HtmlHelper helper, string originalPath, string fileName, string cacheFolder, ScaleMode scaleMode)
        {
            StringBuilder sb = new StringBuilder();
            string formatString = "<img src=\"{0}\" alt=\"{1}\" />";
            sb.AppendFormat(formatString, GetCachedImage(originalPath, fileName, cacheFolder, scaleMode), fileName);
            return sb.ToString();
        }

        public static string OriginalImage(this HtmlHelper helper, string originalPath, string fileName)
        {
            StringBuilder sb = new StringBuilder();
            string formatString = "<img src=\"{0}\" alt=\"{1}\" />";

            sb.AppendFormat(formatString, Path.Combine(originalPath, fileName), fileName);
            return sb.ToString();
        }

        public static void SaveCachedImage(string originalPath, string fileName, string cacheFolder, ScaleMode scaleMode)
        {
            CacheImage(originalPath, fileName, cacheFolder, scaleMode);
        }


    }
}