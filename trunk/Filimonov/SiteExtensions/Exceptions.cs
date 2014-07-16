using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SiteExtensions
{
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
