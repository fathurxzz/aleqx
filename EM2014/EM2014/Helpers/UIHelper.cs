﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EM2014.Helpers
{
    public class MenuItem
    {
        public string Title { get; set; }
        public int ContentId { get; set; }
        public string ContentName { get; set; }
        public bool Selected { get; set; }
        public bool Current { get; set; }
        public int SortOrder { get; set; }
    }
}