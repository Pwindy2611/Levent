using Levent.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Levent.Areas.Admin.Controllers
{
    public class Admin_ProductController : Controller
    {
        LeventEntities2 db = new LeventEntities2();
        // GET: Admin/Admin_Product
        public ActionResult SelectCate()
        {
            Category catels = new Category();
            catels.ListCate = db.Category.ToList<Category>();
            return PartialView(catels);

        }
        public ActionResult Product_Create(int id = 0)
        {
            Product emp = new Product();
            var lastemployee = db.Product.OrderByDescending(x => x.ID_Pro).FirstOrDefault();
            if (id != 0)
            {
                emp = db.Product.Where(x => x.ID_Pro == id).FirstOrDefault();
            }
            else if (lastemployee == null)
            {
                emp.ID_Pro = 0;
            }
            else
            {
                emp.ID_Pro = lastemployee.ID_Pro + 1;
            }
            return View(emp);
        }
        [HttpPost]
        public ActionResult Product_Create(Product pro)
        {
            try
            {
                if (pro.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(pro.UploadImage.FileName);
                    string extent = Path.GetExtension(pro.UploadImage.FileName);
                    filename += extent;
                    pro.Image_Pro = "~/Content/image/" + filename;
                    pro.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/image/"), filename));
                }
                List<Category> catels = db.Category.ToList();
                foreach (var item in catels)
                {
                    pro.Name_Cate = db.Category.Where(x => x.ID_Cate == pro.ID_Cate).Select(x => x.Name_Cate).FirstOrDefault();
                }
                db.Product.Add(pro);
                db.SaveChanges();
                return RedirectToAction("Product_Control", pro);
            }
            catch
            {
                return Content("Sai roi bro ");
            }
        }
        public ActionResult Product_Delete(int id)
        {
            return View(db.Product.Where(s => s.ID_Pro == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Product_Delete(int id, Product room)
        {
            room = db.Product.Where(s => s.ID_Pro == id).FirstOrDefault();
            db.Product.Remove(room);
            db.SaveChanges();
            return RedirectToAction("Product_Control");
        }
        public ActionResult Product_Detail(int id)
        {
            return View(db.Product.Where(s => s.ID_Pro == id).FirstOrDefault());
        }
        public ActionResult Product_Edit(int id)
        {
            return View(db.Product.Where(s => s.ID_Pro == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Product_Edit(int id, Product room)
        {
            try
            {
                if (room.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(room.UploadImage.FileName);
                    string extent = Path.GetExtension(room.UploadImage.FileName);
                    filename += extent;
                    room.Image_Pro = "~/Content/image/" + filename;
                    room.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/image/"), filename));
                }
                List<Category> catels = db.Category.ToList();
                foreach (var item in catels)
                {
                    room.Name_Cate = db.Category.Where(x => x.ID_Cate == room.ID_Cate).Select(x => x.Name_Cate).FirstOrDefault();
                }
                db.Entry(room).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Product_Control");
            }
            catch
            {
                return Content("Sai roi bro");
            }

        }
        public ActionResult Product_Control()
        {
            return View(db.Product.ToList());
        }
    }
}