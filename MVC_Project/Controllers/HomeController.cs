using MVC_Project.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index()
        {
            List<ProductVM> prodList = new List<ProductVM>();
            var prods = (from p in db.Products
                         select p);
            
            foreach (var item in prods)
            {
                var prodImg = (from i in db.ProductImages
                               where i.Product_Id == item.Product_Id
                               select i.Img_Url).First();
                ProductVM prod = new ProductVM();
                prod.Product_Id = item.Product_Id;
                prod.Name = item.Name;
                prod.Price = item.Price;
                prod.ImgUrl = prodImg;
                prod.Rate = 0;

                prodList.Add(prod);
            }
            return View(prodList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}