using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SmartOnlineFurniture.DataContext;
using SmartOnlineFurniture.Models;
using Vereyon.Web;
using System.Net;
using System.Data.Entity;

namespace SmartOnlineFurniture.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        private FurnitureDbContext db = new FurnitureDbContext();
        Companies aCompany = new Companies();
        Categories aCategory = new Categories();
       

        public ActionResult Index()
        {
            if (Session["userName"] ==null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public ActionResult Create()
        {
            if (Session["userName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Companies company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.SaveChanges();
                FlashMessage.Confirmation("Company saved successfully");
                return RedirectToAction("Create");

            }
            return View();
        }
        public JsonResult IsCompanyNameExist(string CompanyName)
        {
            aCompany.CompanyName = CompanyName.Trim();
            var company = db.Companies.ToList();
            if (!company.Any(comp => comp.CompanyName.ToLower() == aCompany.CompanyName.ToLower()))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CategoriesSave()
        {
            if (Session["userName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CategoriesSave(Categories category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                FlashMessage.Confirmation("Category saved successfully");
                return RedirectToAction("CategoriesSave");

            }
            return View();
        }
        public JsonResult IsCategoryExist(string CategoryName)
        {
            aCategory.CategoryName = CategoryName.Trim();
            var category = db.Categories.ToList();
            if (!category.Any(cate => cate.CategoryName.ToLower() ==aCategory.CategoryName.ToLower()))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewCompany()
        {
            if (Session["userName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(db.Companies.ToList());
            
        }

        public ActionResult Edit(int? id)
        {
            if (Session["userName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Companies companies = db.Companies.Find(id);
            if (companies == null)
            {
                return HttpNotFound();
            }
            return View(companies);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyId,CompanyName")] Companies companies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewCompany");
            }
            return View(companies);
        }

        public ActionResult Delete(int? id)
        {
            if (Session["userName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Companies companies = db.Companies.Find(id);
            if (companies == null)
            {
                return HttpNotFound();
            }
            return View(companies);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Companies companies = db.Companies.Find(id);
            db.Companies.Remove(companies);
            db.SaveChanges();
            return RedirectToAction("ViewCompany");
        }

        public ActionResult ViewCategory()
        {
            if (Session["userName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(db.Categories.ToList());
        }

        public ActionResult EditCategory(int? id)
        {
            if (Session["userName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if(categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory([Bind(Include = "CategoryId ,CategoryName")] Categories categories)
        {
            if(ModelState.IsValid)
            {
                db.Entry(categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewCategory");
            }
            return View(categories);

        }

        public ActionResult DeleteCategory(int? id)
        {
            if (Session["userName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if(categories==null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }
        [HttpPost,ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int id)
        {
            Categories categories = db.Categories.Find(id);
            db.Categories.Remove(categories);
            db.SaveChanges();
            return RedirectToAction("ViewCategory");

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