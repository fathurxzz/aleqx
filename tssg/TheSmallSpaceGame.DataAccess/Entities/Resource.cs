using System;
using System.Collections.Generic;

namespace TheSmallSpaceGame.DataAccess.Entities
{
    public partial class Resource
    {
        public Resource()
        {
            this.Users = new List<User>();
        }

        public int Id { get; set; }
        public long Metal { get; set; }
        public long Crystal { get; set; }
        public long Deiterium { get; set; }
        public System.DateTime LastRecalculateDate { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
