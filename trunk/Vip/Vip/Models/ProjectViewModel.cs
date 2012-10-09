using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vip.Models
{
    public class ProjectViewModel : SiteViewModel
    {
        public Project Project { get; set; }
        public IEnumerable<Project> Projects { get; set; }

        public ProjectViewModel(CatalogueContainer context, string project)
            : base(context, null)
        {
            Title = "Проекты";

            if (string.IsNullOrEmpty(project))
            {
                Projects = context.Project.Where(p=>!p.MainPage).ToList();
                Project = context.Project.First(p => p.MainPage);
            }
            else
            {
                Project = context.Project.Include("ProjectImages").First(c => c.Name == project);
                Title += " - " + Project.Title;
                SeoDescription = Project.SeoDescription;
                SeoKeywords = Project.SeoKeywords;
            }


        }
    }
}