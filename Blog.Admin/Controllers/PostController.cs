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
        private readonly IPostFileService postFileService;
        public PostController(IPostService postService, ICategoryService categoryService, IPostFileService postFileService) :base()
        {
            this.postService = postService;
            this.categoryService = categoryService;
            this.postFileService = postFileService;
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
        public ActionResult Create(Post post, HttpPostedFileBase[] Uploads)
        {
            if (ModelState.IsValid)
            {
                postService.Insert(post);
                if (Uploads != null && Uploads.Length >= 1)
                {
                    post.PostFiles.Clear();
                    foreach (var item in Uploads)
                    {
                        if (item != null && item.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(item.FileName);
                            var extension = Path.GetExtension(fileName).ToLower();
                            if (extension == ".jpg" || extension == ".gif" || extension == ".png" || extension == ".pdf" || extension == ".doc" || extension == ".docx"|| extension==".JPEG")
                            {
                                var path = Path.Combine(ConfigurationManager.AppSettings["uploadPath"], fileName);
                                item.SaveAs(path);
                                var file = new PostFile();
                                file.Id = Guid.NewGuid();
                                file.FileName = fileName;
                                file.CreatedAt = DateTime.Now;
                                file.CreatedBy = User.Identity.Name;
                                file.UpdatedAt = DateTime.Now;
                                file.UpdatedBy = User.Identity.Name;
                                file.PostId = post.Id;
                                postFileService.Insert(file);
                            }
                        }
                    }
                }
                
                return RedirectToAction("Index");
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
        public ActionResult Edit(Post post, HttpPostedFileBase[] Uploads)
        {
            if (ModelState.IsValid)
            {

                var model = postService.Find(post.Id);
                if (Uploads != null && Uploads.Length >= 1)
                {
                    model.PostFiles.Clear();
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
                                var file = new PostFile();
                                file.Id = Guid.NewGuid();
                                file.FileName = fileName;
                                file.CreatedAt = DateTime.Now;
                                file.CreatedBy = User.Identity.Name;
                                file.UpdatedAt = DateTime.Now;
                                file.UpdatedBy = User.Identity.Name;
                                file.PostId = post.Id;
                                postFileService.Insert(file);
                            }
                        }
                    }
                }




                model.Title = post.Title;
                
                model.Description = post.Description;
               
               
                model.Photo = post.Photo;
                model.CategoryId = post.CategoryId;
                postService.Update(model);
            
                return RedirectToAction("Index");


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
