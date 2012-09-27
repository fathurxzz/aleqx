using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EM2013.Models
{
    public class CatalogueViewModel:SiteViewModel
    {
        public CatalogueViewModel(SiteContext context, string category, string product)
            : base(context, category)
        {
            

        }
    }
}