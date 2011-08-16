using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exchange.Models;
using Exchange.Models.Factories;
using Exchange.Models.Helpers;
using Exchange.Models.MVC;

namespace Exchange.Controllers
{
    public class SignersController : Controller
    {
        //
        // GET: /Signers/

        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.AdminFrontOffice, SiteRoles.AdminFrontOfficeBranch, SiteRoles.DealerFrontOfficeBranch, SiteRoles.DealerFrontOffice })]
        public ActionResult Index()
        {
            using (var conn = DbConnector.Connection)
            {
                var signerFactory = new SignerFactory(conn);
                var signers = signerFactory.GetOfficialsByOfficeId(WebSession.CurrentUser.OfficeId);
                return View(signers);
            }
        }

        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.AdminFrontOffice, SiteRoles.AdminFrontOfficeBranch })]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.AdminFrontOffice, SiteRoles.AdminFrontOfficeBranch })]
        public ActionResult Create(Signer s)
        {
            s.OfficeId = WebSession.CurrentUser.OfficeId;
            using (var conn = DbConnector.Connection)
            {
                var signerFactory = new SignerFactory(conn);
                signerFactory.SaveChanges(s);
            }
            return RedirectToAction("Index");
        }

        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.AdminFrontOffice, SiteRoles.AdminFrontOfficeBranch })]
        public ActionResult Delete(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                var signerFactory = new SignerFactory(conn);
                Signer signer = signerFactory.GetSigner(id);
                signerFactory.Delete(signer);
            }
            return RedirectToAction("Index");
        }

        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.AdminFrontOffice, SiteRoles.AdminFrontOfficeBranch })]
        public ActionResult Edit(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                var signerFactory = new SignerFactory(conn);
                Signer signer = signerFactory.GetSigner(id);
                return View(signer);
            }
            
        }

        [HttpPost]
        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.AdminFrontOffice, SiteRoles.AdminFrontOfficeBranch })]
        public ActionResult Edit(Signer s, int id)
        {
            using (var conn = DbConnector.Connection)
            {
                var signerFactory = new SignerFactory(conn);
                signerFactory.SaveChanges(s);
            }
            return RedirectToAction("Index");
        }

    }
}
