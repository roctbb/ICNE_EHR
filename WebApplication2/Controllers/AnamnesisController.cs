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
    public class AnamnesisController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: Anamnesis
        public ActionResult Index()
        {
            return View(db.anamneses.ToList());
        }

        // GET: Anamnesis/Details/5
        public ActionResult pacientDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anamnesis anamnesis = db.anamneses.Include(p => p.type).Where(p => p.ID == id).First();
            if (anamnesis == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/views/Anamnesis/pacientDetails.cshtml", anamnesis);
        }

        // GET: Anamnesis/Create
        public ActionResult pacientCreate(int visitID, int num)
        {
            newAnamnesis na = new newAnamnesis();
            na.visitID = visitID;
            na.num = num;
            na.anamnesis = new Anamnesis();
            na.eventTypes = db.anamnesisTypes.ToList();
            return PartialView(na);
        }
        public ActionResult Create(newAnamnesis data)
        {
            VisitDate visit = db.visits.Include(v => v.anamnesis).Where(v => v.ID == data.visitID).First();

            if (visit == null)
                return RedirectToAction("Index", "Pacients");

            Pacient pacient = db.pacients.Where(p => p.visits.Any(v => v.ID == data.visitID)).First();
            if (pacient == null)
                return RedirectToAction("Index", "Pacients");

            if (ModelState.IsValid)
            {
                AnamnesisEventType type = db.anamnesisTypes.Where(a => a.ID == data.anamnesis.type.ID).First();
                data.anamnesis.type = type;
                visit.anamnesis.Add(data.anamnesis);
                
                db.SaveChanges();
                return PartialView("/views/Anamnesis/pacientDetails.cshtml", data.anamnesis);
            }
            return PartialView("/views/Anamnesis/pacientCreate.cshtml", data);

        }
        

        // GET: Anamnesis/Edit/5
        public ActionResult pacientEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anamnesis anamnesis = db.anamneses.Include(p=>p.type).Where(p=>p.ID == id).First();
            
            if (anamnesis == null)
            {
                return HttpNotFound();
            }
            return PartialView(anamnesis);
        }

        // POST: Anamnesis/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult pacientEdit([Bind(Include = "ID,comments")] Anamnesis anamnesis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anamnesis).State = EntityState.Modified;
                db.SaveChanges();
                return pacientDetails(anamnesis.ID);
            }
            return PartialView(anamnesis);
        }

        // GET: Anamnesis/Delete/5
        public ActionResult pacientDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anamnesis anamnesis = db.anamneses.Find(id);
            if (anamnesis == null)
            {
                return HttpNotFound();
            }
            //Anamnesis anamnesis = db.anamneses.Find(id);
            db.anamneses.Remove(anamnesis);
            db.SaveChanges();
            //return View(anamnesis);
            return PartialView();
        }

        // POST: Anamnesis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anamnesis anamnesis = db.anamneses.Find(id);
            db.anamneses.Remove(anamnesis);
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
