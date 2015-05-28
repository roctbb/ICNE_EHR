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
    public class ResearchesController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: Researches
        public ActionResult Index()
        {
            return View(db.researches.ToList());
        }

        // GET: Researches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Research research = db.researches.Find(id);
            if (research == null)
            {
                return HttpNotFound();
            }
            return View(research);
        }

        // GET: Researches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Researches/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,description,comments")] Research research)
        {
            if (ModelState.IsValid)
            {
                db.researches.Add(research);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(research);
        }

        // GET: Researches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Research research = db.researches.Find(id);
            if (research == null)
            {
                return HttpNotFound();
            }
            return View(research);
        }

        // POST: Researches/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,description,comments")] Research research)
        {
            if (ModelState.IsValid)
            {
                db.Entry(research).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(research);
        }

        // GET: Researches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Research research = db.researches.Find(id);
            if (research == null)
            {
                return HttpNotFound();
            }
            return View(research);
        }

        // POST: Researches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Research research = db.researches.Find(id);
            db.researches.Remove(research);
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
