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
    }
}