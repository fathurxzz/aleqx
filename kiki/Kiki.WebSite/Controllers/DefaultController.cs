using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiki.DataAccess.Repositories;

namespace Kiki.WebSite.Controllers
{
    public class DefaultController : Controller
    {
        protected readonly ISiteRepository _repository;

        public DefaultController(ISiteRepository repository)
        {
            _repository = repository;
        }

    }
}
