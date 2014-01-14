using System;
using System.Linq;
using iBank.Api.Exceptions;

namespace iBank.Api.Repositories
{
    //public class UserRepository:IUserRepository
    //{
    //    private readonly IBankStore _store;

    //    public UserRepository(IBankStore store)
    //    {
    //        _store = store;
    //    }

    //    public User GetUser(string login, string password)
    //    {
    //        try
    //        {
    //            var user = _store.Users.SingleOrDefault(u => u.Login == login);
    //            if (user == null)
    //            {
    //                throw new SecurityException(string.Format("User {0} not found", login),
    //                    SecurityError.UserNotFound);
    //            }
    //            if (user.Password != password)
    //            {
    //                throw new SecurityException("Incorrect login or password",
    //                    SecurityError.IncorrectLoginOrPassword);
    //            }
    //            return user;
    //        }
    //        catch (SecurityException)
    //        {
    //            throw;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new RepositoryException("Repository is invalid: " + ex.Message, RepositoryError.Unknow);
    //        }
    //    }
    //}
}