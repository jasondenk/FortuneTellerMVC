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
    public class RetirementsController : Controller
    {
        private FortuneTellerMVCEntities db = new FortuneTellerMVCEntities();

        // GET: Retirements
        public ActionResult Index()
        {
            var retirements = db.Retirements.Include(r => r.Customer);
            return View(retirements.ToList());
        }

        // GET: Retirements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retirement retirement = db.Retirements.Find(id);
            if (retirement == null)
            {
                return HttpNotFound();
            }
            return View(retirement);
        }

        // GET: Retirements/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName");
            return View();
        }

        // POST: Retirements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RetirementID,YearsToRetirement,CustomerID")] Retirement retirement)
        {
            if (ModelState.IsValid)
            {
                db.Retirements.Add(retirement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", retirement.CustomerID);
            return View(retirement);
        }

        // GET: Retirements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retirement retirement = db.Retirements.Find(id);
            if (retirement == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", retirement.CustomerID);
            return View(retirement);
        }

        // POST: Retirements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RetirementID,YearsToRetirement,CustomerID")] Retirement retirement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(retirement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", retirement.CustomerID);
            return View(retirement);
        }

        // GET: Retirements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retirement retirement = db.Retirements.Find(id);
            if (retirement == null)
            {
                return HttpNotFound();
            }
            return View(retirement);
        }

        // POST: Retirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Retirement retirement = db.Retirements.Find(id);
            db.Retirements.Remove(retirement);
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
