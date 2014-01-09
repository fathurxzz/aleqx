using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccuVAPI.Configuration
{
    public class AccuVapiConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("boxConfig")]
        public BoxConfigSection BoxConfig
        {
            get { return (BoxConfigSection)this["boxConfig"]; }
            set { this["boxConfig"] = value; }
        }

        [ConfigurationProperty("activeDirectoryAccess")]
        public ActiveDirectorySection  ActiveDirectoryConfig
        {
            get { return (ActiveDirectorySection)this["activeDirectoryAccess"]; }
            set { this["activeDirectoryAccess"] = value; }
        }
    }

    public class BoxConfigSection : ConfigurationElement
    {
        [ConfigurationProperty("userName")]
        public string BoxUsername
        {
            get { return (string)this["userName"]; }
            set { this["userName"] = value; }
        }

        [ConfigurationProperty("password")]
        public string BoxPassword
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }

        [ConfigurationProperty("clientId")]
        public string BoxClientId
        {
            get { return (string)this["clientId"]; }
            set { this["clientId"] = value; }
        }

        [ConfigurationProperty("clientSecret")]
        public string BoxClientSecret
        {
            get { return (string)this["clientSecret"]; }
            set { this["clientSecret"] = value; }
        }

        [ConfigurationProperty("authPath")]
        public string BoxAuthPath
        {
            get { return (string)this["authPath"]; }
            set { this["authPath"] = value; }
        }


        [ConfigurationProperty("tokenPath")]
        public string BoxTokenPath
        {
            get { return (string)this["tokenPath"]; }
            set { this["tokenPath"] = value; }
        }
    }

    public class ActiveDirectorySection : ConfigurationElement
    {
        [ConfigurationProperty("userName")]
        public string UserName
        {
            get { return (string)this["userName"]; }
            set { this["userName"] = value; }
        }

        [ConfigurationProperty("password")]
        public string Password
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }

        [ConfigurationProperty("ldapConnection")]
        public string LdapConnection
        {
            get { return (string)this["ldapConnection"]; }
            set { this["ldapConnection"] = value; }
        }
    }
}
