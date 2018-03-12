using MVC_Project.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
     [AllowAnonymous]
    public class AccountController : Controller
    {
        Entities context = new Entities();
        // GET: Account
        public ActionResult login()
        {
            return View(new UserLoginVM());
        }

        [HttpPost]
        public ActionResult Login(UserLoginVM newuser)
        {
            if (ModelState.IsValid)
            {
                bool found = context.Users.Any(u => u.Emial == newuser.Email && u.Password == newuser.Password);
                if (found == false)//name found
                {
                    //Custom error validation ==validationsummery

                    ModelState.AddModelError(string.Empty, "Email or Password Invalid");
                    return View(newuser);
                }
                else
                {
                    var user = context.Users.Where(u => u.Emial == newuser.Email && u.Password == newuser.Password).Select(u => u).FirstOrDefault();
                    saveUser(user);
                    Session["user"] = user;
                    return RedirectToAction("Index", "home");
                }


            }
            return View();
        }

        // GET: Account
        public ActionResult Register()
        {

            return View(new User());
        }
        [HttpPost]
        public ActionResult Register(User newuser)
        {
            //check valid object
            //save db
            //create OWIN  Context==>
            //call Cookie Middleware
            //signin 
            if (ModelState.IsValid)
            {
                saveUser(newuser);
                return RedirectToAction("Index", "home");
            }
            return View(newuser);
        }
        private void saveUser(User newuser)
        {
            bool found = context.Users.Any(u => u.Emial == newuser.Emial && u.Password == newuser.Password);
            if (found == false)//name found
            {
                context.Users.Add(newuser);
                context.SaveChanges();
            }

            var identity = new ClaimsIdentity(
                new List<Claim>() {
                        new Claim(ClaimTypes.Email, newuser.Emial),
                        new Claim("Name", newuser.User_Name),
                        new Claim(ClaimTypes.NameIdentifier, newuser.U_Id.ToString()),},
                "ApplicationCookie");


            //get context OWIN
            var ctx = Request.GetOwinContext();
            //Auth Midd From OWIN
            var authManager = ctx.Authentication;
            //User Authorizate 
            authManager.SignIn(identity);
        }
        public ActionResult checkMail(string Email)
        {
            if (context.Users.Any(x => x.Emial == Email))
                return Json(false, JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public ActionResult Logoff()
        {
            var ctx = Request.GetOwinContext();
            //Auth Midd From OWIN
            var authManager = ctx.Authentication;
            //User Authorizate 
            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
            //return View(new UserDB());
        }
    }
}