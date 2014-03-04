using System.Data.Entity;
using System.Linq;

namespace Mayka.Models
{
    public class GalleryModel : SiteModel
    {
        public GalleryModel(SiteContext context, string contentId)
            : base(context, contentId)
        {
            Content = context.Content.Include("PhotoGalleryItems").First(c => c.Name == contentId);
        }
    }
}