using MVC_Project.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
     
    public class CartController : Controller
    {
        Entities1 db = new Entities1();
        // GET: Cart
        public ActionResult Index()
        {
            User user = (User)Session["user"];
            var cart = (from c in db.Carts
                        where c.User_Id == user.U_Id
                        select c);
            List<ProductCart> prodsCart = new List<ProductCart>();
            if (user == null)
                return RedirectToAction("Index","Home"); ;
            foreach (var item in cart)
            {
                var prods = (from p in db.Products
                                where p.Product_Id == item.Product_Id
                                select p).FirstOrDefault();
                var imgUrl= (from p in db.ProductImages
                             where p.Product_Id == item.Product_Id
                             select p.Img_Url).FirstOrDefault();
                ProductCart prod = new ProductCart();
                prod.ImgUrl = imgUrl;
                prod.Prodcut_Id = item.Product_Id;
                prod.Product_Name = prods.Name;
                prod.Quantity = item.Quantity;
                prod.ItemTotalPrice = prods.Price * item.Quantity;
                prod.price = prods.Price;

                prodsCart.Add(prod);
            }
            return View(prodsCart);
        }
      
        public void Add(int id)
        {
            User user = (User)Session["user"];
            var cart = (from c in db.Carts
                        where c.Product_Id == id&&c.User_Id==user.U_Id
                        select c).FirstOrDefault();
            if(cart==null)
            {
                Cart cart1 = new Cart();
                cart1.Product_Id = id;
                cart1.Quantity = 1;
                cart1.User_Id = user.U_Id;
                db.Carts.Add(cart1);
                db.SaveChanges();
            }else
            {
                cart.Quantity++;
                db.SaveChanges();
            }
            //return RedirectToAction("Index");
        }
        public ActionResult AddFromDetails(int id,int quantity)
        {
            User user = (User)Session["user"];
            var cart = (from c in db.Carts
                        where c.Product_Id == id && c.User_Id == user.U_Id
                        select c).FirstOrDefault();
            if (cart == null)
            {
                Cart cart1 = new Cart();
                cart1.Product_Id = id;
                cart1.Quantity = quantity;
                cart1.User_Id = user.U_Id;
                db.Carts.Add(cart1);
                db.SaveChanges();
            }
            else
            {
                cart.Quantity+= quantity;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Remove(int id)
        {
            var cart = (from c in db.Carts
                        where c.Product_Id == id
                        select c).FirstOrDefault();
            db.Carts.Remove(cart);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}