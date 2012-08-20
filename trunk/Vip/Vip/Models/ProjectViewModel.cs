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

        public ProjectViewModel(SiteContainer context, string project) : base(context)
        {
            Title = "Проекты";

            if (string.IsNullOrEmpty(project))
            {
                Projects = context.Project.ToList();
            }
            else
            {
                Project = context.Project.Include("ProjectImages").First(c => c.Name == project);
                Title += " - " + Project.Title;
            }


        }
    }
}