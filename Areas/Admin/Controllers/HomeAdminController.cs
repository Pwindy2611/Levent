using Levent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Levent.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        [Route("")]
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }
        LeventEntities2 db = new LeventEntities2();
        public ActionResult LoginAdmin()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAdmin(Admin1 user)
        {
            var check = db.Admin1.Where(s => s.Email == user.Email && s.Password == user.Password).FirstOrDefault();
            if (check == null)
            {
                ViewBag.LoginFail = "Dang nhap that bai";
                ModelState.AddModelError("myError", "InvalidEmail or Password");
                return RedirectToAction("LoginAdmin", "HomeAdmin");

            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["ID"] = user.ID;
                Session["PasswordUser"] = user.Password;
                db.SaveChanges();
                return RedirectToAction("Index", "HomeAdmin");
            }
        }
    }
}