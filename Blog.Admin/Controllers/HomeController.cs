using Blog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPostService postService;
        private readonly ICategoryService categoryService;
        private readonly IGaleryService galeryService;
        public HomeController(IPostService postService, ICategoryService categoryService, IGaleryService galeryService)
        {
            this.galeryService = galeryService;
            this.postService=postService;
            this.categoryService = categoryService;
        }
        public ActionResult Index()
        {
            ViewBag.galerycount = galeryService.GetAll().Count();
            ViewBag.postcount = postService.GetAll().Count();
            ViewBag.categorycount = categoryService.GetAll().Count();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}