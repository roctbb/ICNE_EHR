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
    public class AnalysesController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: Analyses
        public ActionResult Index()
        {
            return View(db.analysis.ToList());
        }

        // GET: Analysis/Details/5
        public ActionResult pacientDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Analysis analysis = db.analysis.Include(p => p.type).Where(p => p.ID == id).FirstOrDefault();
            if (analysis == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/views/Analyses/pacientDetails.cshtml", analysis);
        }

        // GET: Analysis/Create
        public ActionResult pacientCreate(int visitID, int num)
        {
            newAnalysis na = new newAnalysis();
            na.visitID = visitID;
            na.num = num;
            na.analysis = new Analysis();
            na.eventTypes = db.analysisTypes.ToList();
            return PartialView(na);
        }

        // POST: Debuts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(newAnalysis data)
        {
            VisitDate visit = db.visits.Include(v => v.analysis).Where(v => v.ID == data.visitID).FirstOrDefault();

            if (visit == null)
                return RedirectToAction("Index", "Pacients");

            Pacient pacient = db.pacients.Where(p => p.visits.Any(v => v.ID == data.visitID)).FirstOrDefault();
            if (pacient == null)
                return RedirectToAction("Index", "Pacients");

            if (ModelState.IsValid)
            {
                AnalysisType type = db.analysisTypes.Where(a => a.ID == data.analysis.type.ID).First();
                data.analysis.type = type;
                visit.analysis.Add(data.analysis);

                db.SaveChanges();
                return PartialView("/views/Analyses/pacientDetails.cshtml", data.analysis);
            }
            return PartialView("/views/Analyses/pacientCreate.cshtml", data);

        }
        public ActionResult pacientCreateByDate(int id)
        {
            newAnalysis na = new newAnalysis();
            na.analysis = new Analysis();
            na.eventTypes = db.analysisTypes.ToList();
            na.pacientID = id;
            return PartialView(na);
        }
        public ActionResult CreateByDate(newAnalysis data)
        {
            Pacient pacient = db.pacients.Include(p => p.visits.Select(v => v.analysis)).Include(p => p.doctor).Where(p => p.ID == data.pacientID).FirstOrDefault();
            if (pacient == null)
                return RedirectToAction("Index", "Pacients");
            VisitDate visit = pacient.visits.Where(v => v.date == data.initialDate).FirstOrDefault();
            if (visit != null)
            {
                AnalysisType type = db.analysisTypes.Where(a => a.ID == data.analysis.type.ID).First();
                data.analysis.type = type;
                visit.analysis.Add(data.analysis);
                db.SaveChanges();
                return PartialView("/views/Analyses/pacientDetails.cshtml", data.analysis);
            }
            else
            {
                AnalysisType type = db.analysisTypes.Where(a => a.ID == data.analysis.type.ID).First();
                data.analysis.type = type;
                visit = new VisitDate();
                visit.doctorID = pacient.doctor.ID;
                visit.date = data.initialDate;
                visit.analysis = new List<Analysis>();
                visit.analysis.Add(data.analysis);
                pacient.visits.Add(visit);
                db.SaveChanges();
                return PartialView("/views/Analyses/pacientDetails.cshtml", data.analysis);

            }
            //return PartialView("/views/Assigment/pacientCreate.cshtml", data);

        }
        // GET: Analysis/Edit/5
        public ActionResult pacientEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Analysis analysis = db.analysis.Include(p => p.type).Where(p => p.ID == id).First();
            if (analysis == null)
            {
                return HttpNotFound();
            }
            return PartialView(analysis);
        }

        // POST: Analysis/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult pacientEdit([Bind(Include = "ID,description,comments")] Analysis analysis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(analysis).State = EntityState.Modified;
                db.SaveChanges();
                return pacientDetails(analysis.ID);
            }
            return PartialView(analysis);
        }

        // GET: Analysis/Delete/5
        public ActionResult pacientDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Analysis analysis = db.analysis.Find(id);
            if (analysis == null)
            {
                return HttpNotFound();
            }
            //Research Research = db.anamneses.Find(id);
            db.analysis.Remove(analysis);
            db.SaveChanges();
            //return View(Research);
            return PartialView();
        }

        // POST: Analysis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Analysis analysis = db.analysis.Find(id);
            db.analysis.Remove(analysis);
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
