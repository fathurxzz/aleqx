using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AccuV.DataAccess.Entities
{
    public partial class User : IUser
    {
        public User()
        {
            this.UserActivities = new List<UserActivity>();
        }

        public int UserSid { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool Active { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public virtual ICollection<UserActivity> UserActivities { get; set; }
    }
}
