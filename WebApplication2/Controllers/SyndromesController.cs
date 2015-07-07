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
    public class SyndromesController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: Syndromes
        public ActionResult Index()
        {
            return View(db.syndromes.ToList());
        }

        // GET: Syndromes/Details/5
        public ActionResult pacientDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Syndrome syndrome = db.syndromes.Include(p => p.type).Where(p => p.ID == id).First();
            if (syndrome == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/views/Syndromes/pacientDetails.cshtml", syndrome);
        }

        // GET: Syndromes/Create
        public ActionResult pacientCreate(int visitID, int num)
        {
            newSyndrome na = new newSyndrome();
            na.visitID = visitID;
            na.num = num;
            na.syndrome = new Syndrome();
            na.eventTypes = db.syndromeTypes.ToList();
            return PartialView(na);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(newSyndrome data)
        {
            VisitDate visit = db.visits.Include(v => v.syndromes).Where(v => v.ID == data.visitID).First();

            if (visit == null)
                return RedirectToAction("Index", "Pacients");

            Pacient pacient = db.pacients.Where(p => p.visits.Any(v => v.ID == data.visitID)).First();
            if (pacient == null)
                return RedirectToAction("Index", "Pacients");

            if (ModelState.IsValid)
            {
                SyndromeType type = db.syndromeTypes.Where(a => a.ID == data.syndrome.type.ID).First();
                data.syndrome.type = type;
                visit.syndromes.Add(data.syndrome);

                db.SaveChanges();
                return PartialView("/views/Syndromes/pacientDetails.cshtml", data.syndrome);
            }
            return PartialView("/views/Syndromes/pacientCreate.cshtml", data);

        }
        public ActionResult pacientCreateByDate(int id)
        {
            newSyndrome na = new newSyndrome();
            na.syndrome = new Syndrome();
            na.eventTypes = db.syndromeTypes.ToList();
            na.pacientID = id;
            return PartialView(na);
        }
        public ActionResult CreateByDate(newSyndrome data)
        {
            Pacient pacient = db.pacients.Include(p => p.visits.Select(v => v.syndromes)).Include(p => p.doctor).Where(p => p.ID == data.pacientID).First();
            if (pacient == null)
                return RedirectToAction("Index", "Pacients");
            VisitDate visit = pacient.visits.Where(v => v.date == data.initialDate).FirstOrDefault();
            if (visit != null)
            {
                SyndromeType type = db.syndromeTypes.Where(a => a.ID == data.syndrome.type.ID).First();
                data.syndrome.type = type;
                visit.syndromes.Add(data.syndrome);
                db.SaveChanges();
                return PartialView("/views/Syndromes/pacientDetails.cshtml", data.syndrome);
            }
            else
            {
                SyndromeType type = db.syndromeTypes.Where(a => a.ID == data.syndrome.type.ID).First();
                data.syndrome.type = type;
                visit = new VisitDate();
                visit.doctorID = pacient.doctor.ID;
                visit.date = data.initialDate;
                visit.syndromes = new List<Syndrome>();
                visit.syndromes.Add(data.syndrome);
                pacient.visits.Add(visit);
                db.SaveChanges();
                return PartialView("/views/Syndromes/pacientDetails.cshtml", data.syndrome);

            }
            //return PartialView("/views/Syndrome/pacientCreate.cshtml", data);

        }

        // GET: Syndromes/Edit/5
        public ActionResult pacientEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Syndrome syndrome = db.syndromes.Include(p => p.type).Where(p => p.ID == id).First();
            if (syndrome == null)
            {
                return HttpNotFound();
            }
            return PartialView(syndrome);
        }

        // POST: Syndromes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult pacientEdit([Bind(Include = "ID,symptomes,comments,month,year,week,day,minutes,seconds")] Syndrome syndrome)
        {
            if (ModelState.IsValid)
            {
                db.Entry(syndrome).State = EntityState.Modified;
                db.SaveChanges();
                return pacientDetails(syndrome.ID); ;
            }
            return View(syndrome);
        }

        public ActionResult pacientDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Syndrome syndrome = db.syndromes.Find(id);
            if (syndrome == null)
            {
                return HttpNotFound();
            }
            //Syndrom Syndrom = db.anamneses.Find(id);
            db.syndromes.Remove(syndrome);
            db.SaveChanges();
            //return View(Syndrom);
            return PartialView();
        }

        // POST: Syndromes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Syndrome syndrome = db.syndromes.Find(id);
            db.syndromes.Remove(syndrome);
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
