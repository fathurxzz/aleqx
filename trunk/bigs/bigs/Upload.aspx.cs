using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using bigs.Models;
using bigs.Controllers;

namespace bigs
{
    public partial class Upload : System.Web.UI.Page
    {
        private string contentUrl
        {
            get
            {
                return Request.Form["contentUrl"];
            }
        }

        private string controllerName
        {
            get
            {
                return Request.Form["controllerName"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
            using (DataStorage context = new DataStorage())
            {

                string imageName = Request.Files["image"].FileName;
                if (!string.IsNullOrEmpty(imageName))
                {
                    imageName = Path.GetFileName(imageName);
                    string imagePath = Server.MapPath("~/Content/Objects/" + imageName);
                    Request.Files["image"].SaveAs(imagePath);
                    Request.Files["image"].InputStream.Close();
                    ImageContent imageItem = new ImageContent();
                    imageItem.FileName = imageName;
                    imageItem.Language = SystemSettings.CurrentLanguage;
                    imageItem.Url = Request.Form["url"];
                    context.AddToImageContent(imageItem);
                    context.SaveChanges();
                }

                
                
                //List<ImageContent> images = context.ImageContent.Where(i => i.Language == SystemSettings.CurrentLanguage).Select(i => i).ToList();
                //return View(images);
            }
            Response.Write("<script>window.close()</script>");
            //Response.Redirect("Admin/EditPicture/?contentUrl=" + contentUrl + "&controllerName=" + controllerName);
        }
    }
}
