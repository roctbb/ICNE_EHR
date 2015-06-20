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
    public class VisitDatesController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: VisitDates
        public ActionResult Index()
        {
            return View(db.visits.ToList());
        }

        // GET: VisitDates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitDate visitDate = db.visits.Find(id);
            if (visitDate == null)
            {
                return HttpNotFound();
            }
            return View(visitDate);
        }

        // GET: VisitDates/Create

        // POST: VisitDates/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        
        public ActionResult Create(int? id)
        {
            if (id==null)
            {
                return RedirectToAction("Index", "Pacients");
            }
            Pacient pacient = db.pacients.Include(p => p.visits).Where(p => p.ID == id).First();
            if (pacient.visits.Any(p=>p.date == DateTime.Today))
                return RedirectToAction("Details", "Pacients", new {id=id });
            VisitDate visitDate = new VisitDate();
            visitDate.date = DateTime.Today;
            visitDate.doctorID = 1;
            db.visits.Add(visitDate);
            pacient.visits.Add(visitDate);
            db.SaveChanges();
            return RedirectToAction("Details", "Pacients", new { id = id });

        }

        // GET: VisitDates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitDate visitDate = db.visits.Find(id);
            if (visitDate == null)
            {
                return HttpNotFound();
            }
            return View(visitDate);
        }

        // POST: VisitDates/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,doctorID,date")] VisitDate visitDate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visitDate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(visitDate);
        }

        // GET: VisitDates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitDate visitDate = db.visits.Include(v=>v.anamnesis).Include(v=>v.assigments).Include(v=>v.debutes)
                .Include(v=>v.diagnoses).Include(v => v.neurostatuses).Include(v => v.researches)
                .Include(v => v.reviews).Include(v => v.syndromes).Where(v=>v.ID==id).First();
            Pacient pacient = db.pacients.Where(p=>p.visits.Any(v=>v.ID == visitDate.ID)).First();
            if (visitDate == null || pacient ==null)
            {
                return HttpNotFound();
            }
            foreach (var item in visitDate.neurostatuses.ToList())
            {
                db.neurostatuses.Remove(item);
            }
            foreach (var item in visitDate.reviews.ToList())
            {
                db.reviews.Remove(item);
            }
            foreach (var item in visitDate.researches.ToList())
            {
                db.researches.Remove(item);
            }
            foreach (var item in visitDate.syndromes.ToList())
            {
                db.syndromes.Remove(item);
            }
            foreach (var item in visitDate.diagnoses.ToList())
            {
                db.diagnoses.Remove(item);
            }
            foreach (var item in visitDate.debutes.ToList())
            {
                db.debutes.Remove(item);
            }
            foreach (var item in visitDate.assigments.ToList())
            {
                db.assigments.Remove(item);
            }
            foreach (var item in visitDate.anamnesis.ToList())
            {
                db.anamneses.Remove(item);
            }
            db.visits.Remove(visitDate);
            db.SaveChanges();

            return RedirectToAction("Details", "Pacients", new { id = pacient.ID});
        }

        // POST: VisitDates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VisitDate visitDate = db.visits.Find(id);
            db.visits.Remove(visitDate);
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
