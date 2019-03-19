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
        private readonly ICategoryService categoryService;
        public GaleryController(IGaleryService galeryService,ICategoryService categoryService) :base()
        {
            this.galeryService = galeryService;
            this.categoryService = categoryService;

        }
        // GET: Category
        public ActionResult Index()
        {
            var galeries = galeryService.GetAll();
            ViewBag.Categories = categoryService.GetAll();
            return View(galeries);
        }
        public ActionResult Create()
        {
            var galery = new Galery();
            return View(galery);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Galery galery, HttpPostedFileBase[] Uploads)
        {
            if (ModelState.IsValid)
            {
                if (Uploads != null && Uploads.Length >= 1)
                {
                    galery.GaleryFiles.Clear();
                    foreach (var item in Uploads)
                    {
                        if (item != null && item.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(item.FileName);
                            var extension = Path.GetExtension(fileName).ToLower();
                            if (extension == ".jpg" || extension == ".gif" || extension == ".png" || extension == ".pdf" || extension == ".doc" || extension == ".docx")
                            {
                                var path = Path.Combine(ConfigurationManager.AppSettings["uploadPath"], fileName);
                                item.SaveAs(path);
                                var file = new GaleryFile();
                                file.Id = Guid.NewGuid();
                                file.FileName = fileName;
                                file.CreatedAt = DateTime.Now;
                                file.CreatedBy = User.Identity.Name;
                                file.UpdatedAt = DateTime.Now;
                                file.UpdatedBy = User.Identity.Name;

                                galery.GaleryFiles.Add(file);
                            }
                        }
                    }
                }
                galeryService.Insert(galery);
                return RedirectToAction("Index");
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