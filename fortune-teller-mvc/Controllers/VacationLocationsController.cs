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
    public class VacationLocationsController : Controller
    {
        private FortuneTellerMVCEntities db = new FortuneTellerMVCEntities();

        // GET: VacationLocations
        public ActionResult Index()
        {
            var vacationLocations = db.VacationLocations.Include(v => v.Customer);
            return View(vacationLocations.ToList());
        }

        // GET: VacationLocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VacationLocation vacationLocation = db.VacationLocations.Find(id);
            if (vacationLocation == null)
            {
                return HttpNotFound();
            }
            return View(vacationLocation);
        }

        // GET: VacationLocations/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName");
            return View();
        }

        // POST: VacationLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VacationLocationID,VacationLocation1,CustomerID")] VacationLocation vacationLocation)
        {
            if (ModelState.IsValid)
            {
                db.VacationLocations.Add(vacationLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", vacationLocation.CustomerID);
            return View(vacationLocation);
        }

        // GET: VacationLocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VacationLocation vacationLocation = db.VacationLocations.Find(id);
            if (vacationLocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", vacationLocation.CustomerID);
            return View(vacationLocation);
        }

        // POST: VacationLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VacationLocationID,VacationLocation1,CustomerID")] VacationLocation vacationLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vacationLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", vacationLocation.CustomerID);
            return View(vacationLocation);
        }

        // GET: VacationLocations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VacationLocation vacationLocation = db.VacationLocations.Find(id);
            if (vacationLocation == null)
            {
                return HttpNotFound();
            }
            return View(vacationLocation);
        }

        // POST: VacationLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VacationLocation vacationLocation = db.VacationLocations.Find(id);
            db.VacationLocations.Remove(vacationLocation);
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
