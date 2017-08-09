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
    public class CategoriesController : Controller
    {
        private USCSModel db = new USCSModel();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,Domain,PrimaryCategory,SecondaryCategory,TertiaryCategory,Eff_Date,Thru_Date,Deleted,Load_Date")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,Domain,PrimaryCategory,SecondaryCategory,TertiaryCategory,Eff_Date,Thru_Date,Deleted,Load_Date")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetDomainName(string q)
        {
            var DomainNames = (from p in db.Categories where p.Domain.Contains(q) select p.Domain).Distinct().Take(10);

            string content = string.Join<string>("\n", DomainNames);
            return Content(content);
        }
        public ActionResult GetPrimaryCategoryName(string q)
        {
            var PrimaryCategoryNames = (from o in db.Categories where o.PrimaryCategory.Contains(q) select o.PrimaryCategory).Distinct().Take(10);

            string content2 = string.Join<string>("\n", PrimaryCategoryNames);
            return Content(content2);
        }

        public ActionResult GetSecondaryCategoryName(string q)
        {
            var SecondaryCategoryNames = (from s in db.Categories where s.SecondaryCategory.Contains(q) select s.SecondaryCategory).Distinct().Take(10);

            string contentsc = string.Join<string>("\n", SecondaryCategoryNames);
            return Content(contentsc);
        }

        public ActionResult GetTertiaryCategoryName(string q)
        {
            var TertiaryCategoryNames = (from t in db.Categories where t.TertiaryCategory.Contains(q) select t.TertiaryCategory).Distinct().Take(10);

            string contenttc = string.Join<string>("\n", TertiaryCategoryNames);
            return Content(contenttc);
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
