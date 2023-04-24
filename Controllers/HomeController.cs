using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        CredentialsContext db = new CredentialsContext();       // GET: Home
        public ActionResult Index()
        {
            
            return View(db.Products.ToList());
        }
        
    }
}