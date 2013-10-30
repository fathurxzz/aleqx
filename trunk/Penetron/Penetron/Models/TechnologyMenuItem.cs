using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Penetron.Models
{
    public class TechnologyMenuItem
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }
        public bool Current { get; set; }
    }
}