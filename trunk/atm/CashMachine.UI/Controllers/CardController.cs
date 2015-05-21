using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CashMachine.UI.Controllers
{
    [Authorize]
    public class CardController : Controller
    {
        //
        // GET: /Card/
        //[AllowAnonymous]
        public ActionResult Index(string cardNumber)
        {
            return View();
        }
    }
}
