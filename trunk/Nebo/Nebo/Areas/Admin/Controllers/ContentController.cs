using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nebo.Helpers;
using Nebo.Models;

namespace Nebo.Areas.Admin.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/

        public ActionResult Add(int? parentId)
        {
            ViewData["parentId"] = parentId;
            return View(new Content { SortOrder = 0 });
        }

        [HttpPost]
        public ActionResult Add(FormCollection form, HttpPostedFileBase fileUpload, HttpPostedFileBase fileBannerUpload)
        {
            using (var context = new ContentStorage())
            {

                var content = new Content { ContentLevel = 1 };

                if (!string.IsNullOrEmpty(form["parentId"]))
                {
                    int parentId = Convert.ToInt32(form["parentId"]);
                    var parent = context.Content.Where(c => c.Id == parentId).First();
                    content.Parent = parent;
                    content.ContentLevel = parent.ContentLevel + 1;
                }


                TryUpdateModel(content, new[]
                                            {
                                                "Name",
                                                "Title",
                                                "SortOrder",
                                                "SeoDescription",
                                                "SeoKeywords"
                                            });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);

                if (fileUpload != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    fileUpload.SaveAs(filePath);
                    content.ImageSource = fileName;
                }

                if (fileBannerUpload != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileBannerUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    fileBannerUpload.SaveAs(filePath);
                    content.Banner = fileName;
                }

                context.AddToContent(content);

                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { id = content.Name, area = "" });
            }
        }





        public ActionResult Edit(int id)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();
                return View(content);
            }
        }

        [HttpPost]
        public ActionResult Edit(Content model, FormCollection form, HttpPostedFileBase fileUpload, HttpPostedFileBase fileBannerUpload, bool deleteImage, bool deleteBannerImage)
        {

            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == model.Id).First();

                TryUpdateModel(content, new[]
                                            {
                                                "Name",
                                                "Title",
                                                "SortOrder",
                                                "SeoDescription",
                                                "SeoKeywords",
                                            });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);

                if (deleteImage)
                {
                    if(content.ImageSource!=null)
                    {
                        IOHelper.DeleteFile("~/Content/Images", content.ImageSource);
                        content.ImageSource = null;
                    }
                }
                
                if (deleteBannerImage)
                {
                    if (content.Banner != null)
                    {
                        IOHelper.DeleteFile("~/Content/Images", content.Banner);
                        content.Banner = null;
                    }
                }

                if (fileUpload != null)
                {
                    if (content.ImageSource != null)
                    {
                        IOHelper.DeleteFile("~/Content/Images", content.ImageSource);
                    }
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    fileUpload.SaveAs(filePath);
                    content.ImageSource = fileName;
                }

                if (fileBannerUpload != null)
                {
                    if (content.Banner != null)
                    {
                        IOHelper.DeleteFile("~/Content/Images", content.Banner);
                    }
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileBannerUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    fileBannerUpload.SaveAs(filePath);
                    content.Banner = fileName;
                }

                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { id = content.Name, area = "" });
            }
        }


        public ActionResult Delete(int id)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Include("Children").Include("Parent").Where(c => c.Id == id).First();
                
                while (content.Children.Any())
                {
                    var child = content.Children.First();
                    if(child.ImageSource!=null)
                    {
                        IOHelper.DeleteFile("~/Content/Images", child.ImageSource);
                    }
                    if (child.Banner != null)
                    {
                        IOHelper.DeleteFile("~/Content/Images", child.Banner);
                    }
                    context.DeleteObject(child);
                }

                if (content.ImageSource != null)
                {
                    IOHelper.DeleteFile("~/Content/Images", content.ImageSource);
                }
                if (content.Banner != null)
                {
                    IOHelper.DeleteFile("~/Content/Images", content.Banner);
                }
                context.DeleteObject(content);

                context.SaveChanges();
                

                return RedirectToAction("Index", "Home", new { Area = "" });
            }
        }

    }
}
