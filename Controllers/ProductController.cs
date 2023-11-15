using Levent.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Levent.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        Levent_1Entities2 db = new Levent_1Entities2();
        public ActionResult Product_Index_Main()
        {
            return PartialView(db.Products.ToList());
        }
        public ActionResult Products_Detail(int id)
        {
            return View(db.Products.Where(s => s.ID_Pro == id).FirstOrDefault());
        }
    }
    
}