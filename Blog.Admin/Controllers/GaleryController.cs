using Blog.Model;
using Blog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Admin.Controllers
{
    public class GaleryController : ControllerBase
    {
        // GET: Galery
        private readonly IGaleryService galeryService;
        public GaleryController(IGaleryService galeryService)
        {
            this.galeryService = galeryService;


        }
        // GET: Category
        public ActionResult Index()
        {
            var categories = galeryService.GetAll();
            return View(categories);
        }
        public ActionResult Create()
        {
            var galery = new Galery();
            return View(galery);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Galery galery)
        {
            if (ModelState.IsValid)
            {
                galeryService.Insert(galery);
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(Guid id)
        {

            var galery = galeryService.Find(id);
            if (galery == null)
            {
                return HttpNotFound();

            }
            return View(galery);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Galery galery)
        {
            if (ModelState.IsValid)
            {
                var model = galeryService.Find(galery.Id);
                model.Title = galery.Title;
                model.Photo = galery.Photo;

                galeryService.Update(model);
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            galeryService.Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult Details(Guid id)
        {
            return View(galeryService.Find(id));
        }
    }
}