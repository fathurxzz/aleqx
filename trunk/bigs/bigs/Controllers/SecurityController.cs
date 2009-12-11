using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Drawing;
using System.Drawing.Imaging;

namespace bigs.Controllers
{
    public class SecurityController : Controller
    {
        //
        // GET: /Security/

        public void Captcha(string guid)
        {
            HttpContextBase context = HttpContext;
            // get the unique GUID of the captcha; this must be passed in via the querystring
            guid = context.Request.QueryString["guid"];
            CaptchaImage ci = CaptchaImage.GetCachedCaptcha(guid);

            if (String.IsNullOrEmpty(guid) || ci == null)
            {
                context.Response.StatusCode = 404;
                context.Response.StatusDescription = "Not Found";
                context.ApplicationInstance.CompleteRequest();
                return;
            }

            // write the image to the HTTP output stream as an array of bytes
            using (Bitmap b = ci.RenderImage())
            {
                b.Save(context.Response.OutputStream, ImageFormat.Gif);
            }

            context.Response.ContentType = "image/png";
            context.Response.StatusCode = 200;
            context.Response.StatusDescription = "OK";
            context.ApplicationInstance.CompleteRequest();
        }

    }
}
