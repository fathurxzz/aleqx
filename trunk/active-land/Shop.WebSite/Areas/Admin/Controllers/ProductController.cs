using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class ProductController : AdminController
    {
        private readonly IShopRepository _repository;

        public ProductController(IShopRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            _repository.LangId = CurrentLangId;
            var products = _repository.GetProducts();
            return View(products);
        }


        public ActionResult Create(int id)
        {
            _repository.LangId = CurrentLangId;
            return View(new Product {CategoryId = id});
        }
    }
}
