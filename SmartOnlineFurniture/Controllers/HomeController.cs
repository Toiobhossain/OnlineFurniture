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
    public class HomeController : Controller
    {
        // GET: Home
        private FurnitureDbContext db = new FurnitureDbContext();
        Registers aRegister = new Registers();
        Products aProducts = new Products();

        public ActionResult Index(string CompanyName,string CategoryName,string searching)
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName");

            var products = db.Products.Include(p => p.Categories).Include(p => p.Companies);
            return View(db.Products.Where(x=>x.Companies.CompanyName.Contains(CompanyName)&& x.Categories.CategoryName.Contains(CategoryName)&&x.ProductName.Contains(searching)||searching==null||CompanyName==null||CategoryName==null).ToList());
        }

        public ActionResult UserRegistration()
        {
            var list = new List<string>() { "Customer", "ShopOwner" };
            ViewBag.list = list;

            return View();
        }
        [HttpPost]
        public ActionResult UserRegistration(Registers register,HttpPostedFileBase UserImage)
        {
            var list = new List<string>() { "Customer", "ShopOwner" };
            ViewBag.list = list;
            if (ModelState.IsValid)
            {
                string path = uploadImage(UserImage);
                if (path.Equals("-1"))
                {

                }
                else
                {
                    register.UserImage = path;
                    db.Registers.Add(register);
                    db.SaveChanges();
                    FlashMessage.Confirmation("Registration successfully");
                    return RedirectToAction("UserRegistration");
                }
                

            }
            return View();
        }

        public string uploadImage(HttpPostedFileBase UserImage)

        {

            string path = "";

            if (UserImage != null && UserImage.ContentLength > 0)
            {
                string extension = Path.GetExtension(UserImage.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("/Content/image")+ Path.GetFileName(UserImage.FileName));
                        UserImage.SaveAs(path);
                        path = "/Content/image" + Path.GetFileName(UserImage.FileName);
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
        public JsonResult IsEmailExists(string Email)
        {
            aRegister.Email= Email.Trim();
            var em = db.Registers.ToList();
            if (!em.Any(cate => cate.Email.ToLower() == aRegister.Email.ToLower()))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Registers registers)
        {
            var registerDetails1 = db.Registers.Where(x => x.Name == registers.Name && x.Password == registers.Password && x.UserType=="Customer").FirstOrDefault();
            var registerDetails2 = db.Registers.Where(x => x.Name == registers.Name && x.Password == registers.Password && x.UserType == "ShopOwner").FirstOrDefault();
            var registerDetails3 = db.Registers.Where(x => x.Name == registers.Name && x.Password == registers.Password && x.UserType == "Admin").FirstOrDefault();
            if (registerDetails1!=null)
            {
                Session["CustomerName"] = registers.Name;
                return RedirectToAction("Index", "CustomersHome");
            }
            if (registerDetails2 != null)
            {
                Session["name"] = registers.Name;
                return RedirectToAction("Index", "ShopOwner");
            }
            if (registerDetails3 != null)
            {
                Session["userName"] = registers.Name;
                return RedirectToAction("Index", "Admin");
            }
            FlashMessage.Confirmation("UserName & Password Doesn't Match!");
            return View();
        }

        public ActionResult ProducDetails(int? id)
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
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CustomerCare()
        {
            return View();
        }

     
      
    }
}