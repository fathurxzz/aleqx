using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteExtensions;

namespace Filimonov.Models
{
    public class ProjectModel : SiteModel
    {
        public Project Project { get; set; }

        public ProjectModel(SiteContainer context, string id)
            : base(context)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Project = context.Project
                    .Include("ProjectImages")
                    //.Include("Songs")
                    //.Include("FlashContents")
                    .FirstOrDefault(p => p.Name == id);

                

                if (Project == null)
                {
                    throw new HttpNotFoundException(string.Format("Project with key {0}", id));
                }
                
                Project.Songs.Load();
                Project.FlashContents.Load();
                foreach (var flashContent in Project.FlashContents)
                {
                    flashContent.DirectoryName = flashContent.ImageSource;
                    flashContent.ImageSource = flashContent.ImageSource + ".html";
                }
            }

        }
    }
}