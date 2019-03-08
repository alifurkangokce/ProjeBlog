using Blog.Model;
using Blog.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Admin.Controllers
{
    public class GaleryController : ControllerBase
    {
        // GET: Galery
        private readonly IGaleryService galeryService;
        public GaleryController(IGaleryService galeryService):base()
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
        public ActionResult Create(Galery galery, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(upload.FileName);
                    string extension = Path.GetExtension(fileName).ToLower();
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif")
                    {
                        string path = Path.Combine(ConfigurationManager.AppSettings["uploadPath"], fileName);
                        upload.SaveAs(path);
                        galery.Photo = fileName;
                        galeryService.Insert(galery);
                        return RedirectToAction("index");
                    }
                    else
                    {
                        ModelState.AddModelError("Photo", "Dosya uzantısı .jpg, .jpeg, .png ya da .gif olmalıdır.");
                    }
                }
                else
                {
                    galeryService.Insert(galery);
                    return RedirectToAction("index");
                }

            }
            
            return View(galery);
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
        public ActionResult Edit(Galery galery, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(upload.FileName);
                    string extension = Path.GetExtension(fileName).ToLower();
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif")
                    {
                        string path = Path.Combine(ConfigurationManager.AppSettings["uploadPath"], fileName);
                        upload.SaveAs(path);
                        galery.Photo = fileName;
                        galeryService.Update(galery);
                        return RedirectToAction("index");
                    }
                    else
                    {
                        ModelState.AddModelError("Photo", "Dosya uzantısı .jpg, .jpeg, .png ya da .gif olmalıdır.");

                    }
                }
                else
                {
                    // resim seçilip yüklenmese bile diğer bilgileri güncelle
                    galeryService.Update(galery);
                    return RedirectToAction("index");
                }


            }
           
            return View(galery);
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