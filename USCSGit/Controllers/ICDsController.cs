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
    public class ICDsController : Controller
    {
        private USCSModel db = new USCSModel();

        // GET: ICDs
        public ActionResult Index()
        {
            return View(db.ICDs.ToList());
        }

        // GET: ICDs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ICD iCD = db.ICDs.Find(id);
            if (iCD == null)
            {
                return HttpNotFound();
            }
            return View(iCD);
        }

        // GET: ICDs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ICDs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ICDID,ICDVersion,ICDCode,ICDDescription,Eff_Date,Thru_Date,Deleted,Load_Date")] ICD iCD)
        {
            if (ModelState.IsValid)
            {
                db.ICDs.Add(iCD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iCD);
        }

        // GET: ICDs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ICD iCD = db.ICDs.Find(id);
            if (iCD == null)
            {
                return HttpNotFound();
            }
            return View(iCD);
        }

        // POST: ICDs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ICDID,ICDVersion,ICDCode,ICDDescription,Eff_Date,Thru_Date,Deleted,Load_Date")] ICD iCD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iCD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iCD);
        }

        // GET: ICDs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ICD iCD = db.ICDs.Find(id);
            if (iCD == null)
            {
                return HttpNotFound();
            }
            return View(iCD);
        }

        // POST: ICDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ICD iCD = db.ICDs.Find(id);
            db.ICDs.Remove(iCD);
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
