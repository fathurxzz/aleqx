﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Kefirchik.Graphics
{
    public static class GraphicsHelper
    {

        #region privateMethods

        private static Dictionary<string, int> limitHeight = new Dictionary<string, int>();
        private static Dictionary<string, int> limitWidth = new Dictionary<string, int>();


        static GraphicsHelper()
        {
            //limitWidth.Add("thumb1", 150);
            //limitHeight.Add("thumb1", 100);
        }


        private static Rectangle CalculateDestRect(int limWidth, int limHeight, Size imageSize)
        {
            /*
            // вписывание в заданные параметры
            int previewHeight = LimitHeight[name];
            int previewWidth = LimitWidth[name];

            int resultWidth;
            int resultHeight;

            if (sourceImage.Width > sourceImage.Height)
            {
                double coef = (double) sourceImage.Width/(double) previewWidth;

                resultHeight = (int) Math.Truncate(sourceImage.Height/coef);

                resultWidth = previewWidth;
            }
            else
            {
                double coef = (double)sourceImage.Height / (double)previewHeight;

                resultWidth = (int)Math.Truncate(sourceImage.Width / coef);

                resultHeight = previewHeight;
            }

            

            
            return new Rectangle(0, 0, resultWidth, resultHeight);
              
            */

            //return new Rectangle(0, 0, LimitWidth[name], LimitHeight[name]);

            return imageSize.Width < imageSize.Height ? new Rectangle(0, 0, limHeight, limWidth) : new Rectangle(0, 0, limWidth, limHeight);


        }

        private static Rectangle CalculateSourceRect(string name, Size sourceImage)
        {
            int previewHeight;
            int previewWidth;

            if (sourceImage.Width > sourceImage.Height)
            {
                previewHeight = limitHeight.ContainsKey(name) ? limitHeight[name] : 0;
                previewWidth = limitWidth.ContainsKey(name) ? limitWidth[name] : 0;
            }
            else
            {
                previewWidth = limitHeight.ContainsKey(name) ? limitHeight[name] : 0;
                previewHeight = limitWidth.ContainsKey(name) ? limitWidth[name] : 0;
            }

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

        private static void ScaleImage(string name, Bitmap image, int limWidth, int limHeight, Stream saveTo)
        {
            Rectangle sourceRect = CalculateSourceRect(name, image.Size);
            Rectangle destRect = CalculateDestRect(limWidth, limHeight, image.Size);
            Bitmap thumbnailImage = new Bitmap(destRect.Width, destRect.Height);
            var graphics = System.Drawing.Graphics.FromImage(thumbnailImage);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(image, destRect, sourceRect, GraphicsUnit.Pixel);
            thumbnailImage.Save(saveTo, System.Drawing.Imaging.ImageFormat.Jpeg);
            saveTo.Position = 0;
        }

        private static string GetCachedImage(string originalPath, string fileName, string cacheFolder)
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

            //if(!Directory.Exists(HttpContext.Current.Server.MapPath("~/ImageCache")))
            //{

            //}

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
                    CacheImage(originalPath, fileName, cacheFolder);
                }
                catch
                {
                    return GetCachedImage(originalPath, "nophoto.gif", cacheFolder);
                }
                return result;
            }
        }

        private static void CacheImage(string originalPath, string fileName, string cacheFolder)
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
                ScaleImage(cacheFolder, image, limitWidth[cacheFolder], limitHeight[cacheFolder], stream);
            }
        }

        public static string CachedImage(this HtmlHelper helper, string originalPath, string fileName, string cacheFolder, string alt)
        {
            StringBuilder sb = new StringBuilder();
            string formatString = "<img src=\"{0}\" alt=\"{1}\" />";

            sb.AppendFormat(formatString, GetCachedImage(originalPath, fileName, cacheFolder), alt);

            return sb.ToString();
        }

        public static string CachedImage(this HtmlHelper helper, ThumbnailParameters thumbnailparams, string originalPath, string fileName, string cacheFolder, string alt)
        {
            StringBuilder sb = new StringBuilder();
            string formatString = "<img src=\"{0}\" alt=\"{1}\" />";
            
            



           
            /*
            limitHeight.Add(cacheFolder,thumbnailDimensions.Height);
            limitWidth.Add(cacheFolder,thumbnailDimensions.Width);
            */



            sb.AppendFormat(formatString, GetCachedImage(originalPath, fileName, cacheFolder), alt);

            return sb.ToString();
        }
        
        #endregion

        /*private static void SaveCachedImage(string originalPath, string fileName, string cacheFolder)
        {
            CacheImage(originalPath, fileName, cacheFolder);
        }*/
    }
}
