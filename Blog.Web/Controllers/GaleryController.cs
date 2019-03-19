using Blog.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class GaleryController : Controller
    {
        private readonly IGaleryService galeryService;
        private readonly ICategoryService categoryService;
        public GaleryController(IGaleryService galeryService, ICategoryService categoryService)
        {
            this.categoryService = categoryService;
            this.galeryService = galeryService;
        }
        // GET: Galery
        public ActionResult Index()
        {
       
            ViewBag.AssetsUrl = ConfigurationManager.AppSettings["assetsUrl"];
            ViewBag.Categories = categoryService.GetAll();
            return View(galeryService.GetAll());
        }
    }
}