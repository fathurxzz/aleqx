using System.Web;

namespace SiteExtensions
{
    public class HttpNotFoundException : HttpException
    {
        public HttpNotFoundException()
            : base(404, "")
        {

        }

        public HttpNotFoundException(string objectName)
            : base(404, objectName + " not found")
        {

        }
    }
}
