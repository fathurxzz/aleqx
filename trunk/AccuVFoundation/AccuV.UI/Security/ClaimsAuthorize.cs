using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using AccuV.DataAccess.Entities;
using AccuV.UI.Utility;
using Microsoft.AspNet.Identity;

namespace AccuV.UI.Security
{
    public class ClaimsAuthorize : AuthorizeAttribute
    {
        private readonly string _claim;
        private readonly AccessPermissions _permissions;

        public ClaimsAuthorize(string claim, AccessPermissions permissions)
        {
            _claim = claim;
            _permissions = permissions;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User == null)
                return false;

            var identity = httpContext.User.Identity as ClaimsIdentity;
            
            if (identity == null)
                return false;
           
            return identity.Claims.Any(c => c.Type == _claim && c.Value.ToAccessPermissions().Test(_permissions));
        }
    }
}