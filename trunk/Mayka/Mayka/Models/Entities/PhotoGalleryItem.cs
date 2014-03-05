namespace Mayka.Models
{
    public partial class PhotoGalleryItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageSource { get; set; }
        public string Url { get; set; }
        public int ContentId { get; set; }

        public virtual Content Content { get; set; }
    }

}
