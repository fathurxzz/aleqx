using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using AccuVAPI.Configuration;
using AccuVAPI.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AccuVAPI.DocManagement
{
    public class BoxAuthenticator
    {
        private static readonly string Login;
        private static readonly string Password;
        private static readonly string AuthApi;
        private static readonly string TokenApi;
        private static readonly string ClientId;
        private static readonly string ClientSecret;


        static BoxAuthenticator()
        {
            var config = (AccuVapiConfigSection)System.Configuration.ConfigurationManager.GetSection("accuVAPI");
            Login = config.BoxConfig.BoxUsername;
            Password = config.BoxConfig.BoxPassword;
            AuthApi = config.BoxConfig.BoxAuthPath;
            TokenApi = config.BoxConfig.BoxTokenPath;
            ClientId = config.BoxConfig.BoxClientId;
            ClientSecret = config.BoxConfig.BoxClientSecret;
        }

        #region Built-In Params

        private Dictionary<string, string> _authParams;
        private Dictionary<string, string> AuthParams 
        {
            get
            {
                if (_allowParams == null)
                {
                    _authParams = new Dictionary<string, string>();
                    _authParams["login"] = Login;
                    _authParams["password"] = Password;
                    _authParams["client_id"] = ClientId;
                    _authParams["response_type"] = "code";
                    _authParams["redirect_uri"] = AuthApi;
                    _authParams["state"] = "authenticated";
                }
                return _authParams;
            }
        }

        private Dictionary<string, string> _allowParams;
        private Dictionary<string, string> AllowParams
        {
            get
            {
                if (_allowParams == null)
                {
                    _allowParams = new Dictionary<string, string>();
                    _allowParams["client_id"] = ClientId;
                    _allowParams["response_type"] = "code";
                    _allowParams["redirect_uri"] = AuthApi;
                    _allowParams["scope"] = "root_readwrite";
                    _allowParams["state"] = "authenticated";
                    _allowParams["doconsent"] = "doconsent";
                    _allowParams["consent_accept"] = "Accept";
                }
                return _allowParams;
            }
        }

        private Dictionary<string, string> _accessParams;
        private Dictionary<string, string> AccessParams
        {
            get
            {
                if (_accessParams == null)
                {
                    _accessParams = new Dictionary<string, string>();
                    _accessParams["grant_type"] = "authorization_code";
                    _accessParams["client_id"] = ClientId;
                    _accessParams["client_secret"] = ClientSecret;
                    _accessParams["redirect_uri"] = AuthApi;
                }
                return _accessParams;
            }
        }

        #endregion

        /// <summary>
        /// 
        /// As part of this method we are taking 3 steps
        /// 1 - box.net authentication (fill in the username/password form and submit it)
        /// 2 - consent (the user is normally asked to allow the application to access the box.net account; 
        ///              so we submit the second form simulating the "Allow" click)
        /// 3 - after we extract the "code" from step #2, we make another request to extract the "access_token" and "refresh_token"
        /// 
        /// </summary>
        /// <returns>A | separated string containing access_token|refresh_token</returns>
        public string Authenticate()
        {
            string boxAccessToken = string.Empty;

            RequestManager mgr = new RequestManager();


            // Step #1
            string authContent = CreateURLParamsFromDictionary(AuthParams);
            HttpWebResponse authResponse = mgr.SendPOSTRequest(AuthApi, authContent);

            // Step #2
            // Fill in one important parameter of the consent step
            AllowParams["ic"] = ExtractIC(authResponse);
            string allowContent = CreateURLParamsFromDictionary(AllowParams);
            HttpWebResponse allowResponse = mgr.SendPOSTRequest(AuthApi, allowContent);

            // Step #3
            NameValueCollection authCompleteParams = HttpUtility.ParseQueryString(allowResponse.ResponseUri.Query.ToString());
            if (authCompleteParams["code"] != null)
            {
                AccessParams["code"] = authCompleteParams["code"];
                string accessContent = CreateURLParamsFromDictionary(AccessParams);
                HttpWebResponse accessResponse = mgr.SendPOSTRequest(TokenApi, accessContent);
                string accessResponseJSONString = new StreamReader(accessResponse.GetResponseStream()).ReadToEnd();
                JObject accessResponseObject = JsonConvert.DeserializeObject<JObject>(accessResponseJSONString);

                if (accessResponseObject["access_token"] != null)
                {
                    boxAccessToken = accessResponseJSONString;
                }
            }

            return boxAccessToken;
        }

        private string ExtractIC(HttpWebResponse authResponse)
        {
            string stringResponse = new StreamReader(authResponse.GetResponseStream()).ReadToEnd();
            Match icMatch = Regex.Match(stringResponse, @"name=""ic"" value="".*""", RegexOptions.IgnoreCase);

            if (icMatch.Success)
            {
                var nameValue = icMatch.Groups[0].Value.ToString().Split(' ');
                if (nameValue.Count() == 2)
                {
                    var ic = nameValue[1].Split('=')[1].Trim('"');
                    if (ic != null)
                        return ic;

                }
            }

            return "";
        }

        private string CreateURLParamsFromDictionary(Dictionary<string, string> d)
        {
            StringBuilder result = new StringBuilder();
            foreach (KeyValuePair<string, string> parameter in d)
            {
                result.Append(parameter.Key);
                result.Append("=");
                result.Append(HttpUtility.UrlEncode(parameter.Value));
                result.Append("&");
            }
            result.Remove(result.Length - 1, 1);
            return result.ToString();
        }
    }
}