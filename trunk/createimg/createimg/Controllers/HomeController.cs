using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace createimg.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }



        public ActionResult GetOvalImg()
        {
            string imageFile = System.Web.HttpContext.Current.Server.MapPath("~/Content/wolf.jpg");
            var srcImage = Image.FromFile(imageFile);

            ImageHelper helper = new ImageHelper(srcImage);



            Image image = helper.CreateOvalThumbnail(new Size(200, 200));

            //var destImg = ImageHelper.OvalImage(image);
            
            using (var streak = new MemoryStream())
            {
                image.Save(streak, ImageFormat.Png);
                return File(streak.ToArray(), "image/png");
            }
        }

        public ActionResult GetImg()
        {
            string imageFile = System.Web.HttpContext.Current.Server.MapPath("~/Content/wolf.jpg");
            var srcImage = Image.FromFile(imageFile);

            ImageHelper helper = new ImageHelper(srcImage);



            Image image = helper.CreateThumbnail(new Size(200, 200));

            //var destImg = ImageHelper.OvalImage(image);

            using (var streak = new MemoryStream())
            {
                image.Save(streak, ImageFormat.Png);
                return File(streak.ToArray(), "image/png");
            }
        }
    }



}
