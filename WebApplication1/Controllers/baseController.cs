using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BaseController : Controller
    {


       public static DataBaseConnectionDataContext db;
        public  BaseController()
        {
            if (db == null)
            {
                db = new DataBaseConnectionDataContext();
            }
        }
             
    }
}