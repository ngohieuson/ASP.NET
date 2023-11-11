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
        
            WebBanHangEntities obj = new WebBanHangEntities();
        public ActionResult Index()
        {
            var lstCategory = obj.Category.ToList();
            return View(lstCategory);
        }
      
        public ActionResult ProductCategory(int Id)
        {

            var listProduct = obj.Product.Where(n => n.CategoryId == Id).ToList();
            return View(listProduct);
        }
    }
}