﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kulumu.Models
{
    public class CaruselFrame
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public string CategoryName { get; set; }
    }
}