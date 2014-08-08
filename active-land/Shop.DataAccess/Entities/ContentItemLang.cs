namespace Shop.DataAccess.Entities
{
    public partial class ContentItemLang
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ContentItemId { get; set; }
        public int LanguageId { get; set; }
        public virtual ContentItem ContentItem { get; set; }
        public virtual Language Language { get; set; }
    }
}
