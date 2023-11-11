using ASP.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ASP.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        WebBanHangEntities obj = new WebBanHangEntities();
        // GET: Admin/Category
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstCategory = new List<Category>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstCategory = obj.Category.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstCategory = obj.Category.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            lstCategory = lstCategory.OrderByDescending(n => n.Id).ToList();
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        public ActionResult Create()
        {
          
            return View();
        }

        [ValidateInput(false)]

        [HttpPost]
        public ActionResult Create(Category objCategory)

        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (objCategory.ImageUpLoad != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpLoad.FileName);
                        //ten hinh
                        string extension = Path.GetExtension(objCategory.ImageUpLoad.FileName);
                        //jpg
                        fileName = fileName + extension;
                        //ten hinh.jpg
                        objCategory.Avatar = fileName;
                        objCategory.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                    }
                    objCategory.CreatedOnUtc = DateTime.Now;
                    obj.Category.Add(objCategory);
                    obj.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(objCategory);

                }

            }

            return View(objCategory);
        }




        [HttpGet]
        public ActionResult Details(int id)
        {
            var objCategory = obj.Category.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = obj.Category.Where(n => n.Id == id).FirstOrDefault();

            return View(objCategory);
        }

        [HttpPost]
        public ActionResult Delete(Category objPro)
        {
            var objCategory = obj.Category.Where(n => n.Id == objPro.Id).FirstOrDefault();
            obj.Category.Remove(objCategory);
            obj.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
           
            var objCategory = obj.Category.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category objCategory, FormCollection form)
        {

            if (objCategory.ImageUpLoad != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpLoad.FileName);
                //tenhinh
                string extension = Path.GetExtension(objCategory.ImageUpLoad.FileName);
                //png
                fileName = fileName + extension;
                //tenhinh.png
                objCategory.Avatar = fileName;
                objCategory.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));

            }
            else
            {
                objCategory.Avatar = form["oldimage"];
                obj.Entry(objCategory).State = EntityState.Modified;
                obj.SaveChanges();
                return RedirectToAction("Index");
            }
            obj.Entry(objCategory).State = EntityState.Modified;
            obj.SaveChanges();
            return RedirectToAction("Index");
        }
        


    }
}