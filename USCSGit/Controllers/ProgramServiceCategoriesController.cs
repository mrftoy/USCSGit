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
    public class ProgramServiceCategoriesController : Controller
    {
        private USCSModel db = new USCSModel();

        // GET: ProgramServiceCategories
        public ActionResult Index()
        {
            return View(db.ProgramServiceCategories.ToList());
        }

        // GET: ProgramServiceCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramServiceCategory programServiceCategory = db.ProgramServiceCategories.Find(id);
            if (programServiceCategory == null)
            {
                return HttpNotFound();
            }
            return View(programServiceCategory);
        }

        // GET: ProgramServiceCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProgramServiceCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProgramServiceCategoryID,ProgramServiceCategory1,Eff_Date,Thru_Date,Active,Load_Date")] ProgramServiceCategory programServiceCategory)
        {
            if (ModelState.IsValid)
            {
                db.ProgramServiceCategories.Add(programServiceCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(programServiceCategory);
        }

        // GET: ProgramServiceCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramServiceCategory programServiceCategory = db.ProgramServiceCategories.Find(id);
            if (programServiceCategory == null)
            {
                return HttpNotFound();
            }
            return View(programServiceCategory);
        }

        // POST: ProgramServiceCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProgramServiceCategoryID,ProgramServiceCategory1,Eff_Date,Thru_Date,Active,Load_Date")] ProgramServiceCategory programServiceCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programServiceCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(programServiceCategory);
        }

        // GET: ProgramServiceCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramServiceCategory programServiceCategory = db.ProgramServiceCategories.Find(id);
            if (programServiceCategory == null)
            {
                return HttpNotFound();
            }
            return View(programServiceCategory);
        }

        // POST: ProgramServiceCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProgramServiceCategory programServiceCategory = db.ProgramServiceCategories.Find(id);
            db.ProgramServiceCategories.Remove(programServiceCategory);
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
