using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ASP.Context;
namespace ASP.Controllers
{
    public class RegisterController : Controller
    {
        WebBanHangEntities obj = new WebBanHangEntities();

        // GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users _user)
        {
            if (ModelState.IsValid)
            {
                var check = obj.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check != null)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại");
                    return View(_user);
                }
                else
                {
                    _user.Password = GetMD5(_user.Password);
                    obj.Configuration.ValidateOnSaveEnabled = false;
                    obj.Users.Add(_user);
                    obj.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
            }

            ViewBag.error = "Có lỗi xảy ra trong quá trình đăng ký";
            return View(_user);
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            StringBuilder byte2String = new StringBuilder();
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String.Append(targetData[i].ToString("x2"));
            }
            return byte2String.ToString();
        }
    }
}