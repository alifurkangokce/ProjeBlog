using Blog.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Admin.Controllers
{
    public class ApplicationUserController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationUserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: ApplicationUser
        public ActionResult Index()
        {
            var users = UserManager.Users.ToList();
            return View(users);
        }
        public ActionResult Delete(string id)
        {
            var userToDelete = UserManager.FindById(id);
            var currentUserId = User.Identity.GetUserId();
            if (userToDelete == null)
            {
                TempData["NullUser"] = "Uyarı: Kullanıcı bulunamadı!";
                return RedirectToAction("Index");
            }
            if (userToDelete.Id == currentUserId)
            {
                TempData["CurrentUser"] = "Uyarı: Kendinizi silemezsiniz!";
                return RedirectToAction("Index");
            }
            UserManager.Delete(userToDelete);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string id)
        {
            var user = UserManager.FindById(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var model = UserManager.FindById(user.Id);
                model.FullName = user.FullName;
                model.Email = user.Email;
                model.Photo = user.Photo;
                UserManager.Update(model);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}