using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class Thumbnail
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }


        public override string ToString()
        {
            return "hello from image with dimentions w=" + Width + " h=" + Height;
        }
    }

    public class Thumbnails:List<Thumbnail>
    {

    }


    
}