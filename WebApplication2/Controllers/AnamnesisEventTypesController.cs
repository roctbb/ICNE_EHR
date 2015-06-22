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
    [Authorize]
    public class AnamnesisEventTypesController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: AnamnesisEventTypes
        public ActionResult Index()
        {
            return View(db.anamnesisTypes.ToList());
        }

        // GET: AnamnesisEventTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnamnesisEventType anamnesisEventType = db.anamnesisTypes.Find(id);
            if (anamnesisEventType == null)
            {
                return HttpNotFound();
            }
            return View(anamnesisEventType);
        }

        // GET: AnamnesisEventTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnamnesisEventTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name")] AnamnesisEventType anamnesisEventType)
        {
            if (ModelState.IsValid)
            {
                db.anamnesisTypes.Add(anamnesisEventType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(anamnesisEventType);
        }

        // GET: AnamnesisEventTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnamnesisEventType anamnesisEventType = db.anamnesisTypes.Find(id);
            if (anamnesisEventType == null)
            {
                return HttpNotFound();
            }
            return View(anamnesisEventType);
        }

        // POST: AnamnesisEventTypes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name")] AnamnesisEventType anamnesisEventType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anamnesisEventType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(anamnesisEventType);
        }

        // GET: AnamnesisEventTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnamnesisEventType anamnesisEventType = db.anamnesisTypes.Find(id);
            if (anamnesisEventType == null)
            {
                return HttpNotFound();
            }
            return View(anamnesisEventType);
        }

        // POST: AnamnesisEventTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnamnesisEventType anamnesisEventType = db.anamnesisTypes.Find(id);
            db.anamnesisTypes.Remove(anamnesisEventType);
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
