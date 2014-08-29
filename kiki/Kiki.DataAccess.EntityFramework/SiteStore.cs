using System.Data.Entity;
using Kiki.DataAccess.Entities;

namespace Kiki.DataAccess.Models
{
    public class SiteStore : ISiteStore
    {
        readonly SiteContext _context = new SiteContext();

        public IDbSet<Article> Articles
        {
            get { return _context.Articles; }
        }
        public IDbSet<Attention> Attentions
        {
            get { return _context.Attentions; }
        }
        public IDbSet<Content> Contents
        {
            get { return _context.Contents; }
        }
        public IDbSet<GalleryImage> GalleryImages
        {
            get { return _context.GalleryImages; }
        }
        public IDbSet<Reason> Reasons
        {
            get { return _context.Reasons; }
        }
        public IDbSet<Sale> Sales
        {
            get { return _context.Sales; }
        }
        public IDbSet<Service> Services
        {
            get { return _context.Services; }
        }
        public IDbSet<ServiceItem> ServiceItems
        {
            get { return _context.ServiceItems; }
        }
        public IDbSet<SiteImage> SiteImages
        {
            get { return _context.SiteImages; }
        }
        public IDbSet<Subscriber> Subscribers
        {
            get { return _context.Subscribers; }
        }
    }
}