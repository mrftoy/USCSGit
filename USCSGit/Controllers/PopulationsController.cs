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
    public class PopulationsController : Controller
    {
        private USCSModel db = new USCSModel();

        // GET: Populations
        public ActionResult Index()
        {
            return View(db.Populations.ToList());
        }

        // GET: Populations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Population population = db.Populations.Find(id);
            if (population == null)
            {
                return HttpNotFound();
            }
            return View(population);
        }

        // GET: Populations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Populations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PopulationID,Population1,AgeRange,Eff_Date,Thru_Date,Active,Load_Date")] Population population)
        {
            if (ModelState.IsValid)
            {
                db.Populations.Add(population);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(population);
        }

        // GET: Populations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Population population = db.Populations.Find(id);
            if (population == null)
            {
                return HttpNotFound();
            }
            return View(population);
        }

        // POST: Populations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PopulationID,Population1,AgeRange,Eff_Date,Thru_Date,Active,Load_Date")] Population population)
        {
            if (ModelState.IsValid)
            {
                db.Entry(population).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(population);
        }

        // GET: Populations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Population population = db.Populations.Find(id);
            if (population == null)
            {
                return HttpNotFound();
            }
            return View(population);
        }

        // POST: Populations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Population population = db.Populations.Find(id);
            db.Populations.Remove(population);
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
