using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpaceGame.DataAccess.Repositories;

namespace SpaceGame.UI.Controllers
{
    public class FacilityController : Controller
    {

        private IFacilityRepository _repository;

        public FacilityController(IFacilityRepository repository)
        {
            _repository = repository;
        }

        

    }
}
