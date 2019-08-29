using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.filter;
using WebApplication1.Models;
using WebApplication1.Models.Account;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        DataBaseConnectionDataContext db = new DataBaseConnectionDataContext();
        // GET: Home
        
        [AuthorizationFilter]
        public ActionResult Index()
        {
            //if (Session["user"] == null)
            //{
            //    return Content("acces denied!");
            //}


                var user = (User)Session["user"];
            ViewBag.name = user.Name;
            return View();
        }

        public  ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Create(Products products)
        {

            string extension = Path.GetExtension(products.PhotoFile.FileName);
            string newName = Helper.Random32();
            string longPath = Server.MapPath("/Photo/");

            products.PhotoFile.SaveAs(longPath + newName + extension);


            Product product = new Product()
            {
                Name = products.ProductName,
                Price = Convert.ToInt32(products.ProductPrice*100),
                PhotoFile = newName + extension,
                CreateDate = DateTime.Now
        };
            DataBaseConnectionDataContext db = new DataBaseConnectionDataContext();
            db.Products.InsertOnSubmit(product);
            db.SubmitChanges();

            return RedirectToAction("Catalog");
        }



        public ActionResult Catalog()
        {
            DataBaseConnectionDataContext db = new DataBaseConnectionDataContext();
            return View(db.Products);
        }

        [HttpPost]
        public JsonResult Catalog(Products products)
        {
            if (String.IsNullOrEmpty(products.ProductName) || products.ProductPrice <= 0){
                throw new Exception();
            }
            {

                var DBProduct = new Product()
                {
                    Name = products.ProductName,
                    Price = Convert.ToInt32(products.ProductPrice),
                    CreateDate = DateTime.Now
                };
                db.Products.InsertOnSubmit(DBProduct);
                db.SubmitChanges();
                return Json(DBProduct);
            }
        }

        [HttpPost]
        public JsonResult Delete(int ProductId)
        {

            db.Products.DeleteOnSubmit(db.Products.FirstOrDefault(x => x.ID == ProductId));
            
            db.SubmitChanges();
            return Json(ProductId);
        }

        [HttpPost]
        public JsonResult GetProductById(int id)
        {
            return Json(db.Products.FirstOrDefault(x => x.ID == id));
        }
        [HttpPost]
         public JsonResult Edit(ProductEdit productEdit)
        {
            var editProduct = db.Products.FirstOrDefault(x => x.ID == productEdit.ProductId);
            editProduct.Name = productEdit.ProductName;
            editProduct.Price = Convert.ToInt32(productEdit.ProductPrice);
            db.SubmitChanges();
            return Json(true);
        }

       }
    }
