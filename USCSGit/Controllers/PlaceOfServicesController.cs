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
    public class PlaceOfServicesController : Controller
    {
        private USCSModel db = new USCSModel();

        // GET: PlaceOfServices
        public ActionResult Index()
        {
            return View(db.PlaceOfServices.ToList());
        }

        // GET: PlaceOfServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceOfService placeOfService = db.PlaceOfServices.Find(id);
            if (placeOfService == null)
            {
                return HttpNotFound();
            }
            return View(placeOfService);
        }

        // GET: PlaceOfServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlaceOfServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlaceOfServiceID,POSCode,POSName,POSDescription,Eff_Date,Thru_Date,Deleted,Load_Date")] PlaceOfService placeOfService)
        {
            if (ModelState.IsValid)
            {
                db.PlaceOfServices.Add(placeOfService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(placeOfService);
        }

        // GET: PlaceOfServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceOfService placeOfService = db.PlaceOfServices.Find(id);
            if (placeOfService == null)
            {
                return HttpNotFound();
            }
            return View(placeOfService);
        }

        // POST: PlaceOfServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlaceOfServiceID,POSCode,POSName,POSDescription,Eff_Date,Thru_Date,Deleted,Load_Date")] PlaceOfService placeOfService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(placeOfService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(placeOfService);
        }

        // GET: PlaceOfServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceOfService placeOfService = db.PlaceOfServices.Find(id);
            if (placeOfService == null)
            {
                return HttpNotFound();
            }
            return View(placeOfService);
        }

        // POST: PlaceOfServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlaceOfService placeOfService = db.PlaceOfServices.Find(id);
            db.PlaceOfServices.Remove(placeOfService);
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
