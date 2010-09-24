﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excursions.Models;

namespace Excursions.Areas.Admin.Controllers
{
    public class ExcursionsController : Controller
    {
        //
        // GET: /Admin/Excursions/

        [HttpGet]
        public ActionResult AddEdit(int? id)
        {
            Excursion excursion = null;
            if (id.HasValue)
            {
                using (var context = new ContentStorage())
                {
                    excursion = context.Excursion.Include("Comments").Select(c => c).Where(c => c.Id == id).First();
                }
            }
            return View(excursion);
        }

        [HttpPost]
        public ActionResult AddEdit(Excursion excursion, int? Id)
        {
            using (var context = new ContentStorage())
            {

                if (Id.HasValue && Id.Value > 0)
                {
                    excursion.Id = Id.Value;
                    object originalItem;
                    EntityKey entityKey = new EntityKey("ContentStorage.Excursions", "Id", excursion.Id);
                    if (context.TryGetObjectByKey(entityKey, out originalItem))
                    {
                        context.ApplyPropertyChanges(entityKey.EntitySetName, excursion);
                    }
                }
                else
                {
                    context.AddToExcursion(excursion);
                }
                context.SaveChanges();
            }
// ReSharper disable Asp.NotResolved
            return RedirectToAction("Index", "Excursions", new { area = "" });
// ReSharper restore Asp.NotResolved
        }


        public ActionResult Delete(int id)
        {

// ReSharper disable Asp.NotResolved
            return RedirectToAction("Index", "Excursions", new { area = "" });
// ReSharper restore Asp.NotResolved
        }

    }
}
