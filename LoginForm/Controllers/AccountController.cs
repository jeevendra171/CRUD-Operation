using LoginForm.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace LoginForm.Controllers
{
    public class AccountController : Controller
    {
        AccountDBEntities dc = new AccountDBEntities();
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel register)
        {
            if(ModelState.IsValid)
            {
                var reg = new Models.Login
                {
                    Name = register.Name,
                    MobileNumber = register.MobileNumber,
                    Email = register.Email,
                    Address = register.Address,
                    Password = register.Password,
                };
                dc.Logins.Add(reg);
                dc.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(register);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Login login)
        {
            if (ModelState.IsValid)
            {
                var user = dc.Logins.SingleOrDefault(u => u.Name == login.Name);
                if (user != null)
                {
                    if (user.Password == login.Password)
                    {
                        FormsAuthentication.SetAuthCookie(user.Name,false);
                        return RedirectToAction("Success", "Account");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    }
                }
            }
            return View(login);
        }
        public ViewResult Success()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}