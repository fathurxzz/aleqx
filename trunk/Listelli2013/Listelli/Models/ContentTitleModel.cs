using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Listelli.Models
{
    public enum TitleType
    {
        Text,
        Link
    }

    public class ContentTitleModel
    {
        public TitleType Type { get; set; }
        public string Title { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }

    public class ContentTitleModel2 : ContentTitleModel
    {
        public string Title2 { get; set; }
        public string Action2 { get; set; }
        public string Controller2 { get; set; }
        public string Id { get; set; }
    }
}