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
        public GaleryController(IGaleryService galeryService)
        {
            this.galeryService = galeryService;
        }
        // GET: Galery
        public ActionResult Index()
        {


            ViewBag.AssetsUrl = ConfigurationManager.AppSettings["assetsUrl"];
            return View();
        }
    }
}