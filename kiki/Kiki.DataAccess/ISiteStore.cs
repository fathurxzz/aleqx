using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kiki.DataAccess.Entities;

namespace Kiki.DataAccess
{
    public interface ISiteStore
    {
        IDbSet<Article> Articles { get; }
        IDbSet<Content> Contents { get; }
        IDbSet<GalleryImage> GalleryImages { get; }
        IDbSet<Reason> Reasons { get; }
        IDbSet<Sale> Sales { get; }
        IDbSet<Service> Services { get; }
        IDbSet<ServiceItem> ServiceItems { get; }
        IDbSet<SiteImage> SiteImages { get; }
        IDbSet<Subscriber> Subscribers { get; }


        int SaveChanges();
    }
}
