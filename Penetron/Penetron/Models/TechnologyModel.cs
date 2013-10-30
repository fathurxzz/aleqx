using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Penetron.Models
{
    public class TechnologyModel:SiteModel
    {
        public IList<SiteMenuItem> TechnologyMenu { get; protected set; }

        public TechnologyModel(SiteContext context, string contentId) : base(context, contentId)
        {
            //SeoDescription = Technology.SeoDescription;
            //SeoKeywords = Technology.SeoKeywords;

            var technologies = context.Technology.Include("Children").ToList();

            TechnologyMenu = new List<SiteMenuItem>();

            foreach (var technology in technologies)
            {
                if (technology.Parent == null)
                {
                    TechnologyMenu.Add(new SiteMenuItem {Name = technology.Name, Title = technology.Title, Parent = true, Id = technology.Id});
                }
            }

        }
    }
}