namespace ConsoleApplication1
{
    public static class SiteSettings
    {
        private static readonly SiteGraphics Graphics;
        
        static SiteSettings()
        {
            Graphics = new SiteGraphics();
            Graphics.Initialize();
        }

        public static Thumbnail GetThumbnail(string name)
        {
            return Graphics.GetThumbnail(name);
        }
    }
}