using System;
using iBank.SecurityServices.Entities;

namespace iBank.SecurityServices
{
    public interface IWebServiceSecurity
    {
        AuthentificationToken GetAuthentificationToken(string ip);
        
        bool Authentification(string login, string password, string ip, string authentificationTokenValue);

        SessionToken OtpAuthentificationConfirm(string smsOtp, string ip, string authentificationTokenValue);
        
        AuthorizationToken BasicAuthorization(string sessionTokenValue, string ip);

        AuthorizationToken CustomAuthorization(string sessionTokenValue, string authorizationTokenValue, string ip);

        AuthorizationToken ComplexAuthorization(string authentificationTokenValue, string sessionTokenValue, string ip);

        AuthorizationToken OtpComplexAuthorizationConfirm(string smsOtp, string authentificationTokenValue, string sessionTokenValue, string ip);

        AuthentificationToken LogOff(string sessionTokenValue, DateTime sessionTokenExpireTime);
    }
}