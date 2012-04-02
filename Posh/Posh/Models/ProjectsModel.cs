using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Posh.Models
{
    public class ProjectsModel:SiteViewModel
    {
        public List<Project> Projects { get; set; }
        public Project Project { get; set; }

        public ProjectsModel(ModelContainer dataContext, string contentId, string projectId, bool loadContent=true) 
            : base(dataContext, contentId,loadContent)
        {



        }
    }
}