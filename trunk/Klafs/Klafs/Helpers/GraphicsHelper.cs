using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Web.Mvc;
using System.Text;

namespace Dev.Mvc.Helpers
{
    public static class GraphicsHelper
    {
        public enum FixedDimension { Width, Height }

        private static Dictionary<string, int> maxDimensions = new Dictionary<string, int>();
        private static Dictionary<string, FixedDimension> fixDimension = new Dictionary<string, FixedDimension>();
        private static Dictionary<string, int> limitHeight = new Dictionary<string, int>();
        private static Dictionary<string, int> limitWidth = new Dictionary<string, int>();

        static GraphicsHelper()
        {
            maxDimensions.Add("thumbnail", 360);
            fixDimension.Add("thumbnail", FixedDimension.Width);
            limitHeight.Add("thumbnail", 240);
            limitWidth.Add("thumbnail", 360);
        }
        
        private static Rectangle CalcSourceRect(string name, Size image)
        {
            int height = limitHeight.ContainsKey(name) ? limitHeight[name] : 0;
            int width = limitWidth.ContainsKey(name) ? limitWidth[name] : 0;

            int destWidth = image.Width;
            int destHeight = image.Height;

            double ratio;

            if (height > width)
            {
                ratio = (double)height / (double)width;
                if (image.Width > image.Height)
                {
                    destWidth = (int)Math.Truncate(image.Height / ratio);
                }
                else
                {
                    destHeight = (int)Math.Truncate(image.Width / ratio);
                }
            }
            else
            {
                ratio = (double)width / (double)height;
                if (image.Height > image.Width)
                {
                    destHeight = (int)Math.Truncate(image.Width / ratio);
                }
                else
                {
                    destWidth = (int)Math.Truncate(image.Height / ratio);
                }
            }

            return new Rectangle(0, 0, destWidth, destHeight);
        }
        
        public static void ScaleImage1(string name, Bitmap image, int limWidth, int limHeight, Stream saveTo)
        {
            Rectangle sourceRect = CalcSourceRect(name, image.Size);
            Rectangle destRect = new Rectangle(0, 0, limWidth, limHeight);

            Bitmap thumbnailImage = new Bitmap(destRect.Width, destRect.Height);
            Graphics graphics = Graphics.FromImage(thumbnailImage);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(image, destRect, sourceRect, GraphicsUnit.Pixel);
            thumbnailImage.Save(saveTo, System.Drawing.Imaging.ImageFormat.Jpeg);
            saveTo.Position = 0;
        }

        public static string GetCachedImage(string originalPath, string fileName, string cacheFolder, bool forDesigners = false)
        {
            if (string.IsNullOrEmpty(fileName) ||
                !File.Exists(Path.Combine(HttpContext.Current.Server.MapPath(originalPath), fileName)))
            {
                fileName = "tripsWebMvcNoCarImage.jpg";
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
                    CacheImage(originalPath, fileName, cacheFolder, forDesigners);
                }
                catch
                {
                    return GetCachedImage(originalPath, "tripsWebMvcNoCarImage.jpg", cacheFolder);
                }
                return result;
            }
        }

        private static void CacheImage(string originalPath, string fileName, string cacheFolder, bool forDesigners)
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
                ScaleImage1(cacheFolder, image, limitWidth[cacheFolder],  limitHeight[cacheFolder], stream);
            }
        }

        public static string CachedImage(this HtmlHelper helper, string originalPath, string fileName, string cacheFolder, string alt, bool forDesigners = false)
        {
            StringBuilder sb = new StringBuilder();
            string formatString = "<img src=\"{0}\" alt=\"{1}\" />";

            sb.AppendFormat(formatString, GetCachedImage(originalPath, fileName, cacheFolder, forDesigners), alt);

            return sb.ToString();
        }
    }
}