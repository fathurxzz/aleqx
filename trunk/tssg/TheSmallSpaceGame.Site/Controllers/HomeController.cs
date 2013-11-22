using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheSmallSpaceGame.DataAccess.Repositories;

namespace TheSmallSpaceGame.Site.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private readonly IUserRepository _repository;

        public HomeController(IUserRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var user = _repository.GetUser("alex");
            return View();
        }

    }
}
