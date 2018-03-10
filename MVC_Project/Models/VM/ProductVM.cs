using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Project.Models.VM
{
    public class ProductVM
    {
        public int Product_Id {get; set;}
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public string Brand { get; set; }
        public int Rate { get; set; }
    }
}