using Levent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Levent.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        private LeventEntities2 db = new LeventEntities2();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Category_Index(string name)
        {
            var item = db.Category.Where(s => s.Name_Cate.Contains(name)).ToList();
            if (name == null)
            {
                return PartialView("Category_Index", db.Category.ToList());
            }
            else
            {
                return PartialView("Category_Index", db.Category.Where(s => s.Name_Cate.Contains(name)).ToList());
            }
        }
    }
}