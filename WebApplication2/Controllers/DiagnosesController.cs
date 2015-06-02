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
        public ActionResult pacientCreate(int visitID, int num)
        {
            newDiagnosis na = new newDiagnosis();
            na.visitID = visitID;
            na.num = num;
            na.diagnosis= new Diagnosis();
            na.eventTypes = db.diagnosisTypes.ToList();
            return PartialView(na);
        }

        // POST: Diagnoses/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(newDiagnosis data)
        {
            VisitDate visit = db.visits.Include(v => v.diagnoses).Where(v => v.ID == data.visitID).First();

            if (visit == null)
                return RedirectToAction("Index", "Pacients");

            Pacient pacient = db.Pacients.Where(p => p.visits.Any(v => v.ID == data.visitID)).First();
            if (pacient == null)
                return RedirectToAction("Index", "Pacients");

            if (ModelState.IsValid)
            {
                DiagnosisType type = db.diagnosisTypes.Where(a => a.ID == data.diagnosis.type.ID).First();
                data.diagnosis.type = type;
                visit.diagnoses.Add(data.diagnosis);

                db.SaveChanges();
                return PartialView("/views/Diagnoses/pacientDetails.cshtml", data.diagnosis);
            }
            return PartialView("/views/Diagnoses/pacientCreate.cshtml", data);

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
        public ActionResult pacientDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnosis Diagnosis = db.diagnoses.Find(id);
            if (Diagnosis == null)
            {
                return HttpNotFound();
            }
            //Diagnosis Diagnosis = db.anamneses.Find(id);
            db.diagnoses.Remove(Diagnosis);
            db.SaveChanges();
            //return View(Diagnosis);
            return PartialView();
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
