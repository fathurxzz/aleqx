using System;
using iBank.SecurityServices;
using iBank.SecurityServices.Entities;

namespace iBank.Api.WebServices
{
    public class AuthenticationService : IAuthenticationService
    {

        public AuthentificationToken GetAuthentificationToken(string ip)
        {
            return new AuthentificationToken
            {
                AuthentificationTokenValue = "authentication_token_" + ip,
                ExpireTime = DateTime.Now.AddMinutes(15)
            };
        }

        public bool Authentification(string login, string password, string ip, string authentificationTokenValue)
        {
            if (login == "alex" && password == "111")
                return true;
            return false;
        }

        public SessionToken OtpAuthentificationConfirm(string smsOtp, string ip, string authentificationTokenValue)
        {
            return new SessionToken
            {
                ExpireTime = DateTime.Now.AddMinutes(15),
                SessionTokenValue = "session_token_" + ip,
            };
        }

        public AuthorizationToken BasicAuthorization(string sessionTokenValue, string ip)
        {
            return new AuthorizationToken
            {
                AuthorizationTokenValue = "222",
                ExpireTime = DateTime.Now.AddMinutes(5)
            };
        }

        public AuthorizationToken CustomAuthorization(string sessionTokenValue, string authorizationTokenValue, string ip)
        {
            return new AuthorizationToken
            {
                AuthorizationTokenValue = "222",
                ExpireTime = DateTime.Now.AddMinutes(5)
            };
        }

        public AuthorizationToken ComplexAuthorization(string authentificationTokenValue, string sessionTokenValue, string ip)
        {
            return new AuthorizationToken
            {
                AuthorizationTokenValue = "222",
                ExpireTime = DateTime.Now.AddMinutes(5)
            };
        }

        public AuthorizationToken OtpComplexAuthorizationConfirm(string smsOtp, string authentificationTokenValue,
            string sessionTokenValue, string ip)
        {
            return new AuthorizationToken
            {
                AuthorizationTokenValue = "222",
                ExpireTime = DateTime.Now.AddMinutes(5)
            };
        }

        public AuthentificationToken LogOff(string sessionTokenValue, DateTime sessionTokenExpireTime)
        {
            return new AuthentificationToken
            {
                AuthentificationTokenValue = "111",
                ExpireTime = DateTime.Now.AddMinutes(5)
            };
        }
    }
}