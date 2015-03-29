using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionIntention.UI.Models;
using Newtonsoft.Json;

namespace FashionIntention.UI.Controllers
{
    public class HomeController : DefaultController
    {
        private readonly SiteContext _context;

        public HomeController(SiteContext context)
            : base(context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var posts = new List<object>();
            var postList = _context.Posts.OrderByDescending(p => p.Date).Take(4).ToList();
            foreach (var p in postList)
            {
                var tags = new List<object>();
                foreach (var t in p.Tags)
                {
                    tags.Add(t.Title);
                }

                posts.Add(new
                {
                    id = p.Id,
                    date = p.Date.ToShortDateString(),
                    title = p.Title,
                    description = p.Description,
                    imageSrc = p.ImageSrc,
                    tags = tags
                });
            }
            ViewBag.Posts = "dataModels.posts = " + JsonConvert.SerializeObject(posts);

            return View();
        }

        //
        // GET: /Home/Details/5

        public ActionResult PostDetails(int id)
        {
            var p = _context.Posts.First();

            var postItems = new List<object>();
            foreach (var postItem in p.PostItems)
            {
                postItems.Add(new { text = postItem.Text, imageSrc = postItem.ImageSrc });
            }

            var post = new
            {
                id = p.Id,
                date = p.Date.ToShortDateString(),
                title = p.Title,
                description = p.Description,
                imageSrc = p.ImageSrc,
                tags = p.Tags.Select(t => t.Title).Cast<object>().ToList(),
                items = postItems
            };

            ViewBag.Posts = "dataModels.post = " + JsonConvert.SerializeObject(post);
            return View();
        }

        public ActionResult Posts(int year, int month)
        {
            var posts = new List<object>();

            var postList = _context.Posts.Where(p=>p.Date.Year==year && p.Date.Month==month).ToList();

            foreach (var p in postList)
            {
                posts.Add(new
                {
                    id = p.Id,
                    date = p.Date.ToShortDateString(),
                    title = p.Title,
                    description = p.Description,
                    imageSrc = p.ImageSrc,
                    tags = p.Tags.Select(t => t.Title).Cast<object>().ToList()
                });
            }

            ViewBag.Posts = "dataModels.posts = " + JsonConvert.SerializeObject(posts);
            return View();
        }


    }
}
