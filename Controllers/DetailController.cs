using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.Context;
namespace ASP.Controllers
{
    public class DetailController : Controller
    {
        // GET: Detail
        WebSiteBHEntities obj = new WebSiteBHEntities();
        public ActionResult Detail(int id)
        {
            var sp = obj.Product.Where(n => n.Id==id).FirstOrDefault();


            return View(sp);
        }
    }
}