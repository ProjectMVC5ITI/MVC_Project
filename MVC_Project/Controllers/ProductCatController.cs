using MVC_Project.Models.VM;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class ProductCatController : Controller
    {
        Entities1 db = new Entities1();
        // GET: ProductCat
        public ActionResult MobileAndTabs(string searchString, string currentFilter, int? page)
        {
            List<ProductVM> prodList = new List<ProductVM>();
            prodList = cateogryList(page, searchString,currentFilter,4);
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(prodList.ToPagedList(pageNumber, pageSize));
           
        }
        public ActionResult Computers(string searchString, string currentFilter, int? page)
        {
            List<ProductVM> prodList = new List<ProductVM>();
            prodList = cateogryList(page, searchString, currentFilter, 6);
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(prodList.ToPagedList(pageNumber, pageSize));

        }
        List<ProductVM> cateogryList(int?page,string searchString,string currentFilter,int id)
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
                         where p.Cat_Id == id
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
            return prodList;
        }

    }
}