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
            
            //SiteGraphics graphics = new SiteGraphics();
            //graphics.Initialize();


            

            //Thumbnail thumbnail = graphics.GetThumbnail("SmallPreview");




            //var thumbnail1 = graphics.GetThumbnail(ThumbnailNames.BigPreview);


            Thumbnail thumbnail = SiteSettings.GetThumbnail("SmallPreview1");

            Console.WriteLine(thumbnail);



        }
    }
}
