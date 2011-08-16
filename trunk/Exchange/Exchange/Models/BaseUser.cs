using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Exchange.ua.com.pib.@base.webaccess;

namespace Exchange.Models
{
    public abstract class BaseUser
    {
        public List<int> SiteRoleIds = new List<int>();

        protected virtual List<int> GetSiteRoles(string podrid, string tabNum)
        {
            //var result = new List<int>();

            string authentificationServiceUrl = ConfigurationManager.AppSettings["AuthentificationServiceUrl"];
            if (authentificationServiceUrl == null)
                throw new ConfigurationErrorsException(
                            "В файле конфигурации не указан адрес веб-сервиса аутентификации пользователя (AuthentificationServiceUrl)");

            string group = ConfigurationManager.AppSettings["DealerGroup"];
            if (group == null)
                throw new ConfigurationErrorsException(
                            "В файле конфигурации не указан номер группы \"Біржові валютні операції\" (AuthentificationGroup)");

            var aws = new AccessWebService
            {
                Url = authentificationServiceUrl,
                CookieContainer = new System.Net.CookieContainer()
            };

            /*
            int len = aws.getTaskRoles3_len(Convert.ToInt32(group), podrid, tabNum, 1);

            if(len>0)
            {
                Struct_Roles3[] sg = aws.getTaskRoles3();
                for(int i = 0; i < len; i++)
                {
                    result.Add(sg[i].ROLE_ID);
                }
            }
            */

            Struct_Roles3[] sg = aws.getTaskRoles3();
            SiteRoleIds = sg.Select(r => r.ROLE_ID).ToList();
            


            aws.ReleaseSession();
            //SiteRoleIds = result;
            return SiteRoleIds;
        }
    }
}