using System.ComponentModel;
using System.Linq;

namespace ConsoleApplication1
{
    public class GraphicsHelper
    {
        protected Thumbnails Thumbnails;

        public virtual void Initialize()
        {

        }

        public Thumbnail GetThumbnail(string name)
        {
            return Thumbnails.FirstOrDefault(th => th.Name == name);
        }

        //public Thumbnail GetThumbnail(ThumbnailNames name)
        //{

        //}

    }
}