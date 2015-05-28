using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class SyndromeTypesController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: SyndromeTypes
        public ActionResult Index()
        {
            return View(db.syndromeTypes.ToList());
        }

        // GET: SyndromeTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SyndromeType syndromeType = db.syndromeTypes.Find(id);
            if (syndromeType == null)
            {
                return HttpNotFound();
            }
            return View(syndromeType);
        }

        // GET: SyndromeTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SyndromeTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,description")] SyndromeType syndromeType)
        {
            if (ModelState.IsValid)
            {
                db.syndromeTypes.Add(syndromeType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(syndromeType);
        }

        // GET: SyndromeTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SyndromeType syndromeType = db.syndromeTypes.Find(id);
            if (syndromeType == null)
            {
                return HttpNotFound();
            }
            return View(syndromeType);
        }

        // POST: SyndromeTypes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,description")] SyndromeType syndromeType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(syndromeType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(syndromeType);
        }

        // GET: SyndromeTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SyndromeType syndromeType = db.syndromeTypes.Find(id);
            if (syndromeType == null)
            {
                return HttpNotFound();
            }
            return View(syndromeType);
        }

        // POST: SyndromeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SyndromeType syndromeType = db.syndromeTypes.Find(id);
            db.syndromeTypes.Remove(syndromeType);
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
