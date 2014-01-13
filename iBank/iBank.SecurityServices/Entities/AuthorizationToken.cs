using System;

namespace iBank.SecurityServices.Entities
{
    public class AuthorizationToken
    {
        public string AuthorizationTokenValue { get; set; }
        public DateTime ExpireTime { get; set; } 
    }
}