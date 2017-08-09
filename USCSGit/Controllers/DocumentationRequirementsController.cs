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
    public class DocumentationRequirementsController : Controller
    {
        private USCSModel db = new USCSModel();

        // GET: DocumentationRequirements
        public ActionResult Index()
        {
            return View(db.DocumentationRequirements.ToList());
        }

        // GET: DocumentationRequirements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentationRequirement documentationRequirement = db.DocumentationRequirements.Find(id);
            if (documentationRequirement == null)
            {
                return HttpNotFound();
            }
            return View(documentationRequirement);
        }

        // GET: DocumentationRequirements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentationRequirements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocumentationRequirementID,DocumentationRequirement1,Eff_Date,Thru_Date,Active,Load_Date")] DocumentationRequirement documentationRequirement)
        {
            if (ModelState.IsValid)
            {
                db.DocumentationRequirements.Add(documentationRequirement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(documentationRequirement);
        }

        // GET: DocumentationRequirements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentationRequirement documentationRequirement = db.DocumentationRequirements.Find(id);
            if (documentationRequirement == null)
            {
                return HttpNotFound();
            }
            return View(documentationRequirement);
        }

        // POST: DocumentationRequirements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocumentationRequirementID,DocumentationRequirement1,Eff_Date,Thru_Date,Active,Load_Date")] DocumentationRequirement documentationRequirement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentationRequirement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documentationRequirement);
        }

        // GET: DocumentationRequirements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentationRequirement documentationRequirement = db.DocumentationRequirements.Find(id);
            if (documentationRequirement == null)
            {
                return HttpNotFound();
            }
            return View(documentationRequirement);
        }

        // POST: DocumentationRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentationRequirement documentationRequirement = db.DocumentationRequirements.Find(id);
            db.DocumentationRequirements.Remove(documentationRequirement);
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
