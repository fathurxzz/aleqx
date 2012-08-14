﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Helpers
{
    public class HttpNotFoundException : HttpException
    {
        public HttpNotFoundException() : base(404, "") { }
        public HttpNotFoundException(string objectName) : base(404, objectName + " not found") { }
    }
}