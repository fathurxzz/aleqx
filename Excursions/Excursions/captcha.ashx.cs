using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dev.Helpers;

namespace Excursions
{
    /// <summary>
    /// Summary description for Captcha
    /// </summary>
    public class Captcha : CaptchaImageHandler, IHttpHandler
    {
        /*
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }*/
    }
}