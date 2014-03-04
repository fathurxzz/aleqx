namespace Mayka.Helpers
{
    public enum ContentType
    {
        Content,
        Galley,
        Products
    }

    public class Menu : SiteExtensions.Menu
    {
        public ContentType ContentType { get; set; }
    }
}