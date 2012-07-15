﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Rakurs.Helpers
{
    public enum ScaleMode
    {
        Corp,
        Insert,
        Auto
    }

    public static class GraphicsHelper
    {
        private static Dictionary<string, int> limitHeight = new Dictionary<string, int>();
        private static Dictionary<string, int> limitWidth = new Dictionary<string, int>();

        //public static string[] ThumbnailFolders = { "thumbnail0", "thumbnail1" };
        //public static int[] ThumbnailDimentions = { 177, 200 };


        static GraphicsHelper()
        {
            //for (int i = 0; i < ThumbnailFolders.Length; i++)
            //{
            //    limitWidth.Add(ThumbnailFolders[i], ThumbnailDimentions[i]);
            //    limitHeight.Add(ThumbnailFolders[i], ThumbnailDimentions[i]);
            //}

            limitWidth.Add("galleryThumbnail", 157);
            limitHeight.Add("galleryThumbnail", 156);

            limitWidth.Add("mainFrameThumbnail", 1000);
            limitHeight.Add("mainFrameThumbnail", 514);

        }

        private static bool IsHorizontalImage(Size imageSise)
        {
            return imageSise.Width > imageSise.Height;
        }

        private static Rectangle CalculateDestRect(Size sourceImage, Size thumbImage, ScaleMode scaleMode)
        {

            int previewHeight = thumbImage.Height;
            int previewWidth = thumbImage.Width;

            int resultPreviewImageWidth = 0;
            int resultPreviewImageHeight = 0;
            switch (scaleMode)
            {
                case ScaleMode.Insert:

                    //int minPreviewImageLength = previewHeight > previewWidth
                    //    ? previewWidth
                    //    : previewHeight;
                    //int maxSourceImageLength = sourceImage.Width > sourceImage.Height
                    //                               ? sourceImage.Width
                    //                               : sourceImage.Height;

                    //double ratio = (double)minPreviewImageLength / (double)maxSourceImageLength;


                    double hRatio = (double)previewHeight / (double)sourceImage.Height;
                    double wRatio = (double)previewWidth / (double)sourceImage.Width;

                    double ratio = hRatio < wRatio ? hRatio : wRatio;

                    int resultSourceImageWidth = (int)(sourceImage.Width * ratio);
                    var resultSourceImageHeight = (int)(sourceImage.Height * ratio);



                    if (previewWidth > resultSourceImageWidth)
                    {
                        int offset = (previewWidth - resultSourceImageWidth) / 2;
                        return new Rectangle(offset, 0, resultSourceImageWidth, resultSourceImageHeight);
                    }
                    else if (previewHeight > resultSourceImageHeight)
                    {
                        var offset = (previewHeight - resultSourceImageHeight) / 2;
                        return new Rectangle(0, offset, resultSourceImageWidth, resultSourceImageHeight);
                    }

                    return new Rectangle(0, 0, resultPreviewImageWidth, resultPreviewImageHeight);



                default:
                    return new Rectangle(0, 0, thumbImage.Width, thumbImage.Height);
            }

        }

        private static Rectangle CalculateSourceRect(Size sourceImage, Size thumbImage, ScaleMode scaleMode)
        {
            int previewHeight = thumbImage.Height;
            int previewWidth = thumbImage.Width;

            int resultWidth;
            int resultHeight;

            switch (scaleMode)
            {
                case ScaleMode.Corp:
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
                default:
                    return new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
            }
        }


        private static void ScaleAndSaveOriginalImage(int limitLength, Bitmap image, Stream saveTo)
        {
            
            Rectangle sourceRect = new Rectangle(0,0,image.Width,image.Height);
            Rectangle destRect;
            int resultSourceImageWidth = image.Width;
            int resultSourceImageHeight = image.Height;
            if (image.Width <= limitLength && image.Height <= limitLength)
            {
                destRect = new Rectangle(0, 0, image.Width, image.Height);
            }
            else
            {
                double wRatio = (double)limitLength / (double)image.Width;
                double hRatio = (double)limitLength / (double)image.Height;
                double ratio = hRatio < wRatio ? hRatio : wRatio;
                resultSourceImageWidth = (int) (image.Width*ratio);
                resultSourceImageHeight = (int) (image.Height*ratio);
                destRect = new Rectangle(0, 0, resultSourceImageWidth, resultSourceImageHeight);
            }

            Bitmap thumbnailImage = new Bitmap(resultSourceImageWidth, resultSourceImageHeight);

            Graphics graphics = Graphics.FromImage(thumbnailImage);
            graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, resultSourceImageWidth, resultSourceImageHeight);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(image, destRect, sourceRect, GraphicsUnit.Pixel);

            thumbnailImage.Save(saveTo, System.Drawing.Imaging.ImageFormat.Jpeg);
            saveTo.Position = 0;
        }

        public static void ScaleImage(string name, Bitmap image, int limWidth, int limHeight, Stream saveTo, ScaleMode scaleMode)
        {
            var thumbImage = new Size(limitWidth.ContainsKey(name) ? limitWidth[name] : 0,
                                      limitHeight.ContainsKey(name) ? limitHeight[name] : 0);

            if (scaleMode == ScaleMode.Auto)
            {
                scaleMode = IsHorizontalImage(image.Size) ? ScaleMode.Corp : ScaleMode.Insert;
            }


            Rectangle sourceRect = CalculateSourceRect(image.Size, thumbImage, scaleMode);

            Rectangle destRect = CalculateDestRect(image.Size, thumbImage, scaleMode);

            Bitmap thumbnailImage = new Bitmap(limWidth, limWidth);

            Graphics graphics = Graphics.FromImage(thumbnailImage);
            graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, limWidth, limHeight);
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

        

        public static void SaveOriginalImage(string filePath, string fileName, HttpPostedFileBase file, int limitLength=1000)
        {

            string tmpFilePath = HttpContext.Current.Server.MapPath("~/Content/tmpImages");
            tmpFilePath = Path.Combine(tmpFilePath, fileName);
            file.SaveAs(tmpFilePath);

            Bitmap image;
            string sourcePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/tmpImages"), fileName);
            using (FileStream stream = new FileStream(sourcePath, FileMode.Open))
            {
                image = new Bitmap(stream);
            }
            using (FileStream stream = new FileStream(filePath, FileMode.CreateNew))
            {
                ScaleAndSaveOriginalImage(limitLength, image, stream);
            }

            IOHelper.DeleteFile("~/Content/tmpImages", fileName);

            //file.SaveAs(filePath);
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