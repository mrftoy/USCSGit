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
    public class StaffRequirementsController : Controller
    {
        private USCSModel db = new USCSModel();

        // GET: StaffRequirements
        public ActionResult Index()
        {
            return View(db.StaffRequirements.ToList());
        }

        // GET: StaffRequirements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffRequirement staffRequirement = db.StaffRequirements.Find(id);
            if (staffRequirement == null)
            {
                return HttpNotFound();
            }
            return View(staffRequirement);
        }

        // GET: StaffRequirements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StaffRequirements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffRequirementID,StaffLicensure,Eff_Date,Thru_Date,Active,Load_Date")] StaffRequirement staffRequirement)
        {
            if (ModelState.IsValid)
            {
                db.StaffRequirements.Add(staffRequirement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staffRequirement);
        }

        // GET: StaffRequirements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffRequirement staffRequirement = db.StaffRequirements.Find(id);
            if (staffRequirement == null)
            {
                return HttpNotFound();
            }
            return View(staffRequirement);
        }

        // POST: StaffRequirements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffRequirementID,StaffLicensure,Eff_Date,Thru_Date,Active,Load_Date")] StaffRequirement staffRequirement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffRequirement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staffRequirement);
        }

        // GET: StaffRequirements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffRequirement staffRequirement = db.StaffRequirements.Find(id);
            if (staffRequirement == null)
            {
                return HttpNotFound();
            }
            return View(staffRequirement);
        }

        // POST: StaffRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffRequirement staffRequirement = db.StaffRequirements.Find(id);
            db.StaffRequirements.Remove(staffRequirement);
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
