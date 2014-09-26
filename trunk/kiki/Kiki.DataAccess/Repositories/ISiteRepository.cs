using System;
using System.Collections.Generic;
using Kiki.DataAccess.Entities;

namespace Kiki.DataAccess.Repositories
{
    public interface ISiteRepository : IRepository
    {

        // Articles
        IEnumerable<Article> GetArticles();
        Article GetArticle(int id);
        Article GetArticle(string name);
        void DeleteArticle(int id, Action<string> deleteImages);
        int AddArticle(Article article);
        void SaveArticle(Article article);


        // Content
        IEnumerable<Content> GetContents();
        Content GetContent(int id);
        Content GetContent(string name);
        Content GetContent();
        void SaveContent(Content content);

        // Reason
        IEnumerable<Reason> GetReasons();
        Reason GetReason(int id);
        void DeleteReason(int id);
        void SaveReason(Reason reason);
        int AddReason(Reason reason);

        // SiteImage
        IEnumerable<SiteImage> GetSiteImages(ImageType imageType);
        SiteImage GetSiteImage(int id);
        void DeleteSiteImage(int id, Action<string> deleteImages);
        int AddSiteImage(SiteImage siteImage);
        void SaveSiteImage(SiteImage siteImage);

        // Sales
        IEnumerable<Sale> GetSales();
        Sale GetSale(int id);
        Sale GetSale(string name);
        void DeleteSale(int id, Action<string> deleteImages);
        int AddSale(Sale sale);
        void SaveSale(Sale sale);

        // GalleryImage
        IEnumerable<GalleryImage> GetGalleryImages();
        void DeleteGalleryImage(int id, Action<string> deleteImages);
        int AddGalleryImage(GalleryImage galleryImage);

        // Subscribers 
        IEnumerable<Subscriber> GetSubscribers();
        Subscriber GetSubscriber(int id);
        void DeleteSubscriber(int id);
        void SaveSubscriber(Subscriber subscriber);
        int AddSubscriber(Subscriber subscriber);

        //Service
        IEnumerable<Service> GetServices();
        Service GetService(int id);
        Service GetService(string name);
        IEnumerable<Service> GetSearchableServices(string query);
        IEnumerable<ServiceItem> GetSearchableServiceItems(string query);
        IEnumerable<Service> GetSearchableData(string query);
        void DeleteService(int id, Action<string> deleteImages);
        void SaveService(Service service);
        int AddService(Service service);

        //ServiceItem
        ServiceItem GetServiceItem(int id);
        void DeleteServiceItem(int id);
        void SaveServiceItem(ServiceItem service);
        int AddServiceItem(ServiceItem service);
    }
}