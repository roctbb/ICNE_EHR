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
    public class DebutsController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: Debuts
        public ActionResult Index()
        {
            return View(db.debutes.ToList());
        }

        // GET: Debuts/Details/5
        public ActionResult pacientDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Debut debut = db.debutes.Include(d=>d.type).Where(d=>d.ID == id).First();
            if (debut == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/views/Debuts/pacientDetails.cshtml", debut);
        }

        // GET: Debuts/Create
        public ActionResult pacientCreate(int visitID, int num)
        {
            newDebut na = new newDebut();
            na.visitID = visitID;
            na.num = num;
            na.debut = new Debut();
            na.eventTypes = db.debuteTypes.ToList();
            return PartialView(na);
        }

        // POST: Debuts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(newDebut data)
        {
            VisitDate visit = db.visits.Include(v => v.debutes).Where(v => v.ID == data.visitID).First();

            if (visit == null)
                return RedirectToAction("Index", "Pacients");

            Pacient pacient = db.pacients.Where(p => p.visits.Any(v => v.ID == data.visitID)).First();
            if (pacient == null)
                return RedirectToAction("Index", "Pacients");

            if (ModelState.IsValid)
            {
                DebutType type = db.debuteTypes.Where(a => a.ID == data.debut.type.ID).First();
                data.debut.type = type;
                visit.debutes.Add(data.debut);

                db.SaveChanges();
                return PartialView("/views/Debuts/pacientDetails.cshtml", data.debut);
            }
            return PartialView("/views/Debuts/pacientCreate.cshtml", data);

        }

        // GET: Debuts/Edit/5
        public ActionResult pacientEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Debut debut = db.debutes.Include(d => d.type).Where(d => d.ID == id).First();
            if (debut == null)
            {
                return HttpNotFound();
            }
            return PartialView(debut);
        }

        // POST: Debuts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult pacientEdit([Bind(Include = "ID,comments,description,month,year,minutes,seconds")] Debut debut)
        {
            if (ModelState.IsValid)
            {
                db.Entry(debut).State = EntityState.Modified;
                db.SaveChanges();
                return pacientDetails(debut.ID);
            }
            return View(debut);
        }

        // GET: Debuts/Delete/5
        public ActionResult pacientDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Debut debut = db.debutes.Find(id);
            if (debut == null)
            {
                return HttpNotFound();
            }
            //Debut Debut = db.anamneses.Find(id);
            db.debutes.Remove(debut);
            db.SaveChanges();
            //return View(Debut);
            return PartialView();
        }

        // POST: Debuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Debut debut = db.debutes.Find(id);
            db.debutes.Remove(debut);
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
