using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            SiteGraphics graphics = new SiteGraphics();
            graphics.Initialize();


            var thumbnail = graphics.GetThumbnail("SmallPreview");


            //var thumbnail1 = graphics.GetThumbnail(ThumbnailNames.BigPreview);


            Console.WriteLine(thumbnail);



        }
    }
}
