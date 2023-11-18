using Levent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Levent.Areas.Admin.Controllers
{
    public class Admin_UsersController : Controller
    {
        // GET: Admin/Admin_Users
       LeventEntities2 db = new LeventEntities2();
        public ActionResult User_Control()
        {
            return View(db.User1.ToList());
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
            return RedirectToAction("User_Control");
        }
        public ActionResult User_Detail(int id)
        {
            return View(db.User1.Where(s => s.ID_User == id).FirstOrDefault());
        }
    }
}