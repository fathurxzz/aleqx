using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Penetron.Models
{
    public class SearchResultModel
    {
        public string Title { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public List<SearchResultModel> Map { get; set; }
    }
}