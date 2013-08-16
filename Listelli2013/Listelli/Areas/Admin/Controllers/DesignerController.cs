using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Listelli.Helpers;
using Listelli.Models;
using SiteExtensions;
using SiteExtensions.Graphics;

namespace Listelli.Areas.Admin.Controllers
{

    public class DesignerController : Controller
    {
        //
        // GET: /Admin/Designer/
        [Authorize]
        public ActionResult Index()
        {
            using (var context = new PortfolioContainer())
            {
                var designers = context.Designer.ToList();
                return View(designers);
            }
        }
        [Authorize]
        public ActionResult Create()
        {

            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(Designer model, HttpPostedFileBase fileUpload)
        {
            using (var context = new PortfolioContainer())
            {

                string designerId = SiteHelper.UpdatePageWebName(model.Name);


                if (!Roles.RoleExists("Designers"))
                    Roles.CreateRole("Designers");
                if (!Roles.IsUserInRole(designerId, "Designers"))
                    Roles.AddUserToRole(designerId, "Designers");


                FormsAuthentication.SetAuthCookie(designerId, false /* createPersistentCookie */);
                //using (var context = new LibraryContainer())
                //{
                //    var customer = new Customer { Name = model.UserName, Title = model.UserTitle};
                //    context.AddToCustomer(customer);
                //    context.SaveChanges();
                //}

                //return RedirectToAction("Index", "Category", new { area = "FactoryCatalogue" });



                var designer = new Designer
                               {
                                   Name = designerId,
                                   Description = HttpUtility.HtmlDecode(model.Description)
                               };

                TryUpdateModel(designer, new[] { "DesignerName", "DesignerNameF" });


                if (fileUpload != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload, 500);
                    designer.ImageSource = fileName;
                }

                context.AddToDesigner(designer);
                context.SaveChanges();




                MembershipCreateStatus createStatus;
                Membership.CreateUser(designerId, "cde32wsx", designerId, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {

                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            using (var context = new PortfolioContainer())
            {
                var designer = context.Designer.First(d => d.Id == id);
                return View(designer);
            }
        }

        [HttpPost]
        public ActionResult Edit(Designer model, HttpPostedFileBase fileUpload)
        {
            using (var context = new PortfolioContainer())
            {
                var designer = context.Designer.First(d => d.Id == model.Id);
                designer.Name = SiteHelper.UpdatePageWebName(model.Name);
                designer.Description = HttpUtility.HtmlDecode(model.Description);

                TryUpdateModel(designer, new[] { "DesignerName", "DesignerNameF" });

                if (fileUpload != null)
                {
                    if (!string.IsNullOrEmpty(designer.ImageSource))
                    {
                        ImageHelper.DeleteImage(designer.ImageSource);
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload, 500);
                    designer.ImageSource = fileName;

                }

                context.SaveChanges();

                return RedirectToAction("Details", "Designer", new { area = "DesignersPortfolio", id = designer.Name });
            }
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            using (var context = new PortfolioContainer())
            {




                var designer = context.Designer.Include("DesignerContents").First(d => d.Id == id);
                var designerId = designer.Name;

                while (designer.DesignerContents.Any())
                {
                    var dc = designer.DesignerContents.First();
                    context.DeleteObject(dc);
                }

                ImageHelper.DeleteImage(designer.ImageSource);

                context.DeleteObject(designer);

                context.SaveChanges();


                Membership.DeleteUser(designerId, true);

            }

            return RedirectToAction("Index", "Designer", new { area = "Admin" });
        }


        public ActionResult CreateRoom(int id, int roomType)
        {
            using (var context = new PortfolioContainer())
            {
                var designer = context.Designer.First(d => d.Id == id);

                var dc = new DesignerContent
                         {
                             Designer = designer,
                             RoomType = roomType
                         };

                return View(dc);
            }
        }

        [HttpPost]
        public ActionResult CreateRoom(DesignerContent model)
        {
            using (var context = new PortfolioContainer())
            {

                var designer = context.Designer.First(d => d.Id == model.DesignerId);

                model.Description = HttpUtility.HtmlDecode(model.Description);

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file == null) continue;
                    if (string.IsNullOrEmpty(file.FileName)) continue;

                    var dci = new DesignerContantImage();
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);

                    dci.ImageSource = fileName;

                    model.DesignerContantImages.Add(dci);
                }

                context.AddToDesignerContent(model);

                context.SaveChanges();

                return RedirectToAction("RoomDetails", "Designer", new { area = "DesignersPortfolio", id = designer.Name, roomType = model.RoomType });
            }
        }

        public ActionResult EditRoom(int id)
        {
            using (var context = new PortfolioContainer())
            {
                var dc = context.DesignerContent.Include("Designer").First(d => d.Id == id);
                return View(dc);
            }
        }
        [HttpPost]
        public ActionResult EditRoom(DesignerContent model)
        {
            using (var context = new PortfolioContainer())
            {
                var dc = context.DesignerContent.Include("Designer").First(d => d.Id == model.Id);

                dc.Description = HttpUtility.HtmlDecode(model.Description);
                dc.RoomTitle = model.RoomTitle;

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file == null) continue;
                    if (string.IsNullOrEmpty(file.FileName)) continue;

                    var dci = new DesignerContantImage();
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);

                    dci.ImageSource = fileName;

                    dc.DesignerContantImages.Add(dci);
                }

                context.SaveChanges();

                return RedirectToAction("RoomDetails", "Designer", new { area = "DesignersPortfolio", id = dc.Designer.Name, roomType = dc.RoomType });
            }
        }

        public ActionResult DeleteRoom(int id)
        {
            using (var context = new PortfolioContainer())
            {
                var dc = context.DesignerContent.Include("Designer").Include("DesignerContantImages").First(d => d.Id == id);
                var dn = dc.Designer.Name;
                var rt = dc.RoomType;

                while (dc.DesignerContantImages.Any())
                {
                    var image = dc.DesignerContantImages.First();
                    ImageHelper.DeleteImage(image.ImageSource);
                    context.DeleteObject(image);
                }

                context.DeleteObject(dc);
                context.SaveChanges();

                return RedirectToAction("RoomDetails", "Designer", new { area = "DesignersPortfolio", id = dn, roomType = rt });
            }
        }


        public ActionResult DeleteImage(int id)
        {
            using (var context = new PortfolioContainer())
            {
                var image = context.DesignerContantImage.Include("DesignerContent").First(i => i.Id == id);
                var dc = context.DesignerContent.Include("Designer").First(d => d.Id == image.DesignerContentId);

                ImageHelper.DeleteImage(image.ImageSource);

                context.DeleteObject(image);
                context.SaveChanges();

                return RedirectToAction("RoomDetails", "Designer", new { area = "DesignersPortfolio", id = dc.Designer.Name, roomType = dc.RoomType });
            }
        }


    }
}
