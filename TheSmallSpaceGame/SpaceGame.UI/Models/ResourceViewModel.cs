using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpaceGame.DataAccess;

namespace SpaceGame.UI.Models
{
    public class ResourceViewModel:GameViewModel
    {
        public ResourceProduceLevelSet CurrentResourceProduceLevelSet { get; set; }
    }
}