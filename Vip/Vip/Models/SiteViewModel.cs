using System.Collections.Generic;
using System.Linq;
using SiteExtensions;

namespace Vip.Models
{
    public class SiteViewModel:ISiteModel 
    {
        public string Title { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public bool IsHomePage { get; set; }
        public Menu Menu { get; set; }
        public Content Content { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Project> Projects { get; set; }

        public SiteViewModel(CatalogueContainer context,string contentName)
        {

            var categories = context.Category.ToList();
            Categories = categories;

            var projects = context.Project.ToList();
            Projects = projects;

            var contents = context.Content.Where(c => !c.MainPage).ToList();
            Menu = new Menu();
            foreach (var content in contents)
            {
                Menu.Add(new MenuItem
                             {
                                 ContentId = content.Id,
                                 ContentName = content.Name,
                                 Current = content.Name==contentName,
                                 SortOrder = content.SortOrder,
                                 Title = content.Title
                             });
            }


            if (contentName == null)
            {
                Content = context.Content.First(c => c.MainPage);
            }
            else
            {
                Content = context.Content.FirstOrDefault(c => c.Name == contentName);
                if (Content==null)
                {
                    throw new HttpNotFoundException();
                }
            }



            Title = Content.Title;
                SeoDescription = Content.SeoDescription;
                SeoKeywords = Content.SeoKeywords;
                if (Content.MainPage)
                {
                    IsHomePage = true;
                    //Layouts = context.Layout.Include("Parent").Include("Children").ToList();
                }

            
        }
    }
}