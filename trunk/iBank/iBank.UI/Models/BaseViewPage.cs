using System;
using System.Web;
using System.Web.Mvc;
using iBank.Api.Exceptions;
using iBank.SecurityServices;
using iBank.SecurityServices.Entities;

namespace iBank.UI.Models
{

    public class BaseViewPage : WebViewPage
    {
        public virtual new User User
        {
            get
            {
                try
                {
                    var cookie = Request.Cookies[SiteSettings.TokenId];
                    if (cookie == null)
                        throw new SecurityException("cannot resolve cookie", SecurityError.Unknow);
                    if (cookie["userName"] == null)
                        throw new SecurityException("cannot resolve cookie", SecurityError.Unknow);
                    return new User { Name = cookie["userName"] };
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return User != null;
            }
        }


        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }


    public class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual new User User
        {
            get
            {
                try
                {
                    var cookie = Request.Cookies[SiteSettings.TokenId];
                    if (cookie == null)
                        throw new SecurityException("cannot resolve cookie", SecurityError.Unknow);
                    if (cookie["userName"] == null)
                        throw new SecurityException("cannot resolve cookie", SecurityError.Unknow);
                    return new User { Name = cookie["userName"] };
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return User != null;
            }
        }


        public override void Execute() { throw new NotImplementedException(); }


    }
}