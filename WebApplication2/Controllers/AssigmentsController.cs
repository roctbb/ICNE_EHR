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
    public class AssigmentsController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: Assigments
        public ActionResult Index()
        {
            return View(db.assigments.ToList());
        }

        // GET: Assigments/Details/5
        public ActionResult pacientDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assigment assigment = db.assigments.Include(p => p.type).Where(p => p.ID == id).First();
            if (assigment == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/views/Assigments/pacientDetails.cshtml", assigment);
        }

        // GET: Assigments/Create
        public ActionResult pacientCreate(int visitID, int num)
        {
            newAssigment na = new newAssigment();
            na.visitID = visitID;
            na.num = num;
            na.assigment = new Assigment();
            na.eventTypes = db.assigmentTypes.ToList();
            return PartialView(na);
        }

        // POST: Assigments/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(newAssigment data)
        {
            VisitDate visit = db.visits.Include(v => v.assigments).Where(v => v.ID == data.visitID).First();

            if (visit == null)
                return RedirectToAction("Index", "Pacients");

            Pacient pacient = db.Pacients.Where(p => p.visits.Any(v => v.ID == data.visitID)).First();
            if (pacient == null)
                return RedirectToAction("Index", "Pacients");

            if (ModelState.IsValid)
            {
                AssigmentType type = db.assigmentTypes.Where(a => a.ID == data.assigment.type.ID).First();
                data.assigment.type = type;
                data.assigment.cancelDate = DateTime.Today;
                visit.assigments.Add(data.assigment);

                db.SaveChanges();
                return PartialView("/views/Assigments/pacientDetails.cshtml", data.assigment);
            }
            return PartialView("/views/Assigments/pacientCreate.cshtml", data);

        }

        // GET: Assigments/Edit/5
        public ActionResult pacientEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assigment assigment = db.assigments.Include(p => p.type).Where(p => p.ID == id).First();
            if (assigment == null)
            {
                return HttpNotFound();
            }
            return PartialView(assigment);
        }

        // POST: Assigments/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult pacientEdit( Assigment assigment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assigment).State = EntityState.Modified;

                db.SaveChanges();
                return pacientDetails(assigment.ID);
            }
            return PartialView(assigment);
        }

        // GET: Assigments/Delete/5
        public ActionResult pacientDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assigment assigment = db.assigments.Find(id);
            if (assigment == null)
            {
                return HttpNotFound();
            }
            //Debut Debut = db.anamneses.Find(id);
            db.assigments.Remove(assigment);
            db.SaveChanges();
            //return View(Debut);
            return PartialView();
        }
        public ActionResult pacientCancel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assigment assigment = db.assigments.Find(id);
            if (assigment == null)
            {
                return HttpNotFound();
            }
            //Debut Debut = db.anamneses.Find(id);
            assigment.actual = 1;
            assigment.cancelDate = DateTime.Today;
            db.SaveChanges();
            //return View(Debut);
            return PartialView();
        }
        // POST: Assigments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assigment assigment = db.assigments.Find(id);
            db.assigments.Remove(assigment);
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
