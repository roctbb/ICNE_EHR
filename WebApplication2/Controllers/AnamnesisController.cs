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
    public class AnamnesisController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: Anamnesis
        public ActionResult Index()
        {
            return View(db.anamneses.ToList());
        }

        // GET: Anamnesis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anamnesis anamnesis = db.anamneses.Find(id);
            if (anamnesis == null)
            {
                return HttpNotFound();
            }
            return View(anamnesis);
        }

        // GET: Anamnesis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Anamnesis/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,comments")] Anamnesis anamnesis)
        {
            if (ModelState.IsValid)
            {
                db.anamneses.Add(anamnesis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(anamnesis);
        }

        // GET: Anamnesis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anamnesis anamnesis = db.anamneses.Find(id);
            if (anamnesis == null)
            {
                return HttpNotFound();
            }
            return View(anamnesis);
        }

        // POST: Anamnesis/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,comments")] Anamnesis anamnesis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anamnesis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(anamnesis);
        }

        // GET: Anamnesis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anamnesis anamnesis = db.anamneses.Find(id);
            if (anamnesis == null)
            {
                return HttpNotFound();
            }
            return View(anamnesis);
        }

        // POST: Anamnesis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anamnesis anamnesis = db.anamneses.Find(id);
            db.anamneses.Remove(anamnesis);
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
