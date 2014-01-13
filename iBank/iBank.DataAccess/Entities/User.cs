using System;

namespace iBank.DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string LogIn { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpirePasswordDate { get; set; }
    }
}