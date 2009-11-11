using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;


namespace CollectStickers.Models
{
    public class ProfileCommon : ProfileBase
    {
        private ProfileBase profile;

        public virtual string Phone
        {
            get { return (string)profile.GetPropertyValue("Phone"); }
            set { profile.SetPropertyValue("Phone", value); }
        }

        public virtual string Icq
        {
            get { return (string)profile.GetPropertyValue("Icq"); }
            set { profile.SetPropertyValue("Icq", value); }
        }

        public virtual string UserId
        {
            get { return (string)profile.GetPropertyValue("UserId"); }
            set { profile.SetPropertyValue("UserId", value); }
        }

        protected ProfileCommon(ProfileBase profile)
        {
            this.profile = profile;
        }

        public static new ProfileCommon Create(string userName)
        {
            return new ProfileCommon(ProfileBase.Create(userName));
        }

        public override void Save()
        {
            profile.Save();
        }

        public override System.Configuration.SettingsProviderCollection Providers
        {
            get
            {
                return profile.Providers;
            }
        }

        public override System.Configuration.SettingsPropertyValueCollection PropertyValues
        {
            get
            {
                return profile.PropertyValues;
            }
        }

        public override object this[string propertyName]
        {
            get
            {
                return profile[propertyName];
            }
            set
            {
                profile[propertyName] = value;
            }
        }
        public override System.Configuration.SettingsContext Context
        {
            get
            {
                return profile.Context;
            }
        }
    }
}
