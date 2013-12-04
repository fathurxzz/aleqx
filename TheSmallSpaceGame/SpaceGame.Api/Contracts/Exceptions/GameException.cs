using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame.Api.Contracts.Exceptions
{
    public enum GameError
    {
        Unknow = 0,
        PlanetNotFound = 1,
        NotEnoughResources = 2,
        FacilityDoesNotExistOrUpdating=3
    }

    public class GameException : Exception
    {
        private readonly GameError _error;

        public GameException()
        {

        }

        public GameException(string message, GameError error)
            : base(message)
        {
            _error = error;
        }

        public GameError Error
        {
            get { return _error; }
        }

    }
}
