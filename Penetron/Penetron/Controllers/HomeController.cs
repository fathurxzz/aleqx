using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Penetron.Models;

namespace Penetron.Controllers
{
    public class HomeController : Controller
    {
        IMessageService _messageService;
        SiteContext _context;

        public HomeController(IMessageService messageService, SiteContext context)
        {
            _messageService = messageService;
            _context = context;
        }

        public ActionResult Index()
        {
            var cat = _context.Category.ToList();
            ViewBag.Message = _messageService.GetWelcomeMessage();
            ViewBag.Message2 = cat.Count().ToString();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
