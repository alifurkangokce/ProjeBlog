using Blog.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        // GET: Category
        public ActionResult Index()
        {
            //ViewBag.AssetsUrl = ConfigurationManager.AppSettings["assetsUrl"];
            //return View(categoryService.GetAll());
            return View();
        }
    }
}