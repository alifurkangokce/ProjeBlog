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
    public class CategoryController : ControllerBase
    {
        // GET: Category
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService) 
        {
            this.categoryService = categoryService;


        }
        // GET: Category
        public ActionResult Index()
        {
            var categories = categoryService.GetAll();
            return View(categories);
        }
        public ActionResult Create()
        {
            var category = new Category();
            return View(category);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Category category, HttpPostedFileBase[] Uploads)
        {
            if (ModelState.IsValid)
            {
                if (Uploads != null && Uploads.Length >= 1)
                {
                    category.CategoryFiles.Clear();
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
                                var file = new CategoryFile();
                                file.Id = Guid.NewGuid();
                                file.FileName = fileName;
                                file.CreatedAt = DateTime.Now;
                                file.CreatedBy = User.Identity.Name;
                                file.UpdatedAt = DateTime.Now;
                                file.UpdatedBy = User.Identity.Name;

                                category.CategoryFiles.Add(file);
                            }
                        }
                    }
                }
                categoryService.Insert(category);
                return RedirectToAction("Index");


            }

                return View(category);
            
        }
    
        public ActionResult Edit(Guid id)
        {

            var category = categoryService.Find(id);
            if (category == null)
            {
                return HttpNotFound();

            }
            return View(category);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var model = categoryService.Find(category.Id);
                model.Name = category.Name;
                model.Description = category.Description;
               
                categoryService.Update(model);
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            categoryService.Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult Details(Guid id)
        {
            return View(categoryService.Find(id));
        }
    }
}