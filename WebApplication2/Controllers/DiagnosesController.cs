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
    public class DiagnosesController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: Diagnoses
        public ActionResult Index()
        {
            return View(db.diagnoses.ToList());
        }

        // GET: Diagnoses/Details/5
        public ActionResult pacientDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnosis diagnosis = db.diagnoses.Include(p => p.type).Where(p => p.ID == id).First();
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/views/Diagnoses/pacientDetails.cshtml", diagnosis);
        }

        // GET: Diagnoses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Diagnoses/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,comments")] Diagnosis diagnosis)
        {
            if (ModelState.IsValid)
            {
                db.diagnoses.Add(diagnosis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(diagnosis);
        }

        // GET: Diagnoses/Edit/5
        public ActionResult pacientEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnosis diagnosis = db.diagnoses.Include(p => p.type).Where(p => p.ID == id).First();
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            return PartialView(diagnosis);
        }

        // POST: Diagnoses/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult pacientEdit([Bind(Include = "ID,comments")] Diagnosis diagnosis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diagnosis).State = EntityState.Modified;
                db.SaveChanges();
                return pacientDetails(diagnosis.ID);
            }
            return PartialView(diagnosis);
        }

        // GET: Diagnoses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnosis diagnosis = db.diagnoses.Find(id);
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            return View(diagnosis);
        }

        // POST: Diagnoses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Diagnosis diagnosis = db.diagnoses.Find(id);
            db.diagnoses.Remove(diagnosis);
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
