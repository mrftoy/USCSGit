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
    public class ModifierTypesController : Controller
    {
        private USCSModel db = new USCSModel();

        // GET: ModifierTypes
        public ActionResult Index()
        {
            return View(db.ModifierTypes.ToList());
        }

        // GET: ModifierTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModifierType modifierType = db.ModifierTypes.Find(id);
            if (modifierType == null)
            {
                return HttpNotFound();
            }
            return View(modifierType);
        }

        // GET: ModifierTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModifierTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModifierTypeID,ModifierType1,Eff_Date,Thru_Date,Deleted,Load_Date")] ModifierType modifierType)
        {
            if (ModelState.IsValid)
            {
                db.ModifierTypes.Add(modifierType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modifierType);
        }

        // GET: ModifierTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModifierType modifierType = db.ModifierTypes.Find(id);
            if (modifierType == null)
            {
                return HttpNotFound();
            }
            return View(modifierType);
        }

        // POST: ModifierTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModifierTypeID,ModifierType1,Eff_Date,Thru_Date,Deleted,Load_Date")] ModifierType modifierType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modifierType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modifierType);
        }

        // GET: ModifierTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModifierType modifierType = db.ModifierTypes.Find(id);
            if (modifierType == null)
            {
                return HttpNotFound();
            }
            return View(modifierType);
        }

        // POST: ModifierTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ModifierType modifierType = db.ModifierTypes.Find(id);
            db.ModifierTypes.Remove(modifierType);
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
