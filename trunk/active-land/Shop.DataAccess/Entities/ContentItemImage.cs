namespace Shop.DataAccess.Entities
{
    public partial class ContentItemImage
    {
        public int Id { get; set; }
        public int ContentItemId { get; set; }
        public string ImageSource { get; set; }
        public virtual ContentItem ContentItem { get; set; }
    }
}
