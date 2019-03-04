using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Admin.Controllers
{
    public class ControllerBase : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            ViewBag.AssetsUrl = ConfigurationManager.AppSettings["assetsUrl"];
            base.OnActionExecuted(filterContext);
        }
        // GET: ControllerBase
        public ActionResult Index()
        {
            return View();
        }
    }
}