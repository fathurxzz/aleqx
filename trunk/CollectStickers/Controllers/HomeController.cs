using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollectStickers.Models;
using System.Web.Security;

namespace CollectStickers.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult UserPage()
        {
            List<StickerPresentation> stickerList = new List<StickerPresentation>();

            using (StickersStorage context = new StickersStorage())
            {

                var stickersCollection = (from stikers in context.Stickers.Include("Album")
                                          where stikers.UserId == SystemSettings.CurrentUserId
                                          select new
                                              {
                                                  number = stikers.Number,
                                                  isNeed = stikers.NeedOrFree == 1 ? true : false,
                                                  isFree = stikers.NeedOrFree == 2 ? true : false
                                              }).ToList();



                
                foreach (var item in stickersCollection)
                {
                    StickerPresentation sticker = new StickerPresentation();
                    sticker.Number = item.number;
                    sticker.isNeed = item.isNeed;
                    sticker.isFree = item.isFree;
                    stickerList.Add(sticker);
                }
            }

            return View();
        }

        public ActionResult Users()
        {
            List<UserPresentation> users = null;
            using (MembershipStorage context = new MembershipStorage())
            {
                users = context.GetAllUsers();
            }
            return View(users);
        }
    }
}
