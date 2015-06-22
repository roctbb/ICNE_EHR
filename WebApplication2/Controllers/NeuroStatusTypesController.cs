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
    public class NeuroStatusTypesController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: NeuroStatusTypes
        public ActionResult Index()
        {
            return View(db.neuroStatusTypes.ToList());
        }

        // GET: NeuroStatusTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NeuroStatusType neuroStatusType = db.neuroStatusTypes.Find(id);
            if (neuroStatusType == null)
            {
                return HttpNotFound();
            }
            return View(neuroStatusType);
        }

        // GET: NeuroStatusTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NeuroStatusTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,description")] NeuroStatusType neuroStatusType)
        {
            if (ModelState.IsValid)
            {
                db.neuroStatusTypes.Add(neuroStatusType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(neuroStatusType);
        }

        // GET: NeuroStatusTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NeuroStatusType neuroStatusType = db.neuroStatusTypes.Find(id);
            if (neuroStatusType == null)
            {
                return HttpNotFound();
            }
            return View(neuroStatusType);
        }

        // POST: NeuroStatusTypes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,description")] NeuroStatusType neuroStatusType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(neuroStatusType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(neuroStatusType);
        }

        // GET: NeuroStatusTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NeuroStatusType neuroStatusType = db.neuroStatusTypes.Find(id);
            if (neuroStatusType == null)
            {
                return HttpNotFound();
            }
            return View(neuroStatusType);
        }

        // POST: NeuroStatusTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NeuroStatusType neuroStatusType = db.neuroStatusTypes.Find(id);
            db.neuroStatusTypes.Remove(neuroStatusType);
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
