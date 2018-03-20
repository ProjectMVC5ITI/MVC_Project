using MVC_Project.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class OrderController : Controller
    {
        Entities1 context = new Entities1();
        // GET: Order
        public ActionResult Index()
        {
            User user = (User)Session["user"];
            int userId = user.U_Id;
            Entities1 context = new Entities1();
            var query =
                (from o in context.Orders
                 where o.User_Id == userId
                 select o).ToList();



            return View(query);
        }
        public ActionResult Add()
        {
            User user = (User)Session["user"];
            int userId = user.U_Id;

            var query =
                (from o in context.Carts
                 where o.User_Id == userId
                 select new { o.Product_Id, o.Product.Price, o.Quantity, o.Product }).ToList();
            decimal totalPrice = 0;
            if (query.Count > 0)
            {
                foreach (var item in query)
                {
                    totalPrice += item.Price * item.Quantity;
                }
            }
            //  add new order
            Order newOrder = new Order();
            newOrder.User_Id = userId;
            newOrder.Total_Price = totalPrice;
            newOrder.Sales_Date = DateTime.Now;
            context.Orders.Add(newOrder);
            context.SaveChanges();

            var oId =
                (from o in context.Orders
                 select o.O_Id).Max();
            
            //  add order details
            foreach (var item in query)
            {
                order_details odetails = new order_details();
                odetails.O_Id = oId;
                odetails.p_id = item.Product_Id;
                odetails.Quantity = item.Quantity;
                odetails.Price = (int)item.Product.Price;

                context.order_details.Add(odetails);
                updateQuantity(item.Product_Id, item.Quantity);
            }
            context.SaveChanges();
            return RedirectToAction("Index");
            //return View();
        }
        public ActionResult Details(int id)
        {
            Entities1 context = new Entities1();
            var query =
                (from d in context.order_details
                 where d.O_Id == id
                 select d).ToList();

            
            List<ProductCart> products = new List<ProductCart>();
            foreach (var item in query)
            {
                ProductCart product = new ProductCart();
                product.ImgUrl = (from i in context.ProductImages where i.Product_Id == item.p_id select i.Img_Url).FirstOrDefault();
                product.Product_Name = item.Product.Name;
                product.price = item.Product.Price;
                product.Quantity = item.Quantity;
                product.ItemTotalPrice = item.Order.Total_Price;
                products.Add(product);
            }


            return View(products);
        }

        public void updateQuantity(int pId, int qty)
        {
            Entities1 context = new Entities1();
            var query =
                (from p in context.Products
                 where p.Product_Id == pId
                 select new { p, p.Quantity }).FirstOrDefault();

            int newQty = query.Quantity - qty;
            query.p.Quantity = newQty;
            context.SaveChanges();


        }

    }
}