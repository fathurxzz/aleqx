using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Controllers;
using Listelli.Models;

namespace Listelli.Areas.Admin.Controllers
{
    public class ContentController : AdminController
    {
        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                Content content = context.Content.First(c => c.Id == id);
                content.CurrentLang = CurrentLang.Id;
                return View(content);
            }
        }

        [HttpPost]
        public ActionResult Edit(Content model)
        {
            using (var context = new SiteContainer())
            {
                var cache = context.Content.FirstOrDefault(p => p.Id == model.Id);

                if (cache != null)
                {
                    var lang = context.Language.FirstOrDefault(p => p.Id == model.CurrentLang);
                    if (lang != null)
                    {
                        CreateOrChangeContentLang(context, model, cache, lang);
                    }
                }

                return RedirectToAction("Index", "Home",new{area="",id=cache.Name});
            }

            
        }

        private void CreateOrChangeContentLang(SiteContainer context, Content instance, Content cache, Language lang)
        {
           
                ContentLang contenttLang = null;
                if (cache != null)
                {
                    contenttLang = context.ContentLang.FirstOrDefault(p => p.ContentId == cache.Id && p.LanguageId == lang.Id);
                }
                if (contenttLang == null)
                {
                    var newPostLang = new ContentLang
                                          {
                                              ContentId = instance.Id,
                                              LanguageId = lang.Id,
                                              Title = instance.Title,
                                              Text = instance.Text,
                                              SeoDescription = instance.SeoDescription,
                                              SeoKeywords = instance.SeoKeywords
                                          };
                    context.AddToContentLang(newPostLang);
                }
                else
                {
                    contenttLang.Title = instance.Title;
                    contenttLang.Text = instance.Text;
                    contenttLang.SeoDescription = instance.SeoDescription;
                    contenttLang.SeoKeywords = instance.SeoKeywords;
                }
                context.SaveChanges();
           
        }

    }
}
