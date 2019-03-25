using Blog.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService postService;
        private readonly ICategoryService categoryService;
        public HomeController(IPostService postService, ICategoryService categoryService)
        {
            this.postService = postService;
            this.categoryService = categoryService;
        }
        public ActionResult Index(int? page)
        {
           
            ViewBag.Categories = categoryService.GetAll();
            ViewBag.AssetsUrl = ConfigurationManager.AppSettings["assetsUrl"];
            var posts = postService.GetAll();
            var pageNumber = page ?? 1;
            var onePageOfPosts = posts.ToPagedList(pageNumber, 3);
            return View(onePageOfPosts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.Categories = categoryService.GetAll();
            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "İletişim sayfası.";
            ViewBag.Categories = categoryService.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string firstName, string lastName, string email, string phone, string department, string message)
        {

            firstName = firstName.Trim();
            lastName = lastName.Trim();

            if (firstName == "")
            {

                ViewBag.Message = "Ad alanı gereklidir.";
                ViewBag.IsError = true;
                return View();
            }
            if (firstName.Length > 50)
            {
                ViewBag.Message = "Ad alanı 50 karakterden uzun olamaz";
                ViewBag.IsError = true;
                return View();
            }
            if (lastName == "")
            {

                ViewBag.Message = "Soyad alanı gereklidir";
                ViewBag.IsError = true;
                return View();
            }

            Regex regex = new Regex(@"^5(0[5-7]|[3-5]\d) ?\d{3} ?\d{4}$");//
            Match match = regex.Match(phone);
            if (match.Success == false)
            {
                ViewBag.Message = "Telefon 5XX XXX XXXX biçiminde olmalıdır.";
                ViewBag.IsError = true;
                return View();
            }

            //TODO Mail Gönderme İşlemi


            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.From = new System.Net.Mail.MailAddress("alifurkangokce@gmail.com", "ali");
            mailMessage.Subject = "İletişim Formu: " + firstName + " " + lastName;
            mailMessage.To.Add("alifurkangokce@gmail.com,alifurkangokce@gmail.com");
            string body;
            body = "Ad: " + firstName + "<br />";
            body = "Soyad: " + lastName + "<br />";
            body += "Telefon: " + phone + "<br />";
            body += "E-posta: " + email + "<br />";
            body += "Depart: " + department + "<br />";
            body += "Mesaj: " + message + "<br />";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;


            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential("mail", "sifre");
            smtp.EnableSsl = true;
            smtp.Send(mailMessage);

            ViewBag.Message = "Mesajınız gönderildi. Teşekkür ederiz.";
            //TODO: Mail Gönderme işlemi yapılacak.
            ViewBag.Message = "Form başarıyla iletildi,en kısa zamanda dönüş yapacağız.";
            return View();
        }
    }
}