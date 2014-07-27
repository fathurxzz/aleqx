using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.EntityFramework;
using Shop.DataAccess.Repositories;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {

        //private readonly ShopContext _context;

        //public CategoryController(ShopContext context)
        //{
        //    _context = context;
        //}

        private readonly IShopRepository _repository;

        public CategoryController(IShopRepository repository):base(repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            //var categories = _context.Categories.ToList();
            //foreach (var category in categories)
            //{
            //    category.CurrentLang = CurrentLang.Id;
            //}
            
            var categories = _repository.GetCategories(CurrentLangId);



            return View(categories);
        }

    }
}
