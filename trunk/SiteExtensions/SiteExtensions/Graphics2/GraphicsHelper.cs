using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SiteExtensions.Graphics;

namespace SiteExtensions.Graphics2
{
    /// <summary>
    /// Графический хелпер
    /// </summary>
    public static class GraphicsHelper
    {

        private static int _width;
        private static int _height;

        private static bool IsHorizontalImage(Size imageSise)
        {
            return imageSise.Width > imageSise.Height;
        }




        private static Rectangle CalculateDestRect(Size sourceImage, Size thumbImage, ScaleMode scaleMode, int delta)
        {
            double hvr = (double)sourceImage.Width / (double)sourceImage.Height;
            
            switch (scaleMode)
            {
                case ScaleMode.Insert:
                    thumbImage.Width -= delta*2;
                    thumbImage.Height -= delta*2;

                    double hRatio = (double)thumbImage.Height / sourceImage.Height;
                    double wRatio = (double)thumbImage.Width / sourceImage.Width;
                    double ratio = hRatio < wRatio ? hRatio : wRatio;
                    int resultSourceImageWidth = (int)(sourceImage.Width * ratio);
                    var resultSourceImageHeight = (int)(sourceImage.Height * ratio);
                    if (thumbImage.Width > resultSourceImageWidth)
                    {
                        int offset = ((thumbImage.Width + delta*2) - resultSourceImageWidth) / 2;
                        return new Rectangle(offset, 0 + delta, resultSourceImageWidth, resultSourceImageHeight);
                    }
                    if (thumbImage.Height > resultSourceImageHeight)
                    {
                        var offset = ((thumbImage.Height + delta*2) - resultSourceImageHeight) / 2;
                        return new Rectangle(0 + delta, offset, resultSourceImageWidth, resultSourceImageHeight);
                    }
                    break;

                case ScaleMode.FixedWidth:
                    int destImageHeight = (int)(thumbImage.Width / hvr);
                    return new Rectangle(0, 0, thumbImage.Width, destImageHeight);

                case ScaleMode.FixedHeight:
                    int destImageWidth = (int)(thumbImage.Height * hvr);
                    return new Rectangle(0, 0, destImageWidth, thumbImage.Height);

            }
            return new Rectangle(0, 0, thumbImage.Width, thumbImage.Height);
        }

        private static Rectangle CalculateSourceRect(Size sourceImage, Size thumbImage, ScaleMode scaleMode)
        {
            int previewHeight = thumbImage.Height;
            int previewWidth = thumbImage.Width;

            switch (scaleMode)
            {
                case ScaleMode.Crop:
                    double wRatio = (double)sourceImage.Width / previewWidth;
                    double hRatio = (double)sourceImage.Height / previewHeight;
                    double coef = (double)previewHeight / previewWidth;
                    int resultWidth;
                    int resultHeight;
                    if (wRatio < hRatio)
                    {
                        resultWidth = sourceImage.Width;
                        resultHeight = (int)Math.Truncate(sourceImage.Width * coef);
                        return new Rectangle(0, (sourceImage.Height - resultHeight) / 2, resultWidth, resultHeight);
                    }
                    else
                    {
                        resultHeight = sourceImage.Height;
                        resultWidth = (int)Math.Truncate(sourceImage.Height / coef);
                        return new Rectangle((sourceImage.Width - resultWidth) / 2, 0, resultWidth, resultHeight);
                    }

                case ScaleMode.Insert:
                    return new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);

                default:
                    return new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
            }
        }


      

        public static bool ScaleImage(string name, Bitmap image, Size thumbImage, Stream saveTo, ScaleMode scaleMode, bool useBgImage, int delta)
        {
            if (scaleMode == ScaleMode.Auto)
            {
                scaleMode = IsHorizontalImage(image.Size) ? ScaleMode.Crop : ScaleMode.Insert;
            }

            Rectangle sourceRect = CalculateSourceRect(image.Size, thumbImage, scaleMode);

            Rectangle destRect = CalculateDestRect(image.Size, thumbImage, scaleMode, delta);

            Bitmap thumbnailImage;

            if (useBgImage)
            {
                string backgroundImageSourcePath = Path.Combine(HttpRuntime.AppDomainAppPath,"Content","Images","bg", "bg.jpg");
                using (FileStream stream = new FileStream(backgroundImageSourcePath, FileMode.Open))
                {
                    thumbnailImage = new Bitmap(stream);
                    System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(thumbnailImage);

                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphics.DrawImage(image, destRect, sourceRect, GraphicsUnit.Pixel);

                    thumbnailImage.Save(saveTo, System.Drawing.Imaging.ImageFormat.Jpeg);
                    saveTo.Position = 0;
                }
            }
            else
            {
                if (scaleMode == ScaleMode.Insert)
                    thumbnailImage = new Bitmap(thumbImage.Width, thumbImage.Height);
                else
                    thumbnailImage = new Bitmap(destRect.Width, destRect.Height);

                System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(thumbnailImage);
                if (scaleMode == ScaleMode.Insert)
                    graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, thumbImage.Width, thumbImage.Height);
                else
                    graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, destRect.Width, destRect.Height);
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, destRect, sourceRect, GraphicsUnit.Pixel);

                thumbnailImage.Save(saveTo, System.Drawing.Imaging.ImageFormat.Jpeg);
                saveTo.Position = 0;
            }
            return true;
        }


        private static void GetImageSize(string path)
        {
            using (var bmp = new Bitmap(path))
            {
                _width = bmp.Width;
                _height = bmp.Height;
            }
        }

        public static string GetCachedImage(string fileName, ThumbnailPicture thumbnail)
        {
            

            if (string.IsNullOrEmpty(fileName) || !File.Exists(Path.Combine(HttpRuntime.AppDomainAppPath,"Content","Images", fileName)))
            {
                return null;
            }
            string cacheFolder = thumbnail.CacheFolder;


            string result = Path.Combine(HttpRuntime.AppDomainAppPath, "ImageCache" , cacheFolder , fileName);

            string cachePath = Path.Combine(HttpRuntime.AppDomainAppPath, "ImageCache" , cacheFolder);

            if (!Directory.Exists(cachePath)) Directory.CreateDirectory(cachePath);

            string cachedImagePath = Path.Combine(cachePath, fileName);
            if (File.Exists(cachedImagePath))
            {
                GetImageSize(result);
                return "http://listelli.ua/ImageCache/"+ cacheFolder+"/"+ fileName;
                return result;
            }

            if (CacheImage(fileName, cacheFolder, thumbnail.PictureSize, thumbnail.ScaleMode, thumbnail.UseBackgroundImage, thumbnail.Offset))
            {
                GetImageSize(result);
                return "http://listelli.ua/ImageCache/" + cacheFolder + "/" + fileName;
                return result;
            }

            return null;
        }

        private static bool CacheImage(string fileName, string cacheFolder, PictureSize thumbnailImageSize, ScaleMode scaleMode, bool useBgImage, int delta)
        {
            string sourcePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content", "Images", fileName);
            Bitmap image;
            using (FileStream stream = new FileStream(sourcePath, FileMode.Open))
            {
                image = new Bitmap(stream);
            }

            string cachePath = Path.Combine(HttpRuntime.AppDomainAppPath, "ImageCache", cacheFolder);
            if (!Directory.Exists(cachePath)) Directory.CreateDirectory(cachePath);
            string cachedImagePath = Path.Combine(cachePath, fileName);

            using (FileStream stream = new FileStream(cachedImagePath, FileMode.CreateNew))
            {
                return ScaleImage(cacheFolder, image, new Size(thumbnailImageSize.Width, thumbnailImageSize.Height), stream, scaleMode, useBgImage, delta);
            }
        }



        public static string CachedImage(string fileName, ThumbnailPicture thumbnail)
        {
            StringBuilder sb = new StringBuilder();
            string formatString = "<img src=\"{0}\" alt=\"{1}\" width=\"{2}\" height=\"{3}\" />";
            string imageSrc = GetCachedImage(fileName, thumbnail);
            sb.AppendFormat(formatString, imageSrc, imageSrc, _width, _height);
            return sb.ToString();
        }

       
    }
}
