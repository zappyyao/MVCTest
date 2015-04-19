using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC2.Models;
using System.Data.Entity.Validation;

namespace MVC2.Controllers
{
    public class CRUDController : Controller
    {

        FabricsEntities db = new FabricsEntities();
        // GET: CRUD
        public ActionResult Index()
        {
           
            var data = db.Product.Where(p => p.ProductName.Contains("C") && p.Price >= 5 && p.Price <= 20);

            return View(data);
        }

        // GET: CRUD/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CRUD/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: CRUD/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Product objProduct = new Product();
                objProduct.Price = Convert.ToDecimal(collection["Price"]);
                objProduct.ProductName = collection["ProductName"];
                objProduct.Stock = Convert.ToDecimal(collection["Stock"]);
                objProduct.Active = Convert.ToBoolean(collection["Active"]);
                db.Product.Add(objProduct);
                db.SaveChanges();

                //Test
                //BatchUpdate();

                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                //throw ex;
                var allErrors = new List<string>();
                foreach (DbEntityValidationResult re in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError err in re.ValidationErrors)
                    {
                        allErrors.Add(err.ErrorMessage);
                    }
                }
                //ViewBag.Errors = allErrors;

                return View();
            }
        }


        public ActionResult BatchUpdate()
        {
            var data = db.Product.Where(p => p.ProductName.Contains("C") && p.Price >= 5 && p.Price <= 10);

            foreach (var item in data)
            {
                item.Price = item.Price * 2;
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                //throw ex;
                var allErrors = new List<string>();
                foreach (DbEntityValidationResult re in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError err in re.ValidationErrors)
                    {
                        allErrors.Add(err.ErrorMessage);
                    }
                }
                //ViewBag.Errors = allErrors;

                return View();
            }

            return RedirectToAction("Index");
        }


        // GET: CRUD/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CRUD/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CRUD/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Client vClient = db.Client.Find(id);


                //db.OrderLine.RemoveRange(vClient.Order.)
                foreach (var item in vClient.Order)
                {
                    db.OrderLine.RemoveRange(item.OrderLine);
                }
                db.Order.RemoveRange(vClient.Order);
                db.Client.Remove(vClient);



                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                //throw ex;
                var allErrors = new List<string>();
                foreach (DbEntityValidationResult re in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError err in re.ValidationErrors)
                    {
                        allErrors.Add(err.ErrorMessage);
                    }
                }

            }
            return RedirectToAction("Index");
        }


    
        // POST: CRUD/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
