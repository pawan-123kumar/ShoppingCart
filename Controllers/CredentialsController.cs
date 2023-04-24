using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShoppingCart.Controllers
{
    public class CredentialsController : Controller
    {
        CredentialsContext context = new CredentialsContext();
        // GET: Credentials
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Credential credential)
        {
            if (ModelState.IsValid)
            {
                var user = context.Credentials.Where(x => x.UserName == credential.UserName && x.Password == credential.Password).FirstOrDefault();
                if(user != null)
                {
                  if(user.Roles=="Admin")
                    {
                        Session["username"] = user.UserName;
                        FormsAuthentication.SetAuthCookie(user.UserName, false);
                        return RedirectToAction("Index", "Products");
                    }
                  else if(user.Roles=="User")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else ModelState.AddModelError("", "Invalid UserName or Password");
                    return View();
                }
            }
            ModelState.AddModelError("", "Invalid UserName or Password");
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Credential user)
        {
            if (ModelState.IsValid)
            {
                Credential user1 = context.Credentials.FirstOrDefault(emp => emp.UserName == user.UserName);

                if (user1 == null)
                {
                    user.Roles = "User";
                    context.Credentials.Add(user);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Credentials");
                }
                else
                {
                    ModelState.AddModelError("", "Already exist");
                }
            }

            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["username"] = null;
            return RedirectToAction("Index");
        }

    }
}