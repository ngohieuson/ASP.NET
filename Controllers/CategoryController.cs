using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.Context;

namespace ASP.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Course
        
            WebSiteBHEntities obj = new WebSiteBHEntities();
        public ActionResult Index()
        {
        var lstCate = obj.Category.ToList();

            return View(lstCate);
        }
    }
}