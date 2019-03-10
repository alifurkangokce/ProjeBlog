using Blog.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly ICategoryService categoryService;

        public PostController(IPostService postService, ICategoryService categoryService)
        {
            this.postService = postService;
            this.categoryService = categoryService;

        }
        // GET: Post
        public ActionResult Index()
        {

           // ViewBag.Categories = categoryService.GetAll();
            //int skip = (page - 1) * 5;
            //int take = 5;
            //int pageCount = Convert.ToInt32((Math.Ceiling((double)postService.GetAll().OrderByDescending(o => o.CreatedAt).Count() / (double)5)));
            //ViewBag.PageCount = pageCount;
            //// sayfa noya göre 5 kayıt getir
            //var posts = postService.GetAll().OrderByDescending(o => o.CreatedAt).Skip(skip).Take(take).ToList();

            // son yazılar

            //ViewBag.Page = page;
            ViewBag.AssetsUrl = ConfigurationManager.AppSettings["assetsUrl"];
            return View();
        }
        public ActionResult Details(Guid id)
        {
            ViewBag.Categories = categoryService.GetAll();
            var post = postService.Find(id);
            ViewBag.AssetsUrl = ConfigurationManager.AppSettings["assetsUrl"];
            return View(post);
        }

        public ActionResult RecentPosts()
        {
            ViewBag.RecentPosts = postService.GetAll().OrderByDescending(o => o.CreatedAt).Take(4).ToList();
            return PartialView();
        }
    }
}