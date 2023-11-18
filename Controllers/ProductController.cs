using Levent.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Web.UI;
using System.Data.Entity;

namespace Levent.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        LeventEntities2 db = new LeventEntities2();
        public ActionResult Product_Index_Main()
        {
            return PartialView(db.Product.ToList());
        }
        public ActionResult Products_Detail(int id)
        {
            var x = db.Product.Where(s => s.ID_Pro == id).FirstOrDefault();
            if (x == null)
            {
                Session["event"] = null;
            }
            else
            {
                Session["event"] = x;
            }
            return View(db.Product.Where(s => s.ID_Pro == id).FirstOrDefault());
        }
        public ActionResult Product_Index_Store(int? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var list = db.Product.ToList();
            return View(list.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Product_Index_ByCate(int cateid)
        {
                var productlist = db.Product.OrderByDescending(x => x.ID_Cate).Where(x => x.ID_Cate == cateid);
                return View(productlist);
        }
        public ActionResult SearchOption(double min = double.MinValue ,double max = double.MaxValue)
        {
            var list = db.Product.Where(p => (double)p.Price_pro >= min && (double)p.Price_pro <= max).ToList();
            return View(list);
        }
        public ActionResult MintoMax()
        {
            var products = from p in db.Product
                           orderby p.Price_pro ascending
                           select p;
            return View(products);
        }

        public ActionResult MaxtoMin()
        {
            var products = from p in db.Product
                           orderby p.Price_pro descending
                           select p;
            return View(products);
        }
    }
}