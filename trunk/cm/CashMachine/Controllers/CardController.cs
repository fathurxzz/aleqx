using System;
using System.Web.Mvc;
using CashMachine.Helpers;

namespace CashMachine.Controllers
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
