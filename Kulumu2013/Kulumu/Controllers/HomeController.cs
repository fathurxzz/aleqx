using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Kulumu.Helpers;
using Kulumu.Models;
using SiteExtensions;

namespace Kulumu.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            

            using (var context = new SiteContainer())
            {
                var model = new SiteModel(context, id);
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItemName = model.Content.Name;
                ViewBag.isHomePage = model.IsHomePage;
                return View(model);
            }
        }

        public ActionResult Articles()
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(context, "articles",true);
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItemName = model.Content.Name;
                return View(model);
            }
        }

        public ActionResult ArticleDetails(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(context, "articles", true);
                this.SetSeoContent(model);
                model.Article = context.Article.First(a => a.Name == id);
                ViewBag.CurrentMenuItemName = model.Content.Name;
                return View(model);
            }
        }

        public ActionResult ProductDetails(int id)
        {
            using (var context = new SiteContainer())
            {
                var model = new GalleryModel(context, null, id);
                this.SetSeoContent(model);
                ViewBag.CurrentCategoryId = model.Category.Id;
                return View(model);
            }
        }

        public ActionResult ProductDetailsPopUp(int id, int? productImageId)
        {
            using (var context = new SiteContainer())
            {
                var model = new GalleryModel(context, null, id, productImageId);
                this.SetSeoContent(model);
                return PartialView(model);
            }
        }

        public ActionResult WorkDetails(int id)
        {
            using (var context = new SiteContainer())
            {
                var model = new WorksModel(context, id);
                this.SetSeoContent(model);
                return View(model);
            }
        }

        public ActionResult Gallery(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new GalleryModel(context, id);
                ViewBag.CurrentMenuItemName = model.Content.Name;
                ViewBag.CurrentCategoryId = model.Category.Id;
                this.SetSeoContent(model);
                return View(model);
            }
        }

        public ActionResult Tour(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(context, "tour");
                this.SetSeoContent(model);
                //ViewBag.SpecialCategoryName = context.Category.First(c => c.SpecialCategory).Name;
                ViewBag.CurrentMenuItemName = model.Content.Name;
                ViewBag.isHomePage = model.IsHomePage;
                ViewBag.FlashId = id;
                ViewBag.FlashName = id + ".html";
                return View(model);
            }
        }

        public ActionResult OurWorks()
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(context, "ourworks", false, true);
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItemName = model.Content.Name;
                return View(model);
            }
        }
        
        
        
        [HttpPost]
        public ActionResult Feedback(FeedbackFormModel feedbackFormModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string defaultMailAddressFrom = ConfigurationManager.AppSettings["feedbackEmailFrom"];
                    string defaultMailAddresses = ConfigurationManager.AppSettings["feedbackEmailsTo"];
                    string subject = ConfigurationManager.AppSettings["mailSubject"];
                    string displayName = ConfigurationManager.AppSettings["displayName"];
                    
                    var emailFrom = new MailAddress(defaultMailAddressFrom, displayName);

                    var emailsTo = defaultMailAddresses
                        .Split(new[] { ";", " ", "," }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => new MailAddress(s))
                        .ToList();

                    var result = Helpers.MailHelper.SendTemplate(emailFrom, emailsTo, subject, "FeedbackTemplate.htm", null, true, feedbackFormModel.Name, string.IsNullOrEmpty(feedbackFormModel.Email) ? "[не указано]" : feedbackFormModel.Email, feedbackFormModel.Text);
                    if (result.EmailSent)
                        return PartialView("Success");
                    feedbackFormModel.ErrorMessage = "Ошибка: " + result.ErrorMessage;
                }
                catch (Exception ex)
                {
                    feedbackFormModel.ErrorMessage = ex.Message;
                }
            }
            return PartialView("FeedbackForm", feedbackFormModel);
        }

        [HttpPost]
        public ActionResult Order(OrderFormModel orderFormModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var context = new SiteContainer())
                    {
                        var order = new Order
                                        {
                                            Address = orderFormModel.Address,
                                            Email = orderFormModel.Email,
                                            Name = orderFormModel.Name,
                                            Phone = orderFormModel.Phone,
                                            Size = orderFormModel.Size
                                        };


                        //var size = orderFormModel.Size.FirstOrDefault(s => s.Selected);
                        //if (size != null)
                        //{
                        //    order.Size = size.Text;
                        //}

                        context.AddToOrder(order);
                        context.SaveChanges();
                    }

                    string defaultMailAddressFrom = ConfigurationManager.AppSettings["feedbackEmailFrom"];
                    string defaultMailAddresses = ConfigurationManager.AppSettings["feedbackEmailsTo"];

                    string subject = ConfigurationManager.AppSettings["orderMailSubject"];
                    string displayName = ConfigurationManager.AppSettings["orderDisplayName"];

                    var emailFrom = new MailAddress(defaultMailAddressFrom, displayName);

                    var emailsTo = defaultMailAddresses
                        .Split(new[] { ";", " ", "," }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => new MailAddress(s))
                        .ToList();

                    var result = Helpers.MailHelper.SendOrderTemplate(emailFrom, emailsTo, subject, "OrderTemplate.htm", null, true, orderFormModel.Name, string.IsNullOrEmpty(orderFormModel.Email) ? "[не указано]" : orderFormModel.Email, orderFormModel.Phone, orderFormModel.Address,orderFormModel.Size,orderFormModel.ProductId,orderFormModel.ProductTitle);
                    if (result.EmailSent)
                        return PartialView("SuccessOrder");
                    orderFormModel.ErrorMessage = "Ошибка: " + result.ErrorMessage;
                }
                catch (Exception ex)
                {
                    orderFormModel.ErrorMessage = ex.Message;
                    if (ex.InnerException != null)
                        orderFormModel.ErrorMessage += " " + ex.InnerException.Message;
                }
            }
            return PartialView("OrderForm", orderFormModel);
        }


    }
}
