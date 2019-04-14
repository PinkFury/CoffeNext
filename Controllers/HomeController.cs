using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            const int startPoints = 50;
            using (UserContext db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.User_Name == model.User_Name && u.Email == model.Email);
            }
            if (user == null)
            {
                using (UserContext db = new UserContext())
                {
                    ctr++;
                    db.Users.Add(new User { User_Name = model.User_Name, Email = model.Email, Password = model.Password, PhoneNumber = model.PhoneNumber, Points = startPoints });
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
                    return RedirectToAction("Account", "Home", new { id = user.Id });
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
                return RedirectToAction("Account", "Home", new { id = idCook.Value });
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
        public ActionResult Communicate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Communicate(CommunicateModel model)
        {
            var reciverEmail = new MailAddress("forpssus@gmail.com", "Support");
            var senderEmail = new MailAddress(model.senderEmail,"Sender");
            var message = model.Message;
            var password = "bK330412";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(reciverEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, reciverEmail)
            {
                Subject = model.senderEmail,
                Body = message + Environment.NewLine + "Отправлено с:" + model.senderEmail+"." + Environment.NewLine + "Телефон:" + model.PhoneNumber
            })
            {
                smtp.Send(mess);
            }
            Response.Write("<script>alert('Успешно!') </script>");
            return View(model);
        }
        [Authorize]
        public ActionResult Feedback()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Feedback(FeedbackModel model, int id)
        {
            using(UserContext db = new UserContext())
            {
                var user = db.Users.Where(u => u.Id == id).FirstOrDefault();
                user.Points += 80;
                db.SaveChanges();
            }
            var reciverEmail = new MailAddress("forpssus@gmail.com", "Support");
            var senderEmail = new MailAddress("sad@gmail.com", "Sender");
            var message = model.Feedback;
            var password = "bK330412";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(reciverEmail.Address, password)
            };
            if (model.InstUserName != null && model.InstUserName != "") {
                using (var mess = new MailMessage(reciverEmail, reciverEmail)
                {
                    Subject = "Отзыв от @" + model.InstUserName,
                    Body = message + Environment.NewLine + "Отправлено от" + model.feedEmail + "."
                })
                {
                    smtp.Send(mess);
                }
                Response.Write("<script>alert('Успешно!') </script>");
            }
            else
            {
                using (var mess = new MailMessage(reciverEmail, reciverEmail)
                {
                    Subject = "Отзыв от " + model.feedEmail,
                    Body = message + Environment.NewLine + "Отправлено от" + model.feedEmail + "."
                })
                {
                    smtp.Send(mess);
                }
                Response.Write("<script>alert('Успешно!') </script>");
            }
            return View();
        }
        public ActionResult FeedbackPage()
        {
            return View();
        }
        public ActionResult ScoreSystem()
        {
            return View();
        }
        public ActionResult Account(int id)
        {
            User usr = db.Users.Where(u => u.Id == id).FirstOrDefault();
            ViewBag.UserName = usr.User_Name;
            ViewBag.Points = usr.Points;
            return View();
        }
    }
}