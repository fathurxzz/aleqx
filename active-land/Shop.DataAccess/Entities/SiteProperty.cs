using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    public partial class SiteProperty
    {
        public SiteProperty()
        {
            
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
