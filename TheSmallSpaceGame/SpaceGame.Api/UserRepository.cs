using System;
using System.Linq;
using SpaceGame.Api.Contracts.Exceptions;
using SpaceGame.DataAccess;
using SpaceGame.DataAccess.Entities;
using SpaceGame.DataAccess.Repositories;

namespace SpaceGame.Api
{
    public class UserRepository : IUserRepository
    {
        private readonly ISpaceStore _store;

        public UserRepository(ISpaceStore store)
        {
            _store = store;
        }

        public bool ValidateUser(string email, string password)
        {
            try
            {
                var user = _store.Users.SingleOrDefault(u => u.Email == email);
                if (user == null)
                {
                    throw new UserException(string.Format("User with {0} email not found", email),
                        UserError.UserNotFound);
                }
                if (user.Password != password)
                {
                    throw new UserException("Incorrect login or password",
                        UserError.IncorrectLoginOrPassword);
                }
                return true;
            }
            catch (UserException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GameException("Repository is invalid: " + ex.Message, GameError.Unknow);
            }
        }

        public User GetUser(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    throw new UserException("email cannot be null or empty", UserError.InvalidEmail);

                var user = _store.Users.SingleOrDefault(u => u.Email == email);
                if (user == null)
                {
                    throw new UserException(string.Format("User with {0} email not found", email),
                        UserError.UserNotFound);
                }
                return user;
            }
            catch (UserException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GameException("Repository is invalid: " + ex.Message, GameError.Unknow);
            }
        }

        public User GetUser(int id)
        {
            try
            {
                var user = _store.Users.SingleOrDefault(u => u.Id == id);
                if (user == null)
                {
                    throw new UserException(string.Format("User with {0} id not found", id), UserError.UserNotFound);
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new GameException("Repository is invalid: " + ex.Message, GameError.Unknow);
            }
        }

        public User Register(string email, string name, string password)
        {

            try
            {
                if (!_store.Users.Any(u => u.Email == email))
                {
                    var user = new User { Email = email, Name = name, Password = password };
                    _store.Users.Add(user);
                    _store.SaveChanges();

                    //user.Planets.Add(new Planet {Name = "Planet", UserId = user.Id});
                    //_store.SaveChanges();

                    DateTime currentDateTime = DateTime.Now;

                    var planet = new Planet
                    {
                        UserId = user.Id,
                        Name = "MainPlanet"
                    };
                    _store.Planets.Add(planet);
                    _store.SaveChanges();



                    var metal = new Resource
                    {
                        Amount = 500,
                        ResourceTypeId = 1,
                        LastUpdate = currentDateTime,
                        MineLevel = 0,
                        PlanetId = planet.Id
                    };
                    _store.Resources.Add(metal);


                    var crystal = new Resource
                    {
                        Amount = 500,
                        ResourceTypeId = 2,
                        LastUpdate = currentDateTime,
                        MineLevel = 0,
                        PlanetId = planet.Id
                    };
                    _store.Resources.Add(crystal);

                    var deiterium = new Resource
                    {
                        Amount = 0,
                        ResourceTypeId = 3,
                        LastUpdate = currentDateTime,
                        MineLevel = 0,
                        PlanetId = planet.Id
                    };
                    _store.Resources.Add(deiterium);

                    _store.SaveChanges();

                    return user;
                }
                else
                {
                    throw new UserException("This email already exists", UserError.EmailAlreadyExists);
                }
            }
            catch (UserException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GameException("Repository is invalid: " + ex.Message, GameError.Unknow);
            }
        }

       
    }
}