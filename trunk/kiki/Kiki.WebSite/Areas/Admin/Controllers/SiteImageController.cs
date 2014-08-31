using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiki.DataAccess;
using Kiki.DataAccess.Repositories;

namespace Kiki.WebSite.Areas.Admin.Controllers
{
    public class SiteImageController : AdminController
    {
        //
        // GET: /Admin/SiteImage/


        public SiteImageController(ISiteRepository repository) : base(repository)
        {

        }
    }
}
