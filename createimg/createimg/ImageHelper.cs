using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;

namespace createimg
{
    public enum Crop
    {
        Horizontal,
        Vertical,
        None
    }

    public class ImageHelper
    {
        private readonly Image _sourceImage;
        private double _coef;

        public ImageHelper(HttpPostedFileBase uploadFile)
        {
            _sourceImage = new Bitmap(uploadFile.InputStream);
        }

        public ImageHelper(Image sourceImage)
        {
            _sourceImage = sourceImage;
        }


        private static Image OvalImage(Image img)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddEllipse(0, 0, img.Width, img.Height);
                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    gr.SetClip(gp);
                    gr.DrawImage(img, rect, rect, GraphicsUnit.Pixel);
                }
            }
            return bmp;
        }

        public Image CreateOvalThumbnail(Size thumbnailSize)
        {
            Rectangle sourceRect = CalculateSourceRect(_sourceImage.Size, thumbnailSize);
            Rectangle destRect = new Rectangle(0, 0, thumbnailSize.Width, thumbnailSize.Height);
            Bitmap thumbnailImage = new Bitmap(destRect.Width, destRect.Height);
            Graphics graphics = Graphics.FromImage(thumbnailImage);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(_sourceImage, destRect, sourceRect, GraphicsUnit.Pixel);
            return OvalImage(thumbnailImage);
        }

        public Image CreateThumbnail(Size thumbnailSize)
        {
            Rectangle sourceRect = CalculateSourceRect(_sourceImage.Size, thumbnailSize);
            Rectangle destRect = new Rectangle(0, 0, thumbnailSize.Width, thumbnailSize.Height);
            Bitmap thumbnailImage = new Bitmap(destRect.Width, destRect.Height);
            Graphics graphics = Graphics.FromImage(thumbnailImage);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(_sourceImage, destRect, sourceRect, GraphicsUnit.Pixel);
            return thumbnailImage;
        }

        private Crop GetCrop(Size source, Size thumb)
        {
            double thumbCoef = _coef = (double)thumb.Height / thumb.Width;
            double sourceCoef = (double)source.Height / source.Width;
            if (thumbCoef.Equals(sourceCoef))
                return Crop.None;
            return thumbCoef > sourceCoef ? Crop.Horizontal : Crop.Vertical;
        }


        private Rectangle CalculateSourceRect(Size sourceImageSize, Size thumbImageSize)
        {
            var crop = GetCrop(sourceImageSize, thumbImageSize);
            switch (crop)
            {
                case Crop.None:
                    return new Rectangle(0, 0, sourceImageSize.Width, sourceImageSize.Height);
                case Crop.Horizontal:
                    int width = (int)Math.Truncate(sourceImageSize.Height / _coef);
                    int xPositionStart = (sourceImageSize.Width - width) / 2;
                    return new Rectangle(xPositionStart, 0, width, sourceImageSize.Height);
                case Crop.Vertical:
                    int height = (int)Math.Truncate(sourceImageSize.Width * _coef);
                    int yPositionStart = (sourceImageSize.Height - height) / 2;
                    return new Rectangle(0, yPositionStart, sourceImageSize.Width, height);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}