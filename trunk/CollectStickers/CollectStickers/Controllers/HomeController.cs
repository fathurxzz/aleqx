using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollectStickers.Models;
using System.Web.Security;
using System.Web.Script.Serialization;

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

        public ActionResult EditStickerInfo()
        {
            return View(GetStickerList((Guid)SystemSettings.CurrentUserId));
        }

        public ActionResult StickersSummary()
        {
            return View(GetStickerList((Guid)SystemSettings.CurrentUserId));
        }

        private List<StickerPresentation> GetStickerList(Guid userId)
        {
            List<StickerPresentation> stickerList = new List<StickerPresentation>();
            using (StickersStorage context = new StickersStorage())
            {
                var stickersCollection = (from stikers in context.Stickers.Include("Album")
                                          where stikers.UserId == userId
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
            stickerList.Sort(new CollectStickers.Helpers.Helpers.SortStickers());
            return stickerList; 
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateStickers(FormCollection form)
        {

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            



            if (!string.IsNullOrEmpty(form["enablitiesNeed"]))
            {
                Dictionary<string, string> enables = serializer.Deserialize<Dictionary<string, string>>(form["enablitiesNeed"]);
                using (StickersStorage context = new StickersStorage())
                {
                    Album album = (from a in context.Album where a.Id == 1 select a).FirstOrDefault();
                    if (album == null)
                        return RedirectToAction("EditStickerInfo");


                    foreach (string key in enables.Keys)
                    {
                        bool isEnabled = bool.Parse(enables[key]);
                        int number = int.Parse(key);
                        Stickers sticker = (from s in context.Stickers where s.Number == number && s.UserId == SystemSettings.CurrentUserId select s).FirstOrDefault();
                        if (sticker != null && !isEnabled)
                        {
                            context.DeleteObject(sticker);
                        }
                        else if (sticker == null && isEnabled)
                        {
                            sticker = new Stickers();
                            sticker.NeedOrFree = 1;
                            sticker.Number = (short)number;
                            sticker.UserId = (Guid)SystemSettings.CurrentUserId;
                            sticker.Album = album;
                            context.AddToStickers(sticker);
                        }
                    }
                    context.SaveChanges(true);
                }
 
            }

            if (!string.IsNullOrEmpty(form["enablitiesFree"]))
            {
                Dictionary<string, string> enables = serializer.Deserialize<Dictionary<string, string>>(form["enablitiesFree"]);
                using (StickersStorage context = new StickersStorage())
                {
                    Album album = (from a in context.Album where a.Id == 1 select a).FirstOrDefault();
                    if (album == null)
                        return RedirectToAction("EditStickerInfo");


                    foreach (string key in enables.Keys)
                    {
                        bool isEnabled = bool.Parse(enables[key]);
                        int number = int.Parse(key);
                        Stickers sticker = (from s in context.Stickers where s.Number == number && s.UserId == SystemSettings.CurrentUserId select s).FirstOrDefault();
                        if (sticker != null && !isEnabled)
                        {
                            context.DeleteObject(sticker);
                        }
                        else if (sticker == null && isEnabled)
                        {
                            sticker = new Stickers();
                            sticker.NeedOrFree = 2;
                            sticker.Number = (short)number;
                            sticker.UserId = (Guid)SystemSettings.CurrentUserId;
                            sticker.Album = album;
                            context.AddToStickers(sticker);
                        }
                    }
                    context.SaveChanges(true);
                }

            }

            return RedirectToAction("EditStickerInfo");
        }


        public ActionResult Compatibility()
        {

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
