using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Dev.Mvc.Helpers
{
    public class Watermark
    {

        //this fuction takes an Image and String for watermarking as argument
        //and returns an Image with watermark

        public Bitmap WaterMarkToImage(Bitmap bmp, string watermark)
        {
            //Bitmap bmp;
            //bmp = new Bitmap(ImagePath);

            Graphics graphicsObject;
            int x, y;
            try
            {
                //create graphics object from bitmap
                graphicsObject = Graphics.FromImage(bmp);
            }
            catch (Exception e)
            {

                Bitmap bmpNew = new Bitmap(bmp.Width, bmp.Height);
                graphicsObject = Graphics.FromImage(bmpNew);

                graphicsObject.DrawImage(bmp, new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel);

                bmp = bmpNew;
            }

            //int startsize = (bmp.Width / watermark.Length);//get the font size with respect to length of the string

            int startsize = 8;

            //x and y cordinates to draw a string
            x = bmp.Width - 180;
            y = bmp.Height - 15;

            //System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat(StringFormatFlags.DirectionVertical); -> draws a vertical string for watermark

            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat(StringFormatFlags.NoWrap);

            //drawing string on Image
            graphicsObject.DrawString(watermark, new Font("MS Sans Serif", startsize, FontStyle.Regular), new SolidBrush(Color.FromArgb(120, 255, 255, 255)), x, y, drawFormat);

            //return a water marked image
            return (bmp);
        }



    }
}