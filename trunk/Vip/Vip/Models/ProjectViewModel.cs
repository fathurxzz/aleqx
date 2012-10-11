using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vip.Models
{
    public class ProjectViewModel : SiteViewModel
    {
        public Project Project { get; set; }

        public ProjectViewModel(CatalogueContainer context, string project)
            : base(context, null)
        {
            Title = "Проекты";

            foreach (var p in Projects.Where(p=>p.Name==project))
            {
                p.Current = true;
            }

            if (!string.IsNullOrEmpty(project))
            {
                Project = context.Project.Include("ProjectImages").First(c => c.Name == project);
                Title += " - " + Project.Title;
                SeoDescription = Project.SeoDescription;
                SeoKeywords = Project.SeoKeywords;
            }


        }
    }
}