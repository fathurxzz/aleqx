﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Filimonov.Models
{
    public class ContentItem
    {
        public ContentType ContentType { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}