using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    public partial class MainPageBanner
    {
        public MainPageBanner()
        {
            
        }

        public int Id { get; set; }
        public string ImageSource { get; set; }
    }
}
