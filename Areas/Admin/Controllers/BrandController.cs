﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.Context;
using PagedList;

namespace ASP.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        WebBanHangEntities obj = new WebBanHangEntities();
        // GET: Admin/Brand
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstBrand = new List<Brands>();
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
                lstBrand = obj.Brands.Where(n => n.Name.Contains(SearchString)).ToList();

                //lstBrand = obj.Brands.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstBrand = obj.Brands.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstBrand = lstBrand.OrderByDescending(n => n.Id).ToList();
            return View(lstBrand.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }

        [ValidateInput(false)]

        [HttpPost]
        public ActionResult Create(Brands objBrand)

        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (objBrand.ImageUpLoad != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpLoad.FileName);
                        //ten hinh
                        string extension = Path.GetExtension(objBrand.ImageUpLoad.FileName);
                        //jpg
                        fileName = fileName + extension;
                        //ten hinh.jpg
                        objBrand.Avatar = fileName;
                        objBrand.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                    }
                    objBrand.CreatedOnUtc = DateTime.Now;
                    obj.Brands.Add(objBrand);
                    obj.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(objBrand);

                }

            }

            return View(objBrand);
        }


        [HttpGet]
        public ActionResult Details(int id)
        {
            var objBrand = obj.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = obj.Brands.Where(n => n.Id == id).FirstOrDefault();

            return View(objBrand);
        }

        [HttpPost]
        public ActionResult Delete(Brands objPro)
        {
            var objBrand = obj.Brands.Where(n => n.Id == objPro.Id).FirstOrDefault();
            obj.Brands.Remove(objBrand);
            obj.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            
            var objBrand = obj.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brands objBrand, FormCollection form)
        {

            if (objBrand.ImageUpLoad != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpLoad.FileName);
                //tenhinh
                string extension = Path.GetExtension(objBrand.ImageUpLoad.FileName);
                //png
                fileName = fileName + extension;
                //tenhinh.png
                objBrand.Avatar = fileName;
                objBrand.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));

            }
            else
            {
                objBrand.Avatar = form["oldimage"];
                obj.Entry(objBrand).State = EntityState.Modified;
                obj.SaveChanges();
                return RedirectToAction("Index");
            }
            obj.Entry(objBrand).State = EntityState.Modified;
            obj.SaveChanges();
            return RedirectToAction("Index");
        }
       
    }
}