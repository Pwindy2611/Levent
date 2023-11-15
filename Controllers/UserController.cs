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
        Levent_1Entities2 db = new Levent_1Entities2();
        //public ActionResult Index()
        //{
        //    return PartialView(db.User.ToList());
        //}
        public ActionResult RegisterUser(int id = 0)
        {
            id = 0;
            AdminUser emp = new AdminUser();
            var lastemployee = db.AdminUsers.OrderByDescending(x => x.ID_User).FirstOrDefault();
            if (id != 0)
            {
                emp = db.AdminUsers.Where(x => x.ID_User == id).FirstOrDefault();
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
        public ActionResult RegisterUser(AdminUser user)
        {
            var mail = user.Email_User;
            var pass = user.Password_User;
            if (ModelState.IsValid)
            {
                var check = db.AdminUsers.Where(s => s.ID_User == user.ID_User).FirstOrDefault();
                var check_mail = db.AdminUsers.Where(s => s.Email_User == user.Email_User).FirstOrDefault();

                if (check == null)// chua co id{
                {
                    if (check_mail == null)
                    {
                        //user.Password_User = GetMD5(user.Password_User);
                        db.Configuration.ValidateOnSaveEnabled = false;
                        //Session["ID"] = user.ID;
                        db.AdminUsers.Add(user);
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
            return RedirectToAction("RegisterUser", "User");


        }
        public ActionResult LoginAccount()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAccount(AdminUser user)
        {
            var check = db.AdminUsers.Where(s => s.Email_User == user.Email_User && s.Password_User == user.Password_User).FirstOrDefault();
            if (check == null)
            {
                ViewBag.LoginFail = "Dang nhap that bai";
                Session["User"] = null;
                ModelState.AddModelError("myError", "InvalidEmail or Password");
                return RedirectToAction("LoginAccount", "User");

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
               return View(db.AdminUsers.Where(x => x.ID_User == id).ToList());
        }
        public ActionResult User_Edit(int id)
        {
            return View(db.AdminUsers.Where(s => s.ID_User == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult User_Edit(int id, AdminUser user)
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
            return View(db.AdminUsers.Where(s => s.ID_User == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult User_Delete(int id, AdminUser user)
        {
            user = db.AdminUsers.Where(s => s.ID_User == id).FirstOrDefault();
            db.AdminUsers.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Logout", "User");
        }
    }
}