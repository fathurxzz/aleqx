using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Listelli.Models;
using SiteExtensions;
using MailHelper = Listelli.Helpers.MailHelper;

namespace Listelli.Controllers
{
    public class HomeController : DefaultController
    {
        public ActionResult Index(string id)
        {

            //Thread tr = (Thread)HttpContext.Application["mailSender"];
            //var aaa = tr.ThreadState;

            using (var context = new SiteContainer())
            {
                var model = new SiteModel(CurrentLang, context, id);
                ViewBag.IsHomePage = model.IsHomePage;
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItem = model.Content.Name;
                return View(model);
            }
        }

        public ActionResult Articles()
        {
            using (var context = new SiteContainer())
            {
                var model = new ArticlesModel(CurrentLang, context);
                ViewBag.CurrentMenuItem = "news";
                return View(model);
            }
        }


//        public void S1()
//        {

//            var emails = new string[]{
//                "airis-center3@yandex.ru",
//        "a-kvalitet@mail.ru",
//"amantevivare@mail.ru",
//"andreirom@yandex.ru",
//"anfilada@inet.ua"	,
//"argabud@inet.ua"	,	
//"arhigrad.pl@mail.ru",		
//"asgart_06@mail.ru"		,
//"astudi@i.ua"			 ,
//"avrinsky@kan.gt.com.ua",	
//"babich_L@ua.fm"		 ,
//"baluti@i.kiev.ua"		 ,
//"bel-remesla@narod.ru"	,
//"bestcom2000@ukr.net"	,
//"brumalucraina@ukr.net"	,
//"bud-market@i.ua"		 ,
//"chief@archidom.in"		 ,
//"contact@d-core.com.ua"	,
//"creative-plus@i.ua"	 ,
//"d.velychko@listelli.ua" ,
//"darel64@mail.ru"		 ,
//"dd@adstudio.kiev.ua"	 ,
//"design@listelli.ua"	 ,
//"dima@new-camelot.com.ua",
//"dir@oregonmarket.com.ua",
//"dizajio@mail.ru"		 ,
//"dom@euroclass.kiev.ua"	 ,
//"dorohouse@ukr.net"		 ,
//"d-v23@yandex.ru"		 ,
//"ekonomika@ianp.com.ua"	,
//"elena@ideals.com.ua"		    ,
//"elenabusso@yahoo.it"			,
//"filonenko@bigmir.net"			,
//"floorexcl@yandex.ru"			,
//"gala-project@i.ua"				,
//"gena@geko.kiev.ua"				,
//"help@elite-design.com.ua"		,
//"i.kurilo@saloninterio.com"		,
//"ideals@voliacable.com"			,
//"info@anna-design.com.ua"		,
//"info@artinter.com.ua"			,
//"info@decoplast.eu"				,
//"info@venezia.od.ua"			,
//"interior@etre.com.ua"			,
//"isaicoluba@mail.ru"			,
//"kadva@tn.uz.ua"				,
//"kdesign@promdesign.com.ua"		,
//"kiev@itisgallery.com"			,
//"komfort-kiev@mail.ru"			,
//"kushko.alex@gmail.com"			,
//"lunapark@bigmir.net"			,
//"lyka74@list.ru"				,
//"mail@sevenplan.com"			,
//"masha-arch@mail.ru"			,
//"mavel@inbox.ru"				,
//"mebli-van@yandex.ru"			,
//"miller.kak.miller@gmail.com"	,
//"mykola.kosyk@mail.ru"			,
//"natali_ch@ukr.net"				,
//"nefedov_design@yahoo.com"		,
//"neo@art-neo.kiev.ua"			,
//"nikolay@litokol.kiev.ua"		,
//"nts1@ua.fm"					,
//"o.denysyuk@helen-marlen.com"	,	
//"o.murashko@listelli.ua"		,
//"office@ccproject.com.ua"		,
//"office@orlovskiy.com.ua"		,
//"office@vipsecurity.com.ua"		,
//"office@vivadesign.com.ua"		,
//"ofis@vizantiya.com.ua"			,
//"olesya_room@ukr.net"			,
//"pavel@art-shutters.com.ua"		,
//"poldarbud@mail.ru"				,
//"postavki@vizantiya.com.ua"		,
//"ppp90@ukr.net"					,
//"profles@i.ua"					,
//"ruslan19011@rambler.ru"		,
//"salon@listelli.com.ua"			,
//"salon@listelli.ua"				,
//"sbeast@decorum.com.ua"			,
//"sdd@voliacable.com"			,
//"sergbosh@gmail.com"			,
//"sigma-center@mail.ru"			,
//"smetaninka@yandex.ru"			,
//"soesthetic@a.ua"				,
//"spoltava@mail.ru"				,
//"sveta_@voliacable.com"           ,
//"svetadesign@inbox.ru"			  ,
//"svetlana@artprojektregie.com.ua",		
//"t.stepanets@gmail.com"			  ,
//"tam_bagrij@mail.ru"			  ,
//"tanya_exnova@ukr.net"			  ,
//"tempest@nbi.com.ua"			  ,
//"timeandquality@rambler.ru"		  ,
//"trudes@ukr.net"				  ,
//"umanec_nataliya@ukr.net"		  ,
//"v.bukovskiy@fozzy.ua"			  ,
//"valedeplen@gmail.com"			  ,
//"vikapro@meta.ua"				  ,
//"virtual_76@mail.ru"			  ,
//"vision75@mail.ru"				  ,
//"vladimir@t-style.com.ua"		  ,
//"vsedesign@gmail.com"			  ,
//"xena@aratta-art.com.ua"};

//            using (var context = new CustomerContainer())
//            {
//                foreach (var email in emails)
//                {
//                    var subscriber = new Subscriber
//                    {
//                        Guid = Guid.NewGuid().ToString(),
//                        Email = email,
//                        Active = false
//                    };

//                    context.AddToSubscriber(subscriber);


//                }

//                context.SaveChanges();


//            }
//        }

        [HttpPost]
        public ActionResult Subscribe(SubscribeFormModel subscribeForm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new CustomerContainer())
                    {
                        var subscriber = context.Subscriber.FirstOrDefault(s => s.Email == subscribeForm.SubscribeEmail);
                        if (subscriber != null)
                        {
                            subscribeForm.ErrorMessage = "Этот email уже есть в базе подписчиков";
                            return PartialView("SubscribeForm", subscribeForm);
                        }

                        subscriber = new Subscriber
                                         {
                                             Guid = Guid.NewGuid().ToString(),
                                             Email = subscribeForm.SubscribeEmail,
                                             Active = false
                                         };

                        context.AddToSubscriber(subscriber);




                        string subscribeEmailFrom = ConfigurationManager.AppSettings["subscribeEmailFrom"];

                        var emailFrom = new MailAddress(subscribeEmailFrom, "Listelli");
                        var subscriberEmail = new MailAddress(subscriber.Email);


                        var result = MailHelper.SendTemplate(emailFrom, new List<MailAddress> { subscriberEmail },
                                                                     "Подтверждение регистрации", "ConfirmSubscribe.htm",
                                                                     null, true, subscriber.Guid);

                        if (!result.EmailSent)
                        {
                            subscribeForm.ErrorMessage = "Ошибка: " + result.ErrorMessage;
                            return PartialView("SubscribeForm", subscribeForm);
                        }

                        context.SaveChanges();

                        return PartialView("SubscribeSuccess");
                    }
                }
            }

            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    subscribeForm.ErrorMessage = ex.Message;
                else if (!String.IsNullOrEmpty(ex.InnerException.Message))
                    subscribeForm.ErrorMessage = ex.InnerException.Message;
            }

            return PartialView("SubscribeForm", subscribeForm);

        }


        public ActionResult ConfirmSubscribe(string id)
        {
            using (var context = new CustomerContainer())
            {
                var subscriber = context.Subscriber.FirstOrDefault(s => s.Guid == id);
                if (subscriber == null)
                {
                    ViewBag.Message = "Неверный код подтверждения рассылки";
                }
                else
                {
                    subscriber.Active = true;
                    context.SaveChanges();
                }

                return View();
            }
        }



        public static void ProcessSendEmail()
        {
            while (true)
            {
                using (var context = new CustomerContainer())
                {
                    var emailStatus = context.SendEmailStatus.FirstOrDefault(s => s.Status == 0);

                    if (emailStatus != null)
                    {
                        using (var siteContext = new SiteContainer())
                        {
                            var article = siteContext.Article.First(c => c.Id == emailStatus.ArticleId);

                            var subscriber = context.Subscriber.First(s => s.Id == emailStatus.SubscriberId);

                            var lng = siteContext.Language.FirstOrDefault(p => p.Code == "ru");
                            if (lng != null)
                            {
                                article.CurrentLang = lng.Id;
                            }


                            string articleText = HttpUtility.HtmlDecode(article.Description).Replace("src=\"", "src=\"http://listelli.ua");

                            string subscribeEmailFrom = ConfigurationManager.AppSettings["subscribeEmailFrom"];
                            var emailFrom = new MailAddress(subscribeEmailFrom, "Listelli");
                            
                            MailHelper.SendTemplate(emailFrom, new List<MailAddress>{new MailAddress(subscriber.Email)}, article.Title, "Newsletter.htm", null, true, articleText);

                            emailStatus.Status = 1;

                            
                        }



                    }


                    //var test = new TestTable { Date = DateTime.Now };
                    //context.AddToTestTable(test);
                    context.SaveChanges();
                }

                Thread.Sleep(5000);
            }
        }
    }
}
