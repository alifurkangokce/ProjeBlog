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
            ViewBag.Categories = categoryService.GetAll();
            ViewBag.AssetsUrl = ConfigurationManager.AppSettings["assetsUrl"];
            return View();
            
        }
        public ActionResult Details(Guid id)
        {
            var category = categoryService.Find(id);
            ViewBag.Categories = categoryService.GetAll();
            ViewBag.AssetsUrl = ConfigurationManager.AppSettings["assetsUrl"];
            return View(category);
        }
    }
}