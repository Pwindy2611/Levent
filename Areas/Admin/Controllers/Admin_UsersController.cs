﻿using Levent.Models;
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
        Levent_1Entities2 db = new Levent_1Entities2();
        public ActionResult User_Control()
        {
            return View(db.AdminUsers.ToList());
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
            return RedirectToAction("User_Control");
        }
        public ActionResult User_Detail(int id)
        {
            return View(db.AdminUsers.Where(s => s.ID_User == id).FirstOrDefault());
        }
    }
}