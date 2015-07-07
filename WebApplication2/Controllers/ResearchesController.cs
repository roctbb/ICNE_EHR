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
    public class ResearchesController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: Researches
        public ActionResult Index()
        {
            return View(db.researches.ToList());
        }

        // GET: Researches/Details/5
        public ActionResult pacientDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Research research = db.researches.Include(p => p.type).Where(p => p.ID == id).First();
            if (research == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/views/Researches/pacientDetails.cshtml", research);
        }

        // GET: Researches/Create
        public ActionResult pacientCreate(int visitID, int num)
        {
            newResearch na = new newResearch();
            na.visitID = visitID;
            na.num = num;
            na.research = new Research();
            na.eventTypes = db.researchTypes.ToList();
            return PartialView(na);
        }

        // POST: Debuts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(newResearch data)
        {
            VisitDate visit = db.visits.Include(v => v.researches).Where(v => v.ID == data.visitID).First();

            if (visit == null)
                return RedirectToAction("Index", "Pacients");

            Pacient pacient = db.pacients.Where(p => p.visits.Any(v => v.ID == data.visitID)).First();
            if (pacient == null)
                return RedirectToAction("Index", "Pacients");

            if (ModelState.IsValid)
            {
                ResearchType type = db.researchTypes.Where(a => a.ID == data.research.type.ID).First();
                data.research.type = type;
                visit.researches.Add(data.research);

                db.SaveChanges();
                return PartialView("/views/Researches/pacientDetails.cshtml", data.research);
            }
            return PartialView("/views/Researches/pacientCreate.cshtml", data);

        }
        public ActionResult pacientCreateByDate(int id)
        {
            newResearch na = new newResearch();
            na.research = new Research();
            na.eventTypes = db.researchTypes.ToList();
            na.pacientID = id;
            return PartialView(na);
        }
        public ActionResult CreateByDate(newResearch data)
        {
            Pacient pacient = db.pacients.Include(p => p.visits.Select(v => v.researches)).Include(p => p.doctor).Where(p => p.ID == data.pacientID).First();
            if (pacient == null)
                return RedirectToAction("Index", "Pacients");
            VisitDate visit = pacient.visits.Where(v => v.date == data.initialDate).FirstOrDefault();
            if (visit != null)
            {
                ResearchType type = db.researchTypes.Where(a => a.ID == data.research.type.ID).First();
                data.research.type = type;
                visit.researches.Add(data.research);
                db.SaveChanges();
                return PartialView("/views/Researches/pacientDetails.cshtml", data.research);
            }
            else
            {
                ResearchType type = db.researchTypes.Where(a => a.ID == data.research.type.ID).First();
                data.research.type = type;
                visit = new VisitDate();
                visit.doctorID = pacient.doctor.ID;
                visit.date = data.initialDate;
                visit.researches = new List<Research>();
                visit.researches.Add(data.research);
                pacient.visits.Add(visit);
                db.SaveChanges();
                return PartialView("/views/Researches/pacientDetails.cshtml", data.research);

            }
            //return PartialView("/views/Research/pacientCreate.cshtml", data);

        }
        // GET: Researches/Edit/5
        public ActionResult pacientEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Research research = db.researches.Include(p => p.type).Where(p => p.ID == id).First();
            if (research == null)
            {
                return HttpNotFound();
            }
            return PartialView(research);
        }

        // POST: Researches/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult pacientEdit([Bind(Include = "ID,description,comments")] Research research)
        {
            if (ModelState.IsValid)
            {
                db.Entry(research).State = EntityState.Modified;
                db.SaveChanges();
                return pacientDetails(research.ID);
            }
            return PartialView(research);
        }

        // GET: Researches/Delete/5
        public ActionResult pacientDelete(int? id)
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
            //Research Research = db.anamneses.Find(id);
            db.researches.Remove(research);
            db.SaveChanges();
            //return View(Research);
            return PartialView();
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
