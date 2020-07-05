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
    public class ProductsController : Controller
    {
        private FurnitureDbContext db = new FurnitureDbContext();
        Products aProducts = new Products();

        // GET: Products
        public ActionResult Index()
        {
            if (Session["name"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var products = db.Products.Include(p => p.Categories).Include(p => p.Companies);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            if (Session["name"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,CompanyId,CategoryId,ProductName,Price,Quantity,ProductDetails,ProductImage")] Products products, HttpPostedFileBase ProductImage)
        {
            if (ModelState.IsValid)
            {
                string path = uploadImage(ProductImage);
                if (path.Equals("-1"))
                {

                }
                else
                {
                    products.ProductImage = path;
                    db.Products.Add(products);
                    db.SaveChanges();
                    FlashMessage.Confirmation("Product saved successfully");
                    return RedirectToAction("Create");
                }
               
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", products.CategoryId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", products.CompanyId);
            return View(products);
        }

        public JsonResult IsProductNameExist(string ProductName)
        {
            aProducts.ProductName = ProductName.Trim();
            var PName= db.Products.ToList();
            if (!PName.Any(comp => comp.ProductName.ToLower() == aProducts.ProductName.ToLower()))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public string uploadImage(HttpPostedFileBase ProductImage)

        {

            string path = "";

            if (ProductImage != null && ProductImage.ContentLength > 0)
            {
                string extension = Path.GetExtension(ProductImage.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("/Content/productimage") + Path.GetFileName(ProductImage.FileName));
                        ProductImage.SaveAs(path);
                        path = "/Content/productimage" + Path.GetFileName(ProductImage.FileName);
                    }
                    catch (Exception ex)
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
        // GET: Products/Edit/5
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
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", products.CategoryId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", products.CompanyId);
            return View(products);
        }
       

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,CompanyId,CategoryId,ProductName,Price,Quantity,ProductDetails,ProductImage")] Products products, HttpPostedFileBase ProductImage)
        {
            if (ModelState.IsValid)
            {
                string path = uploadImage(ProductImage);
                if (path.Equals("-1"))
                {
                    db.Entry(products).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    products.ProductImage = path;
                    db.Entry(products).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", products.CategoryId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", products.CompanyId);
            return View(products);
        }
        public ActionResult StockProduct()
        {
            if (Session["name"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            return View();
        }
        [HttpPost]

        public ActionResult StockProduct(int CompanyId,int CategoryId,int ProductId,int Quantity)
        {
            if (ModelState.IsValid)
            {
                var productlist = db.Products.ToList();
                foreach(var product in productlist)
                {
                    if(product.CompanyId==CompanyId&&product.CategoryId==CategoryId&&product.ProductId==ProductId)
                    {
                        int quantity = product.Quantity + Quantity;
                        product.Quantity = quantity;
                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();
                        FlashMessage.Confirmation("Stock Added successfully");
                        return RedirectToAction("StockProduct");
                    }
                }
                
               
                
            }
            return RedirectToAction("StockProduct");
        }

        public JsonResult GetCategoryByCompanyId(int CompanyId)
        {
            var category = db.Products.Where(m => m.CompanyId == CompanyId).ToList();
            return Json(category, JsonRequestBehavior.AllowGet);
        }
        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["name"] == null)
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
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CheckStock()
        {
            if (Session["name"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var products = db.Products.Include(p => p.Categories).Include(p => p.Companies);
            return View(products.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
