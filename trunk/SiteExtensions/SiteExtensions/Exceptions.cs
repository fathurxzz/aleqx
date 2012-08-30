using System.Web;

namespace SiteExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpNotFoundException : HttpException
    {
        /// <summary>
        /// 
        /// </summary>
        public HttpNotFoundException()
            : base(404, "")
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectName"></param>
        public HttpNotFoundException(string objectName)
            : base(404, objectName + " not found")
        {

        }
    }
}
