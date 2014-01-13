using System;

namespace iBank.SecurityServices.Entities
{
    public class AuthentificationToken
    {
        public string AuthentificationTokenValue { get; set; }
        public DateTime ExpireTime { get; set; } 
    }
}