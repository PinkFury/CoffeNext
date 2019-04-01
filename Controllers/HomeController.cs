using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CoffeeNext.Models;

namespace CoffeeNext.Controllers
{

    public class HomeController : Controller
    {
        User user = null;
        int ctr = 2;
        UserContext db = new UserContext();
        public ActionResult Index()
        {
            ViewBag.ctr = ctr;
            return View();
        }
        [HttpGet]
        public ActionResult Menu() {
            return View();
        }
        public ActionResult Adress()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(RegisterModel model)
        {
                const int startPoints = 5;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.User_Name == model.User_Name);
                }
                if (user == null)
                {
                    using(UserContext db = new UserContext())
                    {
                        ctr++;
                        db.Users.Add(new User {User_Name = model.User_Name, Email = model.Email, Password = model.Password, PhoneNumber = model.PhoneNumber, Points = startPoints });
                        db.SaveChanges();
                        user = db.Users.Where(u => u.User_Name == model.User_Name && u.Email == model.Email && u.Password == model.Password).FirstOrDefault();
                    }
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.User_Name, true);
                        var idCook = new HttpCookie("idCook")
                        {
                            Name = "idCook",
                            Value = user.Id.ToString(),
                            Expires = DateTime.Now.AddHours(0.5)
                        };
                        HttpContext.Response.Cookies["idCook"].Value = user.Id.ToString();
                        Response.Cookies.Set(idCook);
                        return RedirectToAction("Account", "Home", new { id = user.Id});
                    }
                    else
                    {
                        Response.Write("<script>alert('Ошибка!') </script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Такой пользователь уже существует!') </script>");
                }
            return View(model);
        }
        public ActionResult Logout()
        {
            var idCook = new HttpCookie("idCook")
            {
                Name = "idCook",
                Expires = DateTime.Now.AddHours(-1)
            };
            Response.Cookies.Set(idCook);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Login(AuthModel model)
        {
            using (UserContext db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.User_Name == model.User_Name && u.Password == model.Password);
            }
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(model.User_Name, true);
                var idCook = new HttpCookie("idCook") {
                    Name = "idCook",
                    Value = user.Id.ToString(),
                    Expires = DateTime.Now.AddHours(0.5)
                };
                HttpContext.Response.Cookies["idCook"].Value = user.Id.ToString();
                Response.Cookies.Set(idCook);
                return RedirectToAction("Account", "Home", new { id = idCook.Value});
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Неправильное имя/пароль');</script>");
            }
        }
        [Authorize]
        public ActionResult About()
        {

            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize]
        public ActionResult Account(int id)
        {
            User usr = db.Users.Where(u => u.Id == id).FirstOrDefault();
            ViewBag.UserName = usr.User_Name;
            ViewBag.Points = usr.Points;
            return View();
        }
    }
}