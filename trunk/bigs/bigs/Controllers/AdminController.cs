using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using bigs.Models;
using System.IO;
using System.Web.Script.Serialization;

namespace bigs.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(NoStore = true, Duration = 1, VaryByParam = "*")]
        public ActionResult EditText(string contentUrl, string controllerName)
        {
            SiteContent content = Utils.GetContent(contentUrl);
            ViewData["controllerName"] = controllerName;
            ViewData["text"] = content.Text;
            ViewData["editTitle"] = content.Title;
            ViewData["keywords"] = content.Keywords;
            ViewData["description"] = content.Description;
            ViewData["contentUrl"] = contentUrl;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditText(string text, string editTitle, string keywords, string description, string controllerName, string contentUrl)
        {
            Utils.SetText(contentUrl, HttpUtility.HtmlDecode(text), editTitle, keywords, description); ;
            return RedirectToAction("Index", controllerName, new { contentUrl = contentUrl });
        }


        [OutputCache(NoStore = true, Duration = 1, VaryByParam = "*")]
        public ActionResult EditTransfers(string contentUrl, string controllerName)
        {
            ViewData["controllerName"] = controllerName;
            ViewData["text"] = Utils.GetContent(contentUrl).Text;
            ViewData["contentUrl"] = contentUrl;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditTransfers(string text, string controllerName, string contentUrl)
        {
            Utils.SetText(contentUrl, HttpUtility.HtmlDecode(text), null, null, null);

            using (DataStorage context = new DataStorage())
            {
                string newUrl = context.SiteContent.Where(sc => sc.Name == "Services" && sc.Language == SystemSettings.CurrentLanguage).Select(sc => sc.Url).First();



                return RedirectToAction("Index", controllerName, new { contentUrl = newUrl });
            }
        }


        [OutputCache(NoStore = true, Duration = 1, VaryByParam = "*")]
        public ActionResult EditPicture(string contentUrl, string controllerName)
        {
            using (DataStorage context = new DataStorage())
            {
                ViewData["controllerName"] = controllerName;
                ViewData["contentUrl"] = contentUrl;
                List<ImageContent> images = context.ImageContent.Select(i => i).ToList();
                return View(images);
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditPicture(string image, string contentUrl, string controllerName)
        {
            using (DataStorage context = new DataStorage())
            {

                string imageName = Request.Files["image"].FileName;
                if (!string.IsNullOrEmpty(imageName))
                {
                    imageName = Path.GetFileName(imageName);
                    string imagePath = Server.MapPath("~/Content/Objects/" + imageName);
                    Request.Files["image"].SaveAs(imagePath);
                    ImageContent imageItem = new ImageContent();
                    imageItem.FileName = imageName;
                    context.AddToImageContent(imageItem);
                    context.SaveChanges();
                }

                List<ImageContent> images = context.ImageContent.Select(i => i).ToList();
                return View(images);
            }
        }

        public ActionResult DeletePicture(int id, string contentUrl, string controllerName)
        {
            
            using (DataStorage context = new DataStorage())
            {
                ImageContent image = (from i in context.ImageContent where i.Id == id select i).First();
                string imageName = image.FileName;
                context.DeleteObject(image);
                context.SaveChanges();
                
                string path = Server.MapPath("~/Content/Objects/" + imageName);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }
            return RedirectToAction("EditPicture", "Admin", new { contentUrl = contentUrl });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateEmail(string email, string redirectUrl)
        {
            if (!string.IsNullOrEmpty(email))
            {
                ApplicationData.UpdateDestinationEmail(email);
                ViewData["email"] = email;
            }
            return Redirect(redirectUrl);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateButtonsState(FormCollection form, string redirectUrl)
        {
            using (DataStorage context = new DataStorage())
            {
                if (!string.IsNullOrEmpty(form["enablities"]))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    Dictionary<string, string> enables = serializer.Deserialize<Dictionary<string, string>>(form["enablities"]);

                    foreach (string key in enables.Keys)
                    {

                        int id = int.Parse(key);
                        ButtonStatuses sButton = (from button in context.ButtonStatuses where button.Id == id select button).First();
                        sButton.SwitchedOn = bool.Parse(enables[key]);
                    }
                    context.SaveChanges();
                }
                List<ButtonStatuses> buttons = (from button in context.ButtonStatuses where button.Language == SystemSettings.CurrentLanguage orderby button.SortOrder ascending select button).ToList();
                return Redirect(redirectUrl);
            }

        }
    }
}
