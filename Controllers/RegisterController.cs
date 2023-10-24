using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.Context;
namespace ASP.Controllers
{
    public class RegisterController : Controller
    {
        WebSiteBHEntities obj = new WebSiteBHEntities();
        
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Users user)
        {
            Users lst = new Users();
            lst.FirstName = user.FirstName;
            lst.LastName = user.LastName;
            lst.Password = user.Password;
            lst.Email = user.Email;
            lst.IsAdmin = user.IsAdmin;
            var list = obj.Users.Add(lst);
            obj.SaveChanges();
            return View(list);
        }
    }
}