using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Penetron.Models
{
    public class SiteMenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }
        public bool Current { get; set; }
        public bool Parent { get; set; }
    }
    
}
