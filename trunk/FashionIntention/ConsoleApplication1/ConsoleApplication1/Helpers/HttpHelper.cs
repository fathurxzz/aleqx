using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Helpers
{
    class HttpHelper
    {
        public static GetHtmlResult GetHtml(string url)
        {
            var result = new GetHtmlResult();
            try
            {
                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(url);

                IWebProxy proxy = myWebRequest.Proxy;
                if (proxy != null)
                {
                    string proxyuri = proxy.GetProxy(myWebRequest.RequestUri).ToString();
                    myWebRequest.UseDefaultCredentials = true;
                    myWebRequest.Proxy = new WebProxy(proxyuri, false);
                    myWebRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                }

                using (HttpWebResponse response = (HttpWebResponse)myWebRequest.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader readStream = null;
                        if (response.CharacterSet == null)
                        {
                            readStream = new StreamReader(responseStream);
                        }
                        else
                        {
                            readStream = new StreamReader(responseStream, Encoding.GetEncoding(response.CharacterSet));
                        }
                        result.ResultHtml = readStream.ReadToEnd();
                        result.DownloadedDataSize = result.ResultHtml.Length;
                        response.Close();
                        readStream.Close();
                        result.Success = true;
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        result.ErrorMessage = "file not found " + url;
                    }
                    else
                    {
                        result.ErrorMessage = ex.Message + "response.StatusCode:" + resp.StatusCode;
                    }
                }
                else
                {
                    result.ErrorMessage = ex.Message + "ex.Status:" + ex.Status;
                }
            }

            return result;
        }
    }
}
