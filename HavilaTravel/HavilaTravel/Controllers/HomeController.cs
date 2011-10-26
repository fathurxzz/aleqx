using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Models;

namespace HavilaTravel.Controllers
{
    public class HomeController : Controller
    {



        private Menu GetMenuItemsOfCurrentContent(int contentId, ContentStorage context)
        {

        }

        


        public ActionResult Index(string id)
        {
            //ViewBag.Message = "Welcome to ASP.NET MVC!";

            using (var context = new ContentStorage())
            {
                var menus = new List<Menu>();

                var headerLeftMenuItems = context.Content.Where(m => m.ContentType == 10).ToList();
                ViewBag.HeaderLeftMenuItems = headerLeftMenuItems;
                
                
                var mainMenuItems = context.Content.Where(m => m.ContentType == 1 && m.ContentLevel == 1).Select(m => m).ToList();

                var menu = new Menu{MenuLevel = (int)mainMenuItems.First().ContentLevel};
                menu.AddRange(
                    mainMenuItems.Select(
                        item =>
                        new MenuItem
                            {Id = (int) item.Id, Name = item.Name, Title = item.Title, Selected = id == item.Name}));
                menus.Add(menu);

                

                ViewBag.MainMenuItems = mainMenuItems;

                ViewBag.CurrentContentName = id;







                /*
                var content = context.Content
                    .Include("Parent").Include("Children").Include("Accordions")
                    .Where(c => c.Name == id || c.ContentType == 0)
                    .First();
                */

                var content = context.Content
                    .Include("Parent").Include("Children").Include("Accordions")
                    .Where(c => c.Name == id)
                    .FirstOrDefault();

                if (content == null)
                {
                    content = context.Content
                        .Include("Children").Include("Accordions")
                        .Where(c => c.ContentType == 0)
                        .First();
                }



                //var contentNameChain = new List<string>();

                var currentContent = content;
                
                while (currentContent.Parent != null)
                {
                    //contentNameChain.Add(currentContent.Name);



                    currentContent = context.Content.Include("Parent").Where(c => c.Id == content.Parent.Id).First();




                }
                //contentNameChain.Add(currentContent.Name);


                //ViewBag.ContentNameChain = contentNameChain;

                foreach (var accordion in content.Accordions)
                {
                    accordion.AccordionImages.Load();
                }



                ViewBag.CurrentContentId = content.Id;


                ViewBag.SeoDescription = content.SeoDescription;
                ViewBag.SeoKeywords = content.SeoKeywords;
                ViewBag.SubMenuItems = content.Children.ToList();

                ViewBag.Title = content.Title;

                return View(content);
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
