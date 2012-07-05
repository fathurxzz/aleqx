using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rakurs.Models
{
    public class CatalogueViewModel : SiteViewModel
    {
        public CatalogueViewModel(StructureContainer context, string contentName, bool loadContent) : base(context, contentName, loadContent)
        {

        }
    }
}