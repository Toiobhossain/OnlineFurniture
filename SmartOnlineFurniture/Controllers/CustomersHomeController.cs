using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartOnlineFurniture.DataContext;
using SmartOnlineFurniture.Models;
using Vereyon.Web;
using System.IO;
using System.Net;

namespace SmartOnlineFurniture.Controllers
{
    public class CustomersHomeController : Controller
    {
        // GET: CustomersHome
        private FurnitureDbContext db = new FurnitureDbContext();
        Products aProducts = new Products();
        public ActionResult Index(string CompanyName, string CategoryName, string searching)
        {
            if (Session["CustomerName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var customerList = db.Registers.ToList();
                string cust = Session["CustomerName"].ToString();
                foreach (var customer in customerList)
                {
                    if (customer.Name == cust)
                    {
                        ViewBag.Image = customer.UserImage;

                        var products = db.Products.Include(p => p.Categories).Include(p => p.Companies);
                        return View(db.Products.Where(x => x.Companies.CompanyName.Contains(CompanyName) && x.Categories.CategoryName.Contains(CategoryName) && x.ProductName.Contains(searching) || searching == null || CompanyName == null || CategoryName == null).ToList());
                    }
                }

                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
                ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName");

                var product = db.Products.Include(p => p.Categories).Include(p => p.Companies);
                return View(db.Products.Where(x => x.ProductName.Contains(searching) || x.Companies.CompanyName.Contains(searching) || x.Categories.CategoryName.Contains(searching) || searching == null).ToList());
            }
        }
        public ActionResult DrawingOrder()
        {
            if (Session["CustomerName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var customerList = db.Registers.ToList();
                string cust = Session["CustomerName"].ToString();
                foreach (var customer in customerList)
                {
                    if (customer.Name == cust)
                    {
                        ViewBag.Image = customer.UserImage;

                        ViewBag.WoodId = new SelectList(db.Woods, "WoodId", "WoodName");
                        ViewBag.StyleId = new SelectList(db.FStyles, "StyleId", "StyleName");
                        //var woodPrice = db.Woods.ToList();
                        //ViewBag.Wood = woodPrice;

                        var takeorder = db.DrawingOrders.ToList();
                        int count = 0;
                        string notification = "";
                        foreach (var ordercount in takeorder)
                        {
                           
                            if(ordercount.Name==cust && ordercount.Status=="Confirmed")
                            {
                               notification += ordercount.Name+" Your Order "+ ordercount.Status+"\n";
                                count++;
                            }
                            else
                            {

                            }
                          
                        }
                        ViewBag.orderNotification = notification;
                        ViewBag.Count = count;
                        return View();
                    }
                 
                }
                ViewBag.WoodId = new SelectList(db.Woods, "WoodId", "WoodName");
                ViewBag.StyleId = new SelectList(db.FStyles, "StyleId", "StyleName");
                return View();

            }
        }
        [HttpPost]
        public ActionResult DrawingOrder(DrawingOrders drawingorders, HttpPostedFileBase Image)
        {
            if(ModelState.IsValid)
            {
                string path = uploadImage(Image);
                if (path.Equals("-1"))
                {

                }
                else
                {
                    drawingorders.Status = "Pending";
                    drawingorders.Image = path;
                    db.DrawingOrders.Add(drawingorders);
                    db.SaveChanges();
                    FlashMessage.Confirmation("Order Information Submission Successfull!");
                    return RedirectToAction("DrawingOrder");
                }
            }
            return RedirectToAction("DrawingOrder");
        }

        public string uploadImage(HttpPostedFileBase Image)

        {

            string path = "";

            if (Image != null && Image.ContentLength > 0)
            {
                string extension = Path.GetExtension(Image.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("/Content/image") + Path.GetFileName(Image.FileName));
                        Image.SaveAs(path);
                        path = "/Content/image" + Path.GetFileName(Image.FileName);
                    }
                    catch (Exception)
                    {
                        path = "-1";
                    }


                }
                else
                {
                    Response.Write("<script>alert('Only jpg,jpeg & png file acceptable..')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file')</script>");
                path = "-1";
            }


            return path;

        }

        public ActionResult ProducDetails(int? id)
        {
            if (Session["CustomerName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            var customerList = db.Registers.ToList();
            string cust = Session["CustomerName"].ToString();
            foreach (var customer in customerList)
            {
                if (customer.Name == cust)
                {
                    ViewBag.Image = customer.UserImage;
                    return View(products);
                }
            }

            return View(products);
        }


        public JsonResult GetWoodIDById(int woodID)
        {
            var woods = db.Woods.Where(m => m.WoodId == woodID).First();
            return Json(woods, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Cart()
        {
            if (Session["CustomerName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var customerList = db.Registers.ToList();
            string cust = Session["CustomerName"].ToString();
            foreach (var customer in customerList)
            {
                if (customer.Name == cust)
                {
                    ViewBag.Image = customer.UserImage;
                    return View();
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Cart(Prices price)
        {
            if(price.CustomerName!=null)
            {
                db.Prices.Add(price);
                db.SaveChanges();
                return RedirectToAction("PlaceToOrder");
            }
           
            return RedirectToAction("Cart");

        }
        public ActionResult AddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = db.Products.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1,


                });
                Session["cart"] = cart;

            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = db.Products.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1,


                });
                Session["cart"] = cart;
            }
        
            return RedirectToAction("Index");
        }
       

        public ActionResult Care()
        {
            if (Session["CustomerName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var customerList = db.Registers.ToList();
            string cust = Session["CustomerName"].ToString();
            foreach (var customer in customerList)
            {
                if (customer.Name == cust)
                {
                    ViewBag.Image = customer.UserImage;
                    return View();
                }
            }
            return View();
        }

        public ActionResult PlaceToOrder()
        {
            if (Session["CustomerName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var productInfo = db.Prices.ToList();
            foreach(var info in productInfo)
            {
               
                var customerinfo = db.Registers.ToList();
                string cust = Session["CustomerName"].ToString();
                foreach (var customer in customerinfo)
                {
                    if (customer.Name == cust && cust == info.CustomerName)
                    {
                        ViewBag.Image = customer.UserImage;
                        ViewBag.Customename = customer.Name;
                        ViewBag.TotalPrice = info.TotalPrice;
                        ViewBag.Quantity = info.ProductQuantity;
                        ViewBag.Address = customer.Address;
                        ViewBag.phone = customer.Phone;
                        ViewBag.pid = info.PID;
                        

                    }
                }
                
                
            }
            return View();
        }
        [HttpPost]
        public ActionResult PlaceToOrder(Orders order)
        {
            if(ModelState.IsValid)
            {
                var product = db.Products.ToList();
                foreach (var pid in product)
                {
                    if(pid.ProductId==order.ProductID)
                    {
                        pid.Quantity = pid.Quantity - 1;
                        db.Orders.Add(order);
                        db.SaveChanges();
                        FlashMessage.Confirmation("Ordered Confirmed Successfull!");
                        return RedirectToAction("PlaceToOrder");
                    }
                    
                }
                
            }
            return View();
        }

    }
}