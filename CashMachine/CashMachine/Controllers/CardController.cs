using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CashMachine.Models;

namespace CashMachine.Controllers
{
    [Authorize]
    public class CardController : Controller
    {
        //
        // GET: /Card/

        private CardStorage _storage;

        public CardController(SiteContext context)
        {
            _storage = new CardStorage(context);
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Operations()
        {
            return View();
        }

        public ActionResult Balance()
        {

            return View();
        }

        public ActionResult GetCash()
        {
            return View();
        }

    }
}
