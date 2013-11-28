using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpaceGame.Api;
using SpaceGame.DataAccess;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.UI.Models
{
    public class GameViewModel
    {
        public ResourceSet ResourceSet { get; set; }
        public List<PlanetPresentation> Planets { get; set; }
        public int CurrentPlanetId { get; set; }
        public string ErrorMessage { get; set; }

    }
}