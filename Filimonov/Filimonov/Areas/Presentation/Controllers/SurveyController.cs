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
            if (User.Identity.Name != "admin")
            {
                if (User.Identity.Name != id)
                {
                    throw new Exception();
                }
            }

            ViewBag.CurrentItem = "survey-details";

            using (var context = new LibraryContainer())
            {
                var customer = context.Customer.Include("Survey").First(c => c.Name == id);
                //customer.Survey.SurveyItems.Load();
                if (customer.Survey !=null)
                {
                    var survey = context.Survey.Include("SurveyItems").FirstOrDefault(s => s.Id == customer.Survey.Id);
                    return View(survey);
                }

                return View();
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

        public ActionResult DeleteSurveyItem(int id)
        {
            using (var context = new LibraryContainer())
            {
                var surveyItem = context.SurveyItem.Include("Survey").First(si => si.Id == id);
                var surveyId = surveyItem.Survey.Id;
                var survey = context.Survey.Include("Customer").First(s => s.Id == surveyId);
                context.DeleteObject(surveyItem);
                context.SaveChanges();
                return RedirectToAction("Details", "Survey", new { id = survey.Customer.Name });
            }
        }


        [OutputCache(VaryByParam = "*", NoStore = true, Duration = 1)]
        [HttpPost]
        public ActionResult SaveSurvey(FormCollection form)
        {
            string number = form["inputNumber"];
            string question = form["txtQuestion"];
            int surveyId = Convert.ToInt32(form["surveyId"]);

            try
            {
                if (string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(question))
                    return View("NewSurveyItem");
                using (var context = new LibraryContainer())
                {
                    var survey = context.Survey.FirstOrDefault(s => s.Id == surveyId);
                    if (survey == null)
                        return View("NewSurveyItem");
                    var surveyItem = new SurveyItem { Survey = survey, Number = number, Question = question };
                    context.AddToSurveyItem(surveyItem);
                    context.SaveChanges();
                    return View("NewSurveyItem", surveyItem);
                }
            }
            catch
            {
                return View("NewSurveyItem");
            }
        }

        [OutputCache(VaryByParam = "*", NoStore = true, Duration = 1)]
        [HttpPost]
        public string UpdateSurvey(int id, FormCollection form)
        {
            using (var context = new LibraryContainer())
            {
                var surveyItem = context.SurveyItem.First(s => s.Id == id);
                surveyItem.Question = form["taQuestion_" + id];
                surveyItem.Number = form["tbNumber_" + id];
                context.SaveChanges();
                return surveyItem.Number + "###" + surveyItem.Question;
            }
        }

        [OutputCache(VaryByParam = "*", NoStore = true, Duration = 1)]
        [HttpPost]
        public bool SaveAnswer(FormCollection form)
        {
            int surveyId = Convert.ToInt32(form["surveyId"]);

            try
            {
                using (var context = new LibraryContainer())
                {
                    var survey = context.Survey.Include("SurveyItems").First(s => s.Id == surveyId);
                    foreach (var item in survey.SurveyItems)
                    {
                        item.Answer = form["taAnswer_" + item.Id];
                        item.Note = form["taNote_" + item.Id];
                    }
                    context.SaveChanges();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

    }
}
