using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fortune_teller_mvc.Models;

namespace fortune_teller_mvc.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerMVCEntities db = new FortuneTellerMVCEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            if (customer.Age%2==0)
            { ViewBag.RetirementYears = 5; }
            else
            { ViewBag.RetirementYears = 6; }
            if (customer.NumberOfSiblings == 0)
            {
                ViewBag.Location = "Hawaii";
            }
            else if (customer.NumberOfSiblings == 1)
            {
                ViewBag.Location = "Peru";
            }
            else if (customer.NumberOfSiblings == 2)
            {
                ViewBag.Location = "Mount Baker, WA";
            }
            else if (customer.NumberOfSiblings == 3)
            {
                ViewBag.Location = "California";
            }
            else if (customer.NumberOfSiblings > 3)
            {
                ViewBag.Location = "Mexico";
            }
            else
            {
                ViewBag.Location = "Bonndock, South Dakota";
            }
            

            if (customer.BirthMonth >= 1 & customer.BirthMonth <= 12)
            {
                if (customer.BirthMonth <= 4)
                {
                    ViewBag.Money = 10000000.00;
                }
                else if (customer.BirthMonth <= 8)
                {
                    ViewBag.Money = 20000000.00;
                }
                else
                {
                    ViewBag.Money = 30000000.00;
                }
            }
            else
            {
                ViewBag.Money = 0.00;
            }
            if (customer.FavoriteColor.ToLower()=="red")
            {
                ViewBag.Transportation = "boat";
            } 
            else if (customer.FavoriteColor.ToLower() == "orange")
            {
                ViewBag.Transportation = "jet";
            }
            else if (customer.FavoriteColor.ToLower() == "yellow")
            {
                ViewBag.Transportation = "dog sled team";
            }
            else if (customer.FavoriteColor.ToLower() == "green")
            {
                ViewBag.Transportation = "helicopter";
            }
            else if (customer.FavoriteColor.ToLower() == "blue")
            {
                ViewBag.Transportation = "jet pack";
            }
            else if (customer.FavoriteColor.ToLower() == "indigo")
            {
                ViewBag.Transportation = "jeep";
            }
            else if (customer.FavoriteColor.ToLower() == "violet")
            {
                ViewBag.Transportation = "magic carpet";
            }
            else
            {
                ViewBag.Transportation = "donkey";
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavoriteColor,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavoriteColor,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
