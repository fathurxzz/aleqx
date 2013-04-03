using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filimonov.Models;

namespace Filimonov.Areas.Presentation.Controllers
{
    [Authorize]
    public class SurveyController : Controller
    {
        //
        // GET: /Presentation/Survey/

        public ActionResult Index()
        {
            using (var context = new LibraryContainer())
            {
                var survays = context.Survey.Include("Customer").Include("SurveyItems").ToList();
                ViewBag.CurrentItem = "survey";
                return View(survays);
            }
        }



        //
        // GET: /Presentation/Survey/Details/5

        public ActionResult Details(string id)
        {
            using (var context = new LibraryContainer())
            {
                var customer = context.Customer.Include("Survey").First(c => c.Name == id);
                customer.Survey.SurveyItems.Load();
                var survey = customer.Survey;
                return View(survey);
            }
        }


        public ActionResult Edit(int id)
        {
            using (var context = new LibraryContainer())
            {
                //var customer = context.Customer.Include("Surveys").First(c => c.Name == id);
                var survay = context.Survey.First(s => s.Id == id);
                return View(survay);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new LibraryContainer())
            {
                var survay = context.Survey.First(s => s.Id == id);

                TryUpdateModel(survay, new[] { "Title", "Date" });
                survay.Description = HttpUtility.HtmlDecode(form["Description"]);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Presentation/Survey/Create

        public ActionResult Create()
        {
            using (var context = new LibraryContainer())
            {
                var customers = context.Customer.Include("Survey").Where(c => c.Name != "admin" && c.Survey == null).ToList();
                ViewBag.Customers = customers;
                return View();
            }
        }

        //
        // POST: /Presentation/Survey/Create

        [HttpPost]
        public ActionResult Create(FormCollection form, int customerId)
        {
            try
            {
                using (var context = new LibraryContainer())
                {
                    var customer = context.Customer.First(c => c.Id == customerId);

                    var survey = new Survey
                                     {
                                         Date = DateTime.Now,
                                         Title = form["Title"],
                                         Description = HttpUtility.HtmlDecode(form["Description"]),
                                         Customer = customer
                                     };

                    context.AddToSurvey(survey);
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new LibraryContainer())
            {
                var survey = context.Survey.Include("SurveyItems").First(s => s.Id == id);
                while (survey.SurveyItems.Any())
                {
                    var si = survey.SurveyItems.First();
                    context.DeleteObject(si);
                }
                context.DeleteObject(survey);

                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [OutputCache(VaryByParam = "*", NoStore = true, Duration = 1)]
        [HttpPost]
        public string CreateSurveyItem(string number,string question)
        {
            return question;

            //using (var context = new LibraryContainer())
            //{

            //}
        }

    }
}
