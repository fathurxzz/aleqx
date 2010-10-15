using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excursions.Helpers;
using Excursions.Models;


namespace Excursions.Controllers
{
    public class ExcursionsController : Controller
    {
        //
        // GET: /Excursions/

        [HttpGet]
        public ActionResult Index()
        {
            using (var context = new ContentStorage())
            {
                var excursionList = context.Excursion.Select(e => e).OrderBy(e => e.SortOrder).ToList();
                return View(excursionList);
            }
        }


        public ActionResult Details(string id)
        {
            using (var context = new ContentStorage())
            {
                var excursion = context.Excursion.Include("Comments").Select(e => e).Where(e => e.Name == id).First();
                return View(excursion);
            }
        }

        public ActionResult AddComment(int id)
        {
            ViewData["excursionId"] = id;
            return View();
        }

        [HttpPost]
        public void AddComment(AddCommentFormModel addCommentFormModel, int excursionId)
        {
            using (var context = new ContentStorage())
            {
                
                

                Excursion excursion = context.Excursion.Select(e => e).Where(e => e.Id == excursionId).First();
                
                var comment = new Comments
                                  {
                                      Excursion = excursion,
                                      Text = HttpUtility.HtmlDecode(addCommentFormModel.Text),
                                      Author = addCommentFormModel.Name,
                                      Date = DateTime.Now
                                  };
                context.AddToComments(comment);
                context.SaveChanges();

                string linkBase = ConfigurationManager.AppSettings["linkBase"];
                string emailFrom = ConfigurationManager.AppSettings["emailFrom"];
                string emailsTo = ApplicationData.NewCommentNotificationEmail;
                const string subject = "testours.1gb.ua - Новый комментарий";
                string[] replacements = {
                                                linkBase + "/Excursions/" + excursion.Name, comment.Date.ToString()
                                                ,
                                                comment.Author, comment.Text
                                            };

                MailHelper.SendTemplate(emailFrom, emailsTo, subject, "newComment", false, replacements);


                

                //Response.Write("<script>window.top.$.fancybox.close()</script>");
                Response.Write("<script>window.top.location.href=window.top.location.href</script>");

                //Redirect("~/Excursions/Details/" + excursion.Name);
                
                //return RedirectToAction("Details", "Excursions", new { area = "", id = excursion.Name });

                
                
                //return RedirectToAction("Details", "Excursions", new { area = "", id = excursion.Name });
            }

        }

    }
}
