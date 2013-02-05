using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Filimonov.Models
{
    public class ContentItemProjects:ContentItem
    {
        public IEnumerable<Project> Projects { get; set; }
    }
}