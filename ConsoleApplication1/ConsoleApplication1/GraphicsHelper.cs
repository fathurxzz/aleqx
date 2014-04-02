using System.ComponentModel;
using System.Linq;

namespace ConsoleApplication1
{
    public abstract class Graphics
    {
        public Thumbnails Thumbnails;

        public abstract void Initialize();
       

        public Thumbnail GetThumbnail(string name)
        {
            return Thumbnails.FirstOrDefault(th => th.Name == name);
        }

        //public Thumbnail GetThumbnail(ThumbnailNames name)
        //{

        //}

    }
}