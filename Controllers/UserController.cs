using Levent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Levent.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        LeventEntities2 db = new LeventEntities2();
        //public ActionResult Index()
        //{
        //    return PartialView(db.User.ToList());
        //}
        public ActionResult RegisterUser(int id = 0)
        {
            id = 0;
            User1 emp = new User1();
            var lastemployee = db.User1.OrderByDescending(x => x.ID_User).FirstOrDefault();
            if (id != 0)
            {
                emp = db.User1.Where(x => x.ID_User == id).FirstOrDefault();
            }
            else if (lastemployee == null)
            {
                emp.ID_User = 0;
            }
            else
            {
                emp.ID_User = lastemployee.ID_User + 1;
            }
            return PartialView(emp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(User1 user)
        {
            var mail = user.Email_User;
            var pass = user.Password_User;
            if (ModelState.IsValid)
            {
                var check = db.User1.SingleOrDefault(s => s.ID_User == user.ID_User && s.Email_User.Equals(mail));
                var check_mail = db.User1.Where(s => s.Email_User == user.Email_User).FirstOrDefault();

                if (check == null)// chua co id{
                {
                    if (check_mail == null)
                    {
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.User1.Add(user);
                        db.SaveChanges();
                        return RedirectToAction("LoginAccount", "User");
                    }
                }
                else
                {
                    ViewBag.ErrorRegister = "Dang ky that bai";
                    return RedirectToAction("RegisterUser", "User");
                }
            }
            return View(user);


        }
        public ActionResult LoginAccount()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAccount(User1 user)
        {
            var check = db.User1.Where(s => s.Email_User == user.Email_User && s.Password_User == user.Password_User).FirstOrDefault();
            if (check == null)
            {
                ViewBag.LoginFail = "Dang nhap that bai";
                Session["User"] = null;
                ModelState.AddModelError("myError", "InvalidEmail or Password");
                return View(user);

            }
            else
            {
                Session["User"] = check;
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["ID"] = user.ID_User;
                Session["PasswordUser"] = user.Password_User;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult HomeUser(int id=0)
        {
            id = 0;
               return View(db.User1.Where(x => x.ID_User == id).ToList());
        }
        public ActionResult User_Edit(int id)
        {
            return View(db.User1.Where(s => s.ID_User == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult User_Edit(int id, User1 user)
        {
            try
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HomeUser", "User");
            }
            catch
            {
                return Content("Sai roi bro");
            }
        }
        public ActionResult User_Delete(int id)
        {
            return View(db.User1.Where(s => s.ID_User == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult User_Delete(int id, User1 user)
        {
            user = db.User1.Where(s => s.ID_User == id).FirstOrDefault();
            db.User1.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Logout", "User");
        }
    }
}