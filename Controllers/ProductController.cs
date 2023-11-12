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
        Levent_1Entities db = new Levent_1Entities();
        public ActionResult Product_Index_Main()
        {
            return PartialView(db.Product.ToList());
        }
    }
}