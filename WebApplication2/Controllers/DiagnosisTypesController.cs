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
    public class DiagnosisTypesController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: DiagnosisTypes
        public ActionResult Index()
        {
            return View(db.diagnosisTypes.ToList());
        }

        // GET: DiagnosisTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiagnosisType diagnosisType = db.diagnosisTypes.Find(id);
            if (diagnosisType == null)
            {
                return HttpNotFound();
            }
            return View(diagnosisType);
        }

        // GET: DiagnosisTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiagnosisTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,description")] DiagnosisType diagnosisType)
        {
            if (ModelState.IsValid)
            {
                db.diagnosisTypes.Add(diagnosisType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(diagnosisType);
        }

        // GET: DiagnosisTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiagnosisType diagnosisType = db.diagnosisTypes.Find(id);
            if (diagnosisType == null)
            {
                return HttpNotFound();
            }
            return View(diagnosisType);
        }

        // POST: DiagnosisTypes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,description")] DiagnosisType diagnosisType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diagnosisType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diagnosisType);
        }

        // GET: DiagnosisTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiagnosisType diagnosisType = db.diagnosisTypes.Find(id);
            if (diagnosisType == null)
            {
                return HttpNotFound();
            }
            return View(diagnosisType);
        }

        // POST: DiagnosisTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiagnosisType diagnosisType = db.diagnosisTypes.Find(id);
            db.diagnosisTypes.Remove(diagnosisType);
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
