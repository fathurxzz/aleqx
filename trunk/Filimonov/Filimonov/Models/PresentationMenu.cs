using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Filimonov.Models
{
    public class PresentationMenu:List<PresentationMenuItem>
    {
        
    }

    public class PresentationMenuItem
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public bool IsCurrent { get; set; }
    }
}