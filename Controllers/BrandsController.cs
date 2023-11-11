using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.Context;
namespace ASP.Controllers
{
    public class BrandsController : Controller
    {
        WebBanHangEntities obj = new WebBanHangEntities();
        // GET: Brands
        public ActionResult Index()
        {
            var lstBrand = obj.Brands.ToList();
            return View(lstBrand);
           
        }
    }
}