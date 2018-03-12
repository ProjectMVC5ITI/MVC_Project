using MVC_Project.Models.VM;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        [AllowAnonymous]
        public ActionResult Index(string searchString,string currentFilter, int? page)
        {
            List<ProductVM> prodList = new List<ProductVM>();
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var prods = (from p in db.Products
                         select p);
            if (!String.IsNullOrEmpty(searchString))
            {
                prods = prods.Where(s => s.Name.Contains(searchString));
            }
            foreach (var item in prods)
            {
                var prodImg = (from i in db.ProductImages
                               where i.Product_Id == item.Product_Id
                               select i.Img_Url).FirstOrDefault();
                ProductVM prod = new ProductVM();
                prod.Product_Id = item.Product_Id;
                prod.Name = item.Name;
                prod.Price = item.Price;
                prod.Description = item.Description;
                prod.Brand = item.Brand;
                prod.ImgUrl = prodImg;
                prod.Quantity = item.Quantity;
                prod.Rate = 0;
                prodList.Add(prod);
            }
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(prodList.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult PrductDetails(ProductVM prod)
        {
            return View(prod);
        }
        
        [AllowAnonymous]
        public ActionResult NavAnonymousUser()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddToCart(ProductVM pr)
        {
            return RedirectToAction("AddFromDetails","cart",new {id=pr.Product_Id, quantity=pr.Quantity });
        }
        public ActionResult UserPView()
        {
            return PartialView();
        }

        public ActionResult NavAuthorizedUser()
        {
            return PartialView();
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