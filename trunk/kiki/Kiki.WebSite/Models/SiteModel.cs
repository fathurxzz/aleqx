using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kiki.DataAccess;
using Kiki.DataAccess.Entities;
using Kiki.DataAccess.Repositories;
using Kiki.WebSite.Areas.Admin.Controllers;
using Kiki.WebSite.Helpers;

namespace Kiki.WebSite.Models
{
    public class SiteModel : ISiteModel
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public bool IsHomePage { get; set; }
        public IEnumerable<SiteImage> MainImages { get; set; }
        public IEnumerable<SiteImage> Attentions { get; set; }
        public IEnumerable<SiteImage> Banners { get; set; }
        public SiteImage Banner { get; set; }
        public IEnumerable<Content> Contents { get; set; }
        public Content Content { get; set; }
        public IEnumerable<Reason> Reasons { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Sale> Sales { get; set; }
        public IEnumerable<Service> Services { get; set; }
        public SiteImage MainImage { get; set; }
        public SiteImage Attention { get; set; }
        public IEnumerable<GalleryImage> GalleryImages { get; set; }

        public SiteModel(ISiteRepository repository, string contentName)
        {
            Title = "Kiki Mornet image salon";
            MainImages = repository.GetSiteImages(ImageType.MainImage);
            Attentions = repository.GetSiteImages(ImageType.Attention);
            Banners = repository.GetSiteImages(ImageType.Banner);
            Contents = repository.GetContents();
            Articles = repository.GetArticles();
            Sales = repository.GetSales();
            Services = repository.GetServices();
            GalleryImages = repository.GetGalleryImages();

            Content = contentName != null 
                ? Contents.FirstOrDefault(c => c.Name == contentName) 
                : Contents.FirstOrDefault(c => c.ContentType == 0);

            Banner = Banners.OrderBy(r => Guid.NewGuid()).Take(1).FirstOrDefault();
            MainImage = MainImages.OrderBy(r => Guid.NewGuid()).Take(1).FirstOrDefault();
            Attention = Attentions.OrderBy(r => Guid.NewGuid()).Take(1).FirstOrDefault();

            Reasons = repository.GetReasons();

        }
    }
}