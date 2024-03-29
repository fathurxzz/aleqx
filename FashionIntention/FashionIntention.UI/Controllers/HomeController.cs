﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionIntention.UI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

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

        private string GenerateMainMenu(int activeMenuItemId, bool active = false)
        {

            //var contents = _context.Contents.ToList();

            var result = new List<object>();

            result.Add(new { title = "оксана<br>литвиненко", cssClass = "author two-lines", selected = activeMenuItemId == 1, active = activeMenuItemId == 1 && active, url = "/about" });
            result.Add(new { title = "пресса", cssClass = "press one-line", selected = activeMenuItemId == 2, active = activeMenuItemId == 2 && active, url = "/press" });
            result.Add(new { title = "путешествия", cssClass = "travel one-line", selected = activeMenuItemId == 3, active = activeMenuItemId == 3 && active, url = "/travel" });
            result.Add(new { title = "категории", cssClass = "category one-line", selected = activeMenuItemId == 4, active = activeMenuItemId == 4 && active, url = "#", isCategoryMenuItem = true });
            result.Add(new { title = "медиа", cssClass = "media one-line", selected = activeMenuItemId == 5, active = activeMenuItemId == 5 && active, url = "/media" });


            //foreach (var content in contents.OrderBy(c => c.SortOrder))
            //{
            //    result.Add(new { id = content.Id, title = content.MenuTitle, selected = activeMenuItemId == content.Id, active = activeMenuItemId == content.Id && active, url = "/" + content.Name });
            //}

            return "dataModels.mainMenu = " + JsonConvert.SerializeObject(result);

        }

        

        public ActionResult Index()
        {
            ViewBag.MainMenu = GenerateMainMenu(0);

            

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
            ViewBag.MainMenu = GenerateMainMenu(0);

            var p = _context.Posts.First(c=>c.Id==id);

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

            ViewBag.Post = "dataModels.post = " + JsonConvert.SerializeObject(post);
            return View();
        }

        public ActionResult Posts(int year, int month)
        {
            ViewBag.MainMenu = GenerateMainMenu(0);

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

        public ActionResult About()
        {
            ViewBag.MainMenu = GenerateMainMenu(1);

            var contentList = _context.ContentItems.ToList();

            var contents = new List<object>();
            foreach (var contentItem in contentList)
            {
                contents.Add(new { text = contentItem.Text, imageSrc = contentItem.ImageSrc });
            }

            ViewBag.About = "dataModels.contents = " + JsonConvert.SerializeObject(contents);
            return View();

        }

        public ActionResult Articles()
        {
            ViewBag.MainMenu = GenerateMainMenu(2);

            var articles = new List<object>();

            var postList = _context.Articles.ToList();

            foreach (var p in postList)
            {
                articles.Add(new
                {
                    id = p.Id,
                    date = p.Date.ToShortDateString(),
                    title = p.Title,
                    description = p.Description
                });
            }

            ViewBag.Articles = "dataModels.articles = " + JsonConvert.SerializeObject(articles);
            return View();

        }

        public ActionResult Media()
        {
            ViewBag.MainMenu = GenerateMainMenu(5);

            var mediaList = _context.MediaItems.ToList();

            var media = new List<object>();
            foreach (var mediaItem in mediaList)
            {
                media.Add(new { text = mediaItem.Text, imageSrc = mediaItem.ImageSrc, videoSrc = mediaItem.VideoSrc });
            }

            ViewBag.Media = "dataModels.media = " + JsonConvert.SerializeObject(media);
            return View();

        }

        public ActionResult Travel()
        {
            ViewBag.MainMenu = GenerateMainMenu(3);
            var tag = _context.Tags.FirstOrDefault(t => t.Title == "travel");
            var posts = new List<object>();

            if (tag != null)
            {
                var postList = tag.Posts;

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
            }
            

            ViewBag.Posts = "dataModels.posts = " + JsonConvert.SerializeObject(posts);
            return View();

        }

        public ActionResult ArticleDetails(int id)
        {
            ViewBag.MainMenu = GenerateMainMenu(2, true);

            var p = _context.Articles.First(c=>c.Id==id);

            var postItems = new List<object>();
            foreach (var postItem in p.ArticleItems)
            {
                postItems.Add(new { text = postItem.Text, imageSrc = postItem.ImageSrc });
            }

            var post = new
            {
                id = p.Id,
                date = p.Date.ToShortDateString(),
                title = p.Title,
                description = p.Description,
                items = postItems
            };

            ViewBag.Article = "dataModels.article = " + JsonConvert.SerializeObject(post);

            return View();
        }

        public ActionResult Tags(int id)
        {
            ViewBag.MainMenu = GenerateMainMenu(0);
            var tag = _context.Tags.FirstOrDefault(t => t.Id == id);
            var posts = new List<object>();

            if (tag != null)
            {
                var postList = tag.Posts;

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
            }


            ViewBag.Posts = "dataModels.posts = " + JsonConvert.SerializeObject(posts);
            return View();
        }

        
        [HttpPost]
        public int Subscribe(string email)
        {
            try
            {
                var cache = _context.Subscribers.FirstOrDefault(s => s.Email == email);
                if (cache == null)
                {
                    var subscriber = new Subscriber
                    {
                        Email = email,
                        IsActive = true,
                        Guid = Guid.NewGuid().ToString("N")
                    };
                    _context.Subscribers.Add(subscriber);
                    _context.SaveChanges();
                }
            }
            catch
            {
                
                
            }
            return 0;
        }


        public ActionResult UnSubscribe(string id)
        {
            ViewBag.MainMenu = GenerateMainMenu(0);
            return View();
        }

        
        [ValidateInput(false)] 
        public ActionResult Search(string q)
        {
            ViewBag.MainMenu = GenerateMainMenu(0);

            var posts = new List<object>();

            ViewBag.Query = q;

            if (q != null && q.Length > 1)
            {


                var query = HttpUtility.HtmlDecode(q).ToLower();

                var allPosts = _context.Posts.ToList();

                var selected = new List<Post>();


                foreach (var post in allPosts)
                {
                    if (post.Title != null && post.Title.ToLower().Contains(query))
                    {
                        selected.Add(post);
                        continue;
                    }


                    if (post.PostItems.Any(postItem => postItem.Text != null && postItem.Text.ToLower().Contains(query)))
                    {
                        selected.Add(post);
                    }
                }

                foreach (var p in selected)
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

            }

            ViewBag.Posts = "dataModels.posts = " + JsonConvert.SerializeObject(posts);
           
            return View();
        }

    }
}
