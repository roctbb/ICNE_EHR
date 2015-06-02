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
            Pacient pacient = db.Pacients.Include(p => p.visits).Where(p => p.ID == id).First();
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
            VisitDate visitDate = db.visits.Find(id);
            if (visitDate == null)
            {
                return HttpNotFound();
            }
            return View(visitDate);
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
