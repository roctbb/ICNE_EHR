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
    public class NeurostatusController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: Neurostatus
        public ActionResult Index()
        {
            return View(db.neurostatuses.ToList());
        }

        // GET: Neurostatus/Details/5
        public ActionResult pacientDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Neurostatus neurostatus = db.neurostatuses.Include(p => p.type).Where(p => p.ID == id).First();
            if (neurostatus == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/views/Neurostatus/pacientDetails.cshtml", neurostatus);
        }

        // GET: Neurostatus/Create
        public ActionResult pacientCreate(int visitID, int num)
        {
            newNeurostatus na = new newNeurostatus();
            na.visitID = visitID;
            na.num = num;
            na.neurostatus = new Neurostatus();
            na.eventTypes = db.neuroStatusTypes.ToList();
            return PartialView(na);
        }

        // POST: Debuts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(newNeurostatus data)
        {
            VisitDate visit = db.visits.Include(v => v.neurostatuses).Where(v => v.ID == data.visitID).First();

            if (visit == null)
                return RedirectToAction("Index", "Pacients");

            Pacient pacient = db.Pacients.Where(p => p.visits.Any(v => v.ID == data.visitID)).First();
            if (pacient == null)
                return RedirectToAction("Index", "Pacients");

            if (ModelState.IsValid)
            {
                NeuroStatusType type = db.neuroStatusTypes.Where(a => a.ID == data.neurostatus.type.ID).First();
                data.neurostatus.type = type;
                visit.neurostatuses.Add(data.neurostatus);

                db.SaveChanges();
                return PartialView("/views/Neurostatus/pacientDetails.cshtml", data.neurostatus);
            }
            return PartialView("/views/Neurostatus/pacientCreate.cshtml", data);

        }

        // GET: Neurostatus/Edit/5
        public ActionResult pacientEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Neurostatus neurostatus = db.neurostatuses.Include(p => p.type).Where(p => p.ID == id).First();
            if (neurostatus == null)
            {
                return HttpNotFound();
            }
            return PartialView(neurostatus);
        }

        // POST: Neurostatus/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult pacientEdit([Bind(Include = "ID,description,comments")] Neurostatus neurostatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(neurostatus).State = EntityState.Modified;
                db.SaveChanges();
                return pacientDetails(neurostatus.ID);
            }
            return PartialView(neurostatus);
        }

        // GET: Neurostatus/Delete/5
        public ActionResult pacientDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Neurostatus neurostatus = db.neurostatuses.Find(id);
            if (neurostatus == null)
            {
                return HttpNotFound();
            }
            //Research Research = db.anamneses.Find(id);
            db.neurostatuses.Remove(neurostatus);
            db.SaveChanges();
            //return View(Research);
            return PartialView();
        }

        // POST: Neurostatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Neurostatus neurostatus = db.neurostatuses.Find(id);
            db.neurostatuses.Remove(neurostatus);
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
