using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Project.Models.VM
{
    public class ProductCart
    {
        public int Prodcut_Id { get; set; }
        public string ImgUrl { get; set; }
        public int Quantity { get; set; }
        public decimal ItemTotalPrice { get; set; }
        public decimal price { get; set; }
        public string Product_Name { get; set; }
    }
}