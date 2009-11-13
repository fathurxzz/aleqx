using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollectStickers.Models;
using System.Web.Security;
using System.Web.Script.Serialization;
using System.Collections;

namespace CollectStickers.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Сервис по обмену наклейками!";

            return View();
        }



        public ActionResult EditStickerInfo()
        {
            int needStickerCount = 0;
            List<StickerPresentation> stickerList = GetStickerList((Guid)SystemSettings.CurrentUserId);
            foreach (var item in stickerList)
            {
                if (item.isNeed)
                    needStickerCount++;
            }

            ViewData["collected"] = SystemSettings.CurrentAlbumStickerCount - needStickerCount;
            ViewData["collectedPercent"] = Math.Round((decimal)(SystemSettings.CurrentAlbumStickerCount - needStickerCount) / SystemSettings.CurrentAlbumStickerCount * 100, 2);
            ViewData["needed"] = needStickerCount;
            return View(stickerList);
        }

        public ActionResult StickersSummary()
        {
            return View(GetStickerList((Guid)SystemSettings.CurrentUserId));
        }

        private List<StickerPresentation> GetStickerList(Guid guid)
        {
            return Helpers.Sticker.GetStickerList(guid);
        }

        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateStickers(FormCollection form)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (!string.IsNullOrEmpty(form["enablities"]))
            {
                Dictionary<string, string> enables = serializer.Deserialize<Dictionary<string, string>>(form["enablities"]);
                using (StickersStorage context = new StickersStorage())
                {
                    Album album = (from a in context.Album where a.Id == 1 select a).FirstOrDefault();
                    if (album == null)
                        return RedirectToAction("EditStickerInfo");

                    foreach (string key in enables.Keys)
                    {
                        int value = int.Parse(enables[key]);
                        int number = int.Parse(key);
                        Stickers sticker = (from s in context.Stickers where s.Number == number && s.UserId == SystemSettings.CurrentUserId select s).FirstOrDefault();
                        if (sticker != null)
                        {
                            if (value == 0)
                                context.DeleteObject(sticker);
                            else
                            {
                                sticker.NeedOrFree = Convert.ToByte(value);
                                context.SaveChanges(true);
                            }
                        }
                        else if (sticker == null)
                        {
                            sticker = new Stickers();
                            sticker.NeedOrFree = Convert.ToByte(value);
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


        public ActionResult Coincidences()
        {
            List<StickerPresentation> myList = GetStickerListNeed((Guid)SystemSettings.CurrentUserId);
            List<StickerPresentation> strangeList = GetStickerListFree();
            List<StickerPresentation> newList = new List<StickerPresentation>();

            foreach (var strangeItem in strangeList)
            {
                foreach (var myItem in myList)
                {
                    if (myItem.Number == strangeItem.Number)
                        newList.Add(strangeItem);
                }
            }
            return View(newList);
        }

        private List<StickerPresentation> GetStickerListFree()
        {
            return Helpers.Sticker.GetStickerListFree();
        }

        private List<StickerPresentation> GetStickerListNeed(Guid guid)
        {
            return Helpers.Sticker.GetStickerListNeed(guid);
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
