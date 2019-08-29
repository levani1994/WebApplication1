using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Products
    {
        [JsonProperty]
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public HttpPostedFileBase PhotoFile { get; set; }
    }
    public class ProductEdit : Products
    {
        public int ProductId { get; set; }
    }
}