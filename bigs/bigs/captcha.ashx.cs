﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bigs
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class captcha :CaptchaImageHandler, IHttpHandler
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
