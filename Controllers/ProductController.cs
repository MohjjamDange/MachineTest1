using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using MachineTest1.Models;

namespace MachineTest1.Controllers
{
    public class ProductController : Controller
    {
        MyDbContext db;
        public static int currentPage = 0;

        public ProductController() {
            
            db = new MyDbContext();
        }
        public ActionResult Category() {

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



        public ActionResult EditCategory(int id) {

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
            tblCategory t =  db.tblCategories.Find(id);
            db.tblCategories.Remove(t);
            db.SaveChanges();


            return RedirectToAction("Category");
        
        
        }




        public ActionResult EditProduct(int id)
        {
            ViewBag.categories = getAllCategories();

            tblProduct p = db.tblProducts.Find(id);
            //tblCategory c = db.tblCategories.Find(p.category_id);
            //p.tblCategory = c;
            return View(p);
        
        }
        [HttpPost]
        public ActionResult EditProduct(tblProduct p)
        {

           db.Entry<tblProduct>(p).State = System.Data.Entity.EntityState.Modified;  
            db.SaveChanges();
            
            TempData["CodeMsg"] = "Data Updated Successfully";
            return RedirectToAction("Index");

        }

        public ActionResult DeleteProduct(int id) {

            tblProduct p = db.tblProducts.Find(id);
            db.tblProducts.Remove(p);   
            db.SaveChanges();
            
            return RedirectToAction("Index");
        
        
        }



        public ActionResult AddProduct() {

            ViewBag.categories =  getAllCategories();
            return View();


        }
        [HttpPost]
        public ActionResult AddProduct(tblProduct p)
        {
            db.tblProducts.Add(p);
            db.SaveChanges();
            ModelState.Clear();
            ViewBag.categories = getAllCategories();
            ViewBag.msg = "Product Added Successfully";
            return View();
        }

        public List<SelectListItem> getAllCategories() {
            List<SelectListItem> lst = new List<SelectListItem>();

            foreach (tblCategory c in db.tblCategories)
            {
                SelectListItem s = new SelectListItem() {
                    Text = c.category_name,
                    Value = c.category_id.ToString()
                
                };

                lst.Add(s);
            
            }

            return lst;
        
        }


        public ActionResult Index(int page = 0)
        {

            if (page == -1)
            {
                page = currentPage - 1;
            }
            if (page == -2)
            {
                page = currentPage + 1;
            }
            if (page == 0)
            {
                page = 1;
                ViewBag.prev = true;
            }
            else
            {
                ViewBag.prev = false;

            }
            currentPage = page;
            ViewBag.currentpage = page;
            int pageSize = 10;
            int totalPage = 0;
            int totalRecord = 0;

            List<tblProduct> lst = GetEnquiries(page, pageSize, out totalRecord, out totalPage);
            ViewBag.dbCount = totalPage;
           ViewBag.CountRecords = totalRecord;
            if (page == 1)
            {
                ViewBag.iLow = 1;

            }
            else if (page == totalPage)
            {
                ViewBag.iLow = (10 * (page - 1)) + 1;

            }
            else
            {
                ViewBag.iLow = (page * 10) - 9;

            }
            if (page == totalPage)
            {

                int i =  lst.Count % 10;
                ViewBag.iHigh = ViewBag.iLow + (i - 1);


            }
            else
            {
                ViewBag.iHigh = page * 10;

            }

            return View(lst);
        }

        public List<tblProduct> GetEnquiries(int page, int pageSize, out int totalRecord, out int totalPage)
        {

            List<tblProduct> query = new List<tblProduct>();
            foreach(tblProduct p in db.tblProducts.ToList())
            {
                tblCategory c = db.tblCategories.Find(p.category_id);

                p.tblCategory = c;
                query.Add(p);   
            }
            totalRecord = db.tblProducts.Count();
            
            totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);
            List<tblProduct> listdata = db.tblProducts.OrderBy(a => a.product_id).Skip(((page - 1) * pageSize)).Take(pageSize).ToList();
            return listdata;
        }


    }
}