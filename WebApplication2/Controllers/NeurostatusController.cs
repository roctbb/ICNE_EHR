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
    public class NeurostatusController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: Neurostatus
        public ActionResult Index()
        {
            return View(db.neurostatuses.ToList());
        }

        // GET: Neurostatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Neurostatus neurostatus = db.neurostatuses.Find(id);
            if (neurostatus == null)
            {
                return HttpNotFound();
            }
            return View(neurostatus);
        }

        // GET: Neurostatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Neurostatus/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,description,comments")] Neurostatus neurostatus)
        {
            if (ModelState.IsValid)
            {
                db.neurostatuses.Add(neurostatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(neurostatus);
        }

        // GET: Neurostatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Neurostatus neurostatus = db.neurostatuses.Find(id);
            if (neurostatus == null)
            {
                return HttpNotFound();
            }
            return View(neurostatus);
        }

        // POST: Neurostatus/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,description,comments")] Neurostatus neurostatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(neurostatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(neurostatus);
        }

        // GET: Neurostatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Neurostatus neurostatus = db.neurostatuses.Find(id);
            if (neurostatus == null)
            {
                return HttpNotFound();
            }
            return View(neurostatus);
        }

        // POST: Neurostatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Neurostatus neurostatus = db.neurostatuses.Find(id);
            db.neurostatuses.Remove(neurostatus);
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
