using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAjax.Models;
using System.Security.Cryptography;

namespace TestAjax.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View(new RegistrationForm());
        }

        [HttpPost]
        public PartialViewResult Index(RegistrationForm model)
        {
            if (ModelState.IsValid)
            {
                //go and do registration business logic,
                RNGCryptoServiceProvider csp = new RNGCryptoServiceProvider();

                byte[] regsitrationBytes = new byte[16];
                csp.GetBytes(regsitrationBytes);
                model.RegsitrationUniqueId = Convert.ToBase64String(regsitrationBytes);
                return PartialView("Success", model);
            }
            else
            {
                //return the data entry form
                return PartialView("FormContent", model);
            }
        }
    }


}
