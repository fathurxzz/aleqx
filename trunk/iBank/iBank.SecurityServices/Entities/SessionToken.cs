using System;

namespace iBank.SecurityServices.Entities
{
    public class SessionToken
    {
        public string SessionTokenValue { get; set; }
        public DateTime ExpireTime { get; set; } 
    }
}