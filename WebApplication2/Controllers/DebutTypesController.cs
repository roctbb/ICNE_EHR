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
    public class DebutTypesController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: DebutTypes
        public ActionResult Index()
        {
            return View(db.debuteTypes.ToList());
        }

        // GET: DebutTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DebutType debutType = db.debuteTypes.Find(id);
            if (debutType == null)
            {
                return HttpNotFound();
            }
            return View(debutType);
        }

        // GET: DebutTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DebutTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name")] DebutType debutType)
        {
            if (ModelState.IsValid)
            {
                db.debuteTypes.Add(debutType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(debutType);
        }

        // GET: DebutTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DebutType debutType = db.debuteTypes.Find(id);
            if (debutType == null)
            {
                return HttpNotFound();
            }
            return View(debutType);
        }

        // POST: DebutTypes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name")] DebutType debutType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(debutType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(debutType);
        }

        // GET: DebutTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DebutType debutType = db.debuteTypes.Find(id);
            if (debutType == null)
            {
                return HttpNotFound();
            }
            return View(debutType);
        }

        // POST: DebutTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DebutType debutType = db.debuteTypes.Find(id);
            db.debuteTypes.Remove(debutType);
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
