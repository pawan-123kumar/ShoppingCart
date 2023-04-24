using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductsController : Controller
    {
        private CredentialsContext db = new CredentialsContext();

        // GET: Products
        public ActionResult Index()
        {
            var product = db.Products.Where(x => x.Status == "Enable").ToList();
            return View(product);
        }

        // GET: Products/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file,Product product)
        {
            string filename = Path.GetFileName(file.FileName);
            string _filename = DateTime.Now.ToString("yymmssfff") + filename;
            string extension = Path.GetExtension(file.FileName);


            string path = Path.Combine(Server.MapPath("~/Content/Images/"), _filename);
            product.Image = "~/Content/Images/" + _filename;

            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {

                db.Products.Add(product);
                db.SaveChanges();

                file.SaveAs(path);
                ViewBag.msg = "Product Added";
                ModelState.Clear();
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.msg = "Invalid File Type";
            }
            return View();
        }

        // GET: Products/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            Session["ImgPath"] = product.Image;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, Product product)
        {
            if (file != null)
            {
                string filename = Path.GetFileName(file.FileName);
                string _filename = DateTime.Now.ToString("yymmssfff") + filename;
                string extension = Path.GetExtension(file.FileName);


                string path = Path.Combine(Server.MapPath("~/Content/Images/"), _filename);
                product.Image = "~/Content/Images/" + _filename;

                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {

                    db.Entry(product).State = EntityState.Modified;
                    string oldImgPath = Request.MapPath(Session["ImgPath"].ToString());  
                    db.SaveChanges();
                   if(db.SaveChanges ()>0)
                    {
                        file.SaveAs(path);
                        if (System.IO.File.Exists(oldImgPath))
                        {
                            System.IO.File.Delete(oldImgPath);
                        }


                    }
                    file.SaveAs(path);
                    ViewBag.msg = "Product Updated";
                    ModelState.Clear();
                    return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError("", "file not supported");
                    return View(product);
                }
            }
            else
            {
                product.Image = Session["ImgPath"].ToString();
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            
        }

        // GET: Products/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

        [HttpPost, ActionName("Delete")]
        public ActionResult SoftDelete(Guid id)
        {
            Product pro = db.Products.Find(id);
            pro.Status = "Disable";
            db.Entry(pro).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
