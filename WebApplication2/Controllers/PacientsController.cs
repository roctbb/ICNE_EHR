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
    public class PacientsController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: Pacients
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchByName(String name = "", String mode = "name")
        {
            
            if (mode.Equals("name"))
                return PartialView(db.Pacients.Where(p => p.name.Contains(name)).ToList());
            else
            {
                var results = db.Pacients.Where(p => p.visits.Any(vd => vd.reviews.Any(r => r.comments.ToLower().Contains(name.ToLower()))));
                return PartialView(results.ToList());
            }
        }
        // GET: Pacients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // db.
            Pacient pacient = db.Pacients
                 .Include(p => p.visits.Select(w => w.anamnesis.Select(r=>r.type)))
                 .Include(p => p.visits.Select(w => w.debutes.Select(r => r.type)))
                 .Include(p => p.visits.Select(w => w.diagnoses.Select(r => r.type)))
                 .Include(p => p.visits.Select(w => w.researches.Select(r => r.type)))
                 .Include(p => p.visits.Select(w => w.anamnesis.Select(r => r.type)))
                 .Include(p => p.visits.Select(w => w.neurostatuses.Select(r => r.type)))
                 .Include(p => p.visits.Select(w => w.assigments.Select(r => r.type)))
                 .Include(p => p.visits.Select(w => w.syndromes.Select(r => r.type)))
                 .Include(p => p.visits.Select(w => w.reviews))
                 .Where(p=>p.ID == id).Single();
            pacient.visits.Sort(delegate (VisitDate t1, VisitDate t2) { return t2.date.CompareTo(t1.date); });
            return View(pacient);
        }

        // GET: Pacients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pacients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,doctor,name,cart,phone,dateOfregistration,sex,birthday,mother,father,adress,weight,comments")] Pacient pacient)
        {
            if (ModelState.IsValid)
            {
                db.Pacients.Add(pacient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pacient);
        }

        // GET: Pacients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacient pacient = db.Pacients.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            return View(pacient);
        }

        // POST: Pacients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,doctor,name,cart,phone,dateOfregistration,sex,birthday,mother,father,adress,weight,comments")] Pacient pacient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pacient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pacient);
        }

        // GET: Pacients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacient pacient = db.Pacients.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            return View(pacient);
        }

        // POST: Pacients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pacient pacient = db.Pacients.Find(id);
            db.Pacients.Remove(pacient);
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
