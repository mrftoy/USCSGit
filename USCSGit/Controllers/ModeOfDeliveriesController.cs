using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using USCS.Models;

namespace USCS.Controllers
{
    public class ModeOfDeliveriesController : Controller
    {
        private USCSModel db = new USCSModel();

        // GET: ModeOfDeliveries
        public ActionResult Index()
        {
            return View(db.ModeOfDeliveries.ToList());
        }

        // GET: ModeOfDeliveries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModeOfDelivery modeOfDelivery = db.ModeOfDeliveries.Find(id);
            if (modeOfDelivery == null)
            {
                return HttpNotFound();
            }
            return View(modeOfDelivery);
        }

        // GET: ModeOfDeliveries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModeOfDeliveries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModeOfDeliveryID,ModeOfDelivery1,Eff_Date,Thru_Date,Active,Load_Date")] ModeOfDelivery modeOfDelivery)
        {
            if (ModelState.IsValid)
            {
                db.ModeOfDeliveries.Add(modeOfDelivery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modeOfDelivery);
        }

        // GET: ModeOfDeliveries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModeOfDelivery modeOfDelivery = db.ModeOfDeliveries.Find(id);
            if (modeOfDelivery == null)
            {
                return HttpNotFound();
            }
            return View(modeOfDelivery);
        }

        // POST: ModeOfDeliveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModeOfDeliveryID,ModeOfDelivery1,Eff_Date,Thru_Date,Active,Load_Date")] ModeOfDelivery modeOfDelivery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modeOfDelivery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modeOfDelivery);
        }

        // GET: ModeOfDeliveries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModeOfDelivery modeOfDelivery = db.ModeOfDeliveries.Find(id);
            if (modeOfDelivery == null)
            {
                return HttpNotFound();
            }
            return View(modeOfDelivery);
        }

        // POST: ModeOfDeliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ModeOfDelivery modeOfDelivery = db.ModeOfDeliveries.Find(id);
            db.ModeOfDeliveries.Remove(modeOfDelivery);
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
