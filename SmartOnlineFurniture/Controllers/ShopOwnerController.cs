using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartOnlineFurniture.DataContext;
using SmartOnlineFurniture.Models;
using Vereyon.Web;
using System.IO;

namespace SmartOnlineFurniture.Controllers
{
    public class ShopOwnerController : Controller
    {
        // GET: ShopOwner
        private FurnitureDbContext db = new FurnitureDbContext();
        public ActionResult Index()
        {
            if (Session["name"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public ActionResult TakeDrawingOrder()
        {
            if (Session["name"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            //var orders = db.DrawingOrders.ToList();
            //foreach(var item in orders)
            //{
            //    if (item.Status == false)
            //    {
            //        ViewBag.status = "Pending";
                   

            //    }
            //   else
            //    {
            //        ViewBag.status = "Confirmed";
                  
            //    }

            //    var drawing2 = db.DrawingOrders.Include(p => p.woods).Include(p => p.fStyles);
            //    return View(drawing2.ToList());

            //}

            var drawing = db.DrawingOrders.Include(p => p.woods).Include(p => p.fStyles);
            return View(drawing.ToList());
        }
        //[HttpPost]
        //public ActionResult TakeDrawingOrder(bool status)
        //{
        //    var order = db.DrawingOrders.ToList();
        //    foreach(var item in order)
        //    {
        //        if(item.Status==false)
        //        {
        //            item.Status = true;
        //            db.Entry(order).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("TakeDrawingOrder");
        //        }
        //    }


        //    return RedirectToAction("TakeDrawingOrder");
        //}

        public ActionResult Edit(int? id)
        {
            if (Session["name"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrawingOrders orders = db.DrawingOrders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.WoodId = new SelectList(db.Woods, "WoodId", "WoodName", orders.WoodId);
            ViewBag.StyleId = new SelectList(db.FStyles, "StyleId", "StyleName", orders.StyleId);
            return View(orders);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DrawingId,WoodId,StyleId,Height,Width,Quantity,Price,Details,Name,Image")] DrawingOrders orders)
        {
            if (ModelState.IsValid)
            {

                    orders.Status = "Confirmed";
                    db.Entry(orders).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("TakeDrawingOrder");
                
            }
            ViewBag.WoodId = new SelectList(db.Woods, "WoodId", "WoodName", orders.WoodId);
            ViewBag.StyleId = new SelectList(db.FStyles, "StyleId", "StyleName", orders.StyleId);
            return View(orders);
        }
    }
}