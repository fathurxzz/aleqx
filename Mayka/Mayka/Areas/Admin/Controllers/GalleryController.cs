using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mayka.Models;

namespace Mayka.Areas.Admin.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        private readonly SiteContext _context;

        public GalleryController(SiteContext context)
        {
            _context = context;
        }

      

    }
}
