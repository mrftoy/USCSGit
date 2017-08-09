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
    public class ModifiersController : Controller
    {
        private USCSModel db = new USCSModel();

        // GET: Modifiers
        public ActionResult Index()
        {
            return View(db.Modifiers.ToList());
        }

        // GET: Modifiers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modifier modifier = db.Modifiers.Find(id);
            if (modifier == null)
            {
                return HttpNotFound();
            }
            return View(modifier);
        }

        // GET: Modifiers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Modifiers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModifierID,Modifier1,ModifierType,ModifierDescription,ModifierDefinition,Eff_Date,Thru_Date,Active,Load_Date")] Modifier modifier)
        {
            if (ModelState.IsValid)
            {
                db.Modifiers.Add(modifier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modifier);
        }

        // GET: Modifiers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modifier modifier = db.Modifiers.Find(id);
            if (modifier == null)
            {
                return HttpNotFound();
            }
            return View(modifier);
        }

        // POST: Modifiers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModifierID,Modifier1,ModifierType,ModifierDescription,ModifierDefinition,Eff_Date,Thru_Date,Active,Load_Date")] Modifier modifier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modifier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modifier);
        }

        // GET: Modifiers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modifier modifier = db.Modifiers.Find(id);
            if (modifier == null)
            {
                return HttpNotFound();
            }
            return View(modifier);
        }

        // POST: Modifiers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Modifier modifier = db.Modifiers.Find(id);
            db.Modifiers.Remove(modifier);
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
