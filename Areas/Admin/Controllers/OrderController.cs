using ASP.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        WebBanHangEntities obj = new WebBanHangEntities();
        public ActionResult Index()
        {
            var listOrder = obj.Order.ToList();
            return View(listOrder);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var order = obj.Order.Where(n => n.Id == id).FirstOrDefault();
            return View(order);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var order = obj.Order.SingleOrDefault(n => n.Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(Order updatedOrder)
        {
            var order = obj.Order.SingleOrDefault(n => n.Id == updatedOrder.Id);
            if (order == null)
            {
                return HttpNotFound();
            }

            // Update properties of the order with the new values
            order.Name = updatedOrder.Name;
            order.UserId = updatedOrder.UserId;
            order.Status = updatedOrder.Status;
            order.CreatedOnUtc = updatedOrder.CreatedOnUtc;

            obj.SaveChanges();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Delete(int Id)
        {

            var objorder = obj.Order.Where(n => n.Id == Id).FirstOrDefault();

            return View(objorder);
        }
        [HttpPost]
        public ActionResult Delete(Order objor)
        {

            var objorder = obj.Order.Where(n => n.Id == objor.Id).FirstOrDefault();
            obj.Order.Remove(objorder);
            obj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}