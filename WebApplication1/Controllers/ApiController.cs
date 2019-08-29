using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ApiController : Controller
    {
        DataBaseConnectionDataContext db = new DataBaseConnectionDataContext();

        // GET: Api
        public JsonResult GetAllProducts()
        {
            var products = db.Products.ToList();
            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}