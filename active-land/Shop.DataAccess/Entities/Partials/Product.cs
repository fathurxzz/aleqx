using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    partial class Product
    {
        public string ImageSource { get; set; }
        public bool IsInCart { get; set; }
        public bool IsSelectedByFilter { get; set; }
    }
}
