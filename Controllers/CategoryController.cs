using MachineTest1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MachineTest1.Controllers
{
    public class CategoryController : Controller
    {
        MyDbContext db;
        public CategoryController()
        {

            db = new MyDbContext();
        }

        public ActionResult Category()
        {

            ViewData["categories"] = db.tblCategories.ToList();
            return View();

        }
        [HttpPost]
        public ActionResult Category(tblCategory c)
        {
            db.tblCategories.Add(c);
            db.SaveChanges();
            ModelState.Clear();
            ViewBag.msg = "Category Added Successfully";
            ViewData["categories"] = db.tblCategories.ToList();

            return View();
        }

        public ActionResult EditCategory(int id)
        {
            tblCategory tb = db.tblCategories.Find(id);
            ViewData["categories"] = db.tblCategories.ToList();

            return View(tb);

        }
        [HttpPost]
        public ActionResult EditCategory(tblCategory c)
        {
            db.Entry<tblCategory>(c).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Category");

        }


        public ActionResult DeleteCategory(int id)
        {
            tblCategory t = db.tblCategories.Find(id);
            db.tblCategories.Remove(t);
            db.SaveChanges();


            return RedirectToAction("Category");


        }



        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
    }
}