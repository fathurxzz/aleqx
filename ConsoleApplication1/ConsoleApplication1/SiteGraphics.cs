namespace ConsoleApplication1
{
    public class SiteGraphics : Graphics
    {
        public override void Initialize()
        {
            Thumbnails = new Thumbnails();
            Thumbnails.Add(new Thumbnail { Width = 200, Height = 100, Name = "SmallPreview" });
            Thumbnails.Add(new Thumbnail { Width = 400, Height = 300, Name = "BigPreview" });
        }
    }
}