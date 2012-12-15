using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SiteExtensions.Graphics
{
    //public class ThunmnailPictures : Dictionary<string, PictureDimensions>
    //{

    //}

    //public enum Orientation
    //{
    //    Horizontal,
    //    Vertical
    //}

    /// <summary>
    /// Метод масштабирования
    /// </summary>
    public enum ScaleMode
    {
        /// <summary>
        /// Фиксированная ширина, произвольная высота
        /// </summary>
        FixedWidth,

        /// <summary>
        /// Фиксированная высота, произвольная ширина
        /// </summary>
        FixedHeight,

        /// <summary>
        /// Обрезка лишнего
        /// </summary>
        Crop,

        /// <summary>
        /// Вписать изображение в превью полностью
        /// </summary>
        Insert,

        /// <summary>
        /// Автоматический, либо обрезка лишнего либо вписывание изображения в превью полностью
        /// </summary>
        Auto
    }

    //public enum FixedDimension
    //{

    //}

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


        private static void ScaleAndSaveOriginalImage(int limitLength, Bitmap image, Stream saveTo)
        {

            Rectangle sourceRect = new Rectangle(0, 0, image.Width, image.Height);
            Rectangle destRect;
            int resultSourceImageWidth = image.Width;
            int resultSourceImageHeight = image.Height;
            if (image.Width <= limitLength && image.Height <= limitLength)
            {
                destRect = new Rectangle(0, 0, image.Width, image.Height);
            }
            else
            {
                double wRatio = (double)limitLength / image.Width;
                double hRatio = (double)limitLength / image.Height;
                double ratio = hRatio < wRatio ? hRatio : wRatio;
                resultSourceImageWidth = (int)(image.Width * ratio);
                resultSourceImageHeight = (int)(image.Height * ratio);
                destRect = new Rectangle(0, 0, resultSourceImageWidth, resultSourceImageHeight);
            }

            Bitmap thumbnailImage = new Bitmap(resultSourceImageWidth, resultSourceImageHeight);

            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(thumbnailImage);
            graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, resultSourceImageWidth, resultSourceImageHeight);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(image, destRect, sourceRect, GraphicsUnit.Pixel);

            thumbnailImage.Save(saveTo, System.Drawing.Imaging.ImageFormat.Jpeg);
            saveTo.Position = 0;
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
                string backgroundImageSourcePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Images/bg"), "bg.jpg");
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
            using (var bmp = new Bitmap(HttpContext.Current.Server.MapPath(path)))
            {
                _width = bmp.Width;
                _height = bmp.Height;
            }
        }

        public static string GetCachedImage(string originalPath, string fileName, ThumbnailPicture thumbnail)
        {
            if (string.IsNullOrEmpty(fileName) || !File.Exists(Path.Combine(HttpContext.Current.Server.MapPath(originalPath), fileName)))
            {
                return null;
            }
            string cacheFolder = thumbnail.CacheFolder;


            string result = Path.Combine("/ImageCache/" + cacheFolder + "/", fileName);
            string cachePath = HttpContext.Current.Server.MapPath("~/ImageCache/" + cacheFolder);

            if (!Directory.Exists(cachePath)) Directory.CreateDirectory(cachePath);

            string cachedImagePath = Path.Combine(cachePath, fileName);
            if (File.Exists(cachedImagePath))
            {
                GetImageSize(result);
                return result;
            }

            if (CacheImage(originalPath, fileName, cacheFolder, thumbnail.PictureSize, thumbnail.ScaleMode, thumbnail.UseBackgroundImage, thumbnail.Offset))
            {
                GetImageSize(result);
                return result;
            }

            return null;
        }

        private static bool CacheImage(string originalPath, string fileName, string cacheFolder, PictureSize thumbnailImageSize, ScaleMode scaleMode, bool useBgImage, int delta)
        {
            string sourcePath = Path.Combine(HttpContext.Current.Server.MapPath(originalPath), fileName);
            Bitmap image;
            using (FileStream stream = new FileStream(sourcePath, FileMode.Open))
            {
                image = new Bitmap(stream);
            }

            string cachePath = HttpContext.Current.Server.MapPath("~/ImageCache/" + cacheFolder);
            if (!Directory.Exists(cachePath)) Directory.CreateDirectory(cachePath);
            string cachedImagePath = Path.Combine(cachePath, fileName);

            using (FileStream stream = new FileStream(cachedImagePath, FileMode.CreateNew))
            {
                return ScaleImage(cacheFolder, image, new Size(thumbnailImageSize.Width, thumbnailImageSize.Height), stream, scaleMode, useBgImage, delta);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="file"></param>
        /// <param name="limitLength"></param>
        public static void SaveOriginalImage(string filePath, string fileName, HttpPostedFileBase file, int limitLength = 1000)
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
        }

        public static string CachedImage(this HtmlHelper helper, string originalPath, string fileName, ThumbnailPicture thumbnail)
        {
            return CachedImage(helper, originalPath, fileName, thumbnail, null);
        }

        public static string CachedImage(this HtmlHelper helper, string originalPath, string fileName, ThumbnailPicture thumbnail, string className)
        {
            StringBuilder sb = new StringBuilder();
            string formatString = "<img src=\"{0}\" alt=\"{1}\" class=\"{2}\" width=\"{3}\" height=\"{4}\" />";
            //string formatString = "<img src=\"{0}\" alt=\"{1}\" class=\"{2}\" />";
            sb.AppendFormat(formatString, GetCachedImage(originalPath, fileName, thumbnail), fileName, className, _width, _height);
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="originalPath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string OriginalImage(this HtmlHelper helper, string originalPath, string fileName)
        {
            StringBuilder sb = new StringBuilder();
            string formatString = "<img src=\"{0}\" alt=\"{1}\" />";

            sb.AppendFormat(formatString, Path.Combine(originalPath, fileName), fileName);
            return sb.ToString();
        }

        public static string OriginalImageWithDim(this HtmlHelper helper, string originalPath, string fileName, string className, string id)
        {
            StringBuilder sb = new StringBuilder();
            string formatString = "<img src=\"{0}\" alt=\"{1}\" width=\"{2}\" height=\"{3}\" class=\"{4}\" id=\"{5}\" />";

            string result = Path.Combine(originalPath, fileName);
            GetImageSize(result);

            sb.AppendFormat(formatString, Path.Combine(originalPath, fileName), fileName, _width, _height, className, id);
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalPath"></param>
        /// <param name="fileName"></param>
        /// <param name="thumbnail"></param>
        /// <param name="scaleMode"></param>
        public static void SaveCachedImage(string originalPath, string fileName, ThumbnailPicture thumbnail, ScaleMode scaleMode)
        {
            CacheImage(originalPath, fileName, thumbnail.CacheFolder, thumbnail.PictureSize, scaleMode, false, 0);
        }


    }
}
