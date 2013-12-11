using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using SpaceGame.Api.Contracts.Exceptions;
using SpaceGame.Api.Model.Entities;
using SpaceGame.DataAccess.Repositories;
using SpaceGame.UI.Helpers;

namespace SpaceGame.UI.Controllers
{
    [Authorize]
    public class ResourceController : BaseController
    {
        private readonly IResourceRepository _repository;

        public ResourceController(IResourceRepository repository, IPlanetRepository planetRepository)
            : base(planetRepository)
        {
            _repository = repository;
        }

        public ActionResult UpgradeResource(int id)
        {
            try
            {
                switch ((ResourceItem) id)
                {
                    case ResourceItem.Metal:
                        _repository.UpdateMetalMine(WebSession.PlanetId, CurrentRoboticsLevel, CurrentNaniteLevel);
                        break;
                    case ResourceItem.Crystal:
                        _repository.UpdateCrystalMine(WebSession.PlanetId, CurrentRoboticsLevel, CurrentNaniteLevel);
                        break;
                    case ResourceItem.Deiterium:
                        _repository.UpdateDeiteriumGenerator(WebSession.PlanetId, CurrentRoboticsLevel,
                            CurrentNaniteLevel);
                        break;
                    default:
                        throw new InvalidEnumArgumentException();
                }
            }
            catch (GameException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }

            return RedirectToAction("Resources", "Home");
        }

        //public ActionResult UpgradeMetalMine()
        //{
        //    try
        //    {
        //        _repository.UpdateMetalMine(WebSession.PlanetId, CurrentRoboticsLevel, CurrentNaniteLevel);
        //    }
        //    catch (GameException ex)
        //    {
        //        TempData["errorMessage"] = ex.Message;
        //    }

        //    return RedirectToAction("Resources", "Home");
        //}

        //public ActionResult UpgradeCrystalMine()
        //{
        //    try
        //    {
        //        _repository.UpdateCrystalMine(WebSession.PlanetId, CurrentRoboticsLevel, CurrentNaniteLevel);
        //    }
        //    catch (GameException ex)
        //    {
        //        TempData["errorMessage"] = ex.Message;
        //    }

        //    return RedirectToAction("Resources", "Home");
        //}

        //public ActionResult UpgradeDeiteriumGenerator()
        //{
        //    try
        //    {
        //        _repository.UpdateDeiteriumGenerator(WebSession.PlanetId, CurrentRoboticsLevel, CurrentNaniteLevel);
        //    }
        //    catch (GameException ex)
        //    {
        //        TempData["errorMessage"] = ex.Message;
        //    }

        //    return RedirectToAction("Resources", "Home");
        //}
    }
}
