using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CollectStickers.Models;
using System.Collections;
using CollectStickers.Controllers;

namespace CollectStickers.Helpers
{
    public class Sticker
    {
        public static List<StickerPresentation> GetStickerList(Guid userId)
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
        public static List<StickerPresentation> GetStickerListNeed(Guid userId)
        {
            List<StickerPresentation> stickerList = new List<StickerPresentation>();
            using (StickersStorage context = new StickersStorage())
            {
                var stickersCollection = (from stikers in context.Stickers.Include("Album")
                                          where stikers.UserId == userId && stikers.NeedOrFree == 1
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

        public static List<StickerPresentation> GetStickerList()
        {
            Hashtable htuser = new Hashtable();

            using (MembershipStorage usercontext = new MembershipStorage())
            {
                var usersList = (from user in usercontext.aspnet_Users where user.UserName.ToLower() != "admin" select user).ToList();
                foreach (var item in usersList)
                {
                    htuser.Add(item.UserId, item.UserName);
                }
            }

            List<StickerPresentation> stickerList = new List<StickerPresentation>();
            using (StickersStorage context = new StickersStorage())
            {
                var stickersCollection = (from stikers in context.Stickers.Include("Album")
                                          where stikers.UserId != SystemSettings.CurrentUserId
                                          select new
                                          {
                                              number = stikers.Number,
                                              isNeed = stikers.NeedOrFree == 1 ? true : false,
                                              isFree = stikers.NeedOrFree == 2 ? true : false,
                                              userId = stikers.UserId
                                          }).ToList();

                foreach (var item in stickersCollection)
                {
                    StickerPresentation sticker = new StickerPresentation();
                    sticker.Number = item.number;
                    sticker.isNeed = item.isNeed;
                    sticker.isFree = item.isFree;
                    sticker.UserId = item.userId;
                    sticker.UserName = (string)htuser[item.userId];
                    stickerList.Add(sticker);
                }
            }
            stickerList.Sort(new CollectStickers.Helpers.Helpers.SortStickersByUser());
            return stickerList;
        }

        public static List<StickerPresentation> GetStickerListFree()
        {
            Hashtable htuser = new Hashtable();

            using (MembershipStorage usercontext = new MembershipStorage())
            {
                var usersList = (from user in usercontext.aspnet_Users where user.UserName.ToLower() != "admin" select user).ToList();
                foreach (var item in usersList)
                {
                    htuser.Add(item.UserId, item.UserName);
                }
            }

            List<StickerPresentation> stickerList = new List<StickerPresentation>();
            using (StickersStorage context = new StickersStorage())
            {
                var stickersCollection = (from stikers in context.Stickers.Include("Album")
                                          where stikers.UserId != SystemSettings.CurrentUserId && stikers.NeedOrFree == 2
                                          select new
                                          {
                                              number = stikers.Number,
                                              isNeed = stikers.NeedOrFree == 1 ? true : false,
                                              isFree = stikers.NeedOrFree == 2 ? true : false,
                                              userId = stikers.UserId
                                          }).ToList();

                foreach (var item in stickersCollection)
                {
                    StickerPresentation sticker = new StickerPresentation();
                    sticker.Number = item.number;
                    sticker.isNeed = item.isNeed;
                    sticker.isFree = item.isFree;
                    sticker.UserId = item.userId;
                    sticker.UserName = (string)htuser[item.userId];
                    stickerList.Add(sticker);
                }
            }
            stickerList.Sort(new CollectStickers.Helpers.Helpers.SortStickersByUser());
            return stickerList;
        }

    }

     
}
