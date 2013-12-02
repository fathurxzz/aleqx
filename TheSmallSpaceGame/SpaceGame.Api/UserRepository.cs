using System;
using System.Collections.Generic;
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

        private int _planetId;
        private DateTime _currentDateTime;

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
                if (_store.Users.Any(u => u.Email == email))
                    throw new UserException("This email already exists", UserError.EmailAlreadyExists);

                var user = new User { Email = email, Name = name, Password = password };
                _store.Users.Add(user);
                _store.SaveChanges();

               
                var planet = new Planet
                             {
                                 UserId = user.Id,
                                 Name = "Homeworld"
                             };
                _store.Planets.Add(planet);
                _store.SaveChanges();

                _currentDateTime = DateTime.Now;
                _planetId = planet.Id;
                
                foreach (var planetResource in InitializePlanetResources())
                {
                    _store.PlanetResources.Add(planetResource);
                }
                
                foreach (var planetFacility in InitializeFacilities())
                {
                    _store.PlanetFacilities.Add(planetFacility);
                }

                _store.SaveChanges();

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


        private IEnumerable<PlanetResource> InitializePlanetResources()
        {
            return new List<PlanetResource>
                   {
                       InitializePlanetResource(500,ResourceItem.Metal),
                       InitializePlanetResource(500,ResourceItem.Crystal),
                       InitializePlanetResource(0,ResourceItem.Deiterium)
                   };
        }

        private PlanetResource InitializePlanetResource(double amount, ResourceItem resourceItem)
        {
            return new PlanetResource
            {
                Amount = amount,
                ResourceId = (int)resourceItem,
                LastUpdate = _currentDateTime,
                MineLevel = 0,
                PlanetId = _planetId
            };
        }


        private IEnumerable<PlanetFacility> InitializeFacilities()
        {
            return new List<PlanetFacility>
                         {
                             InitializeFacility(FacilityItem.RoboticsFactory),
                             InitializeFacility(FacilityItem.Shipyard),
                             InitializeFacility(FacilityItem.ResearchLab),
                             InitializeFacility(FacilityItem.NaniteFactory)
                         };
        }

        private PlanetFacility InitializeFacility(FacilityItem facilityItem)
        {
            return new PlanetFacility
            {
                FacilityId = (int)facilityItem,
                IsUpdating = false,
                PlanetId = _planetId,
                Level = 0,
                UpdateStart = DateTime.Now
            };
        }


    }
}