using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public partial class ProductAttributeValues
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is ProductAttributeValues))
                return false;
            return ((ProductAttributeValues) obj).Id == Id;
        }
    }
}