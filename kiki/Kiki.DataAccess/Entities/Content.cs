namespace Kiki.DataAccess.Entities
{
    public partial class Content
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string MenuTitle { get; set; }
        public int SortOrder { get; set; }
        public int ContentType { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoText { get; set; }
        public string Text { get; set; }
    }
}
