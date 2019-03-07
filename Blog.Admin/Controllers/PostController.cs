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
    public class PostController : ControllerBase
    {
        // GET: Post
        private readonly IPostService postService;
        private readonly ICategoryService categoryService;
        public PostController(IPostService postService, ICategoryService categoryService):base()
        {
            this.postService = postService;
            this.categoryService = categoryService;
        }
        // GET: Post
        public ActionResult Index()
        {
            var post = postService.GetAll();
            return View(post);
        }
        public ActionResult Create()
        {
            var post = new Post();
            ViewBag.CategoryId = new SelectList(categoryService.GetAll(), "Id", "Name");
            return View(post);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Post post, HttpPostedFileBase upload)
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
                        post.Photo = fileName;
                        postService.Insert(post);
                        return RedirectToAction("index");
                    }
                    else
                    {
                        ModelState.AddModelError("Photo", "Dosya uzantısı .jpg, .jpeg, .png ya da .gif olmalıdır.");
                    }
                }
                else
                {
                    postService.Insert(post);
                    return RedirectToAction("index");
                }

            }
            ViewBag.CategoryId = new SelectList(categoryService.GetAll(), "Id", "Name", post.CategoryId);
            return View(post);
        }
        public ActionResult Edit(Guid id)
        {
            var post = postService.Find(id);
            if (post == null)
            {
                return HttpNotFound();

            }
            ViewBag.CategoryId = new SelectList(categoryService.GetAll(), "Id", "Name", post.CategoryId);
            return View(post);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Post post, HttpPostedFileBase upload)
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
                        post.Photo = fileName;
                        postService.Update(post);
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
                    postService.Update(post);
                    return RedirectToAction("index");
                }


            }
            ViewBag.CategoryId = new SelectList(categoryService.GetAll(), "Id", "Name", post.CategoryId);
            return View(post);
        }
        public ActionResult Delete(Guid id)
        {
            postService.Delete(id);
            return RedirectToAction("index");
        }

        public ActionResult Details(Guid id)
        {
            var post = postService.Find(id);
            if (post == null)
            {
                return HttpNotFound();

            }

            return View(post);
        }
    }
}
