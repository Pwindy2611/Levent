using Levent.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Levent.Areas.Admin.Controllers
{
    public class Admin_CategoryController : Controller
    {
        // GET: Admin/Admin_Category
        LeventEntities2 db = new LeventEntities2();
        public ActionResult Category_Control()
        {
            return View(db.Category.ToList());
        }
        public ActionResult Category_Create(int id = 0)
        {

            Category emp = new Category();
            var lastemployee = db.Category.OrderByDescending(x => x.ID_Cate).FirstOrDefault();
            if (id != 0)
            {
                emp = db.Category.Where(x => x.ID_Cate == id).FirstOrDefault();
            }
            else if (lastemployee == null)
            {
                emp.ID_Cate = 0;
            }
            else
            {
                emp.ID_Cate = lastemployee.ID_Cate + 1;
            }
            return View(emp);
        }
        [HttpPost]
        public ActionResult Category_Create(Category pro)
        {
         db.Category.Add(pro);
         db.SaveChanges();
         return RedirectToAction("Category_Control", pro);
        }
        public ActionResult Category_Delete(int id)
        {
            return View(db.Category.Where(s => s.ID_Cate == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Category_Delete(int id, Category cate)
        {
            cate = db.Category.Where(s => s.ID_Cate == id).FirstOrDefault();
            db.Category.Remove(cate);
            db.SaveChanges();
            return RedirectToAction("Category_Control");
        }
        public ActionResult Category_Edit(int id)
        {
            return View(db.Category.Where(s => s.ID_Cate == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Category_Edit(Category cate)
        {
                db.Entry(cate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Category_Control");
        }
    }
}