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
                return PartialView(db.pacients.Where(p => p.name.Contains(name)).ToList());
            else
            {
                var results = db.pacients.Where(p => p.visits.Any(vd => vd.reviews.Any(r => r.comments.ToLower().Contains(name.ToLower()))));
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
            //db.
            Pacient pacient = db.pacients
                .Include(p=>p.doctor)
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
            newPacient na = new newPacient();
            na.pacient = new Pacient();
            na.doctors = db.doctors.ToList();
            return View(na);
        }

        // POST: Pacients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(newPacient data)
        {
            data.pacient.dateOfregistration = DateTime.Today;
            Doctor d = db.doctors.Where(t => t.ID == data.pacient.doctor.ID).First();
            if (d==null)
                return RedirectToAction("Index", "Pacients");
            data.pacient.doctor = d;
            if (ModelState.IsValid)
            {
                db.pacients.Add(data.pacient);
                db.SaveChanges();
                return RedirectToAction("Details", new {id=data.pacient.ID});
            }

            return View(data);
        }

        // GET: Pacients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacient pacient = db.pacients.Find(id);
            newPacient na = new newPacient();
            na.pacient = pacient;
            
            if (pacient == null)
            {
                return HttpNotFound();
            }
            na.doctors = db.doctors.ToList();
            return View(na);
        }

        // POST: Pacients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(newPacient data)
        {

            Doctor d = db.doctors.Where(t => t.ID == data.pacient.doctor.ID).First();
            Pacient pc = db.pacients.Find(data.pacient.ID);
            
            if (d == null)
                return RedirectToAction("Index", "Pacients");
            //pc = data.pacient;
            //pc.doctor = dr;
            if (ModelState.IsValid)
            {

                pc.adress = data.pacient.adress;
                pc.birthday = data.pacient.birthday;
                pc.cart = data.pacient.cart;
                pc.comments = data.pacient.comments;
                pc.doctor = d;
                pc.father = data.pacient.father;
                pc.mother = data.pacient.mother;
                pc.name = data.pacient.name;
                pc.phone = data.pacient.phone;
                pc.sex = data.pacient.sex;
                db.SaveChanges();
                
            }
            return RedirectToAction("Details", new {id=pc.ID });
        }

        // GET: Pacients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacient pacient = db.pacients.Include(p=>p.visits).Where(p=>p.ID == id).First();
            if (pacient == null)
            {
                return HttpNotFound();
            }
            VisitDatesController vdt = new VisitDatesController();
            foreach (var item in pacient.visits.ToList())
            {
                vdt.Delete(item.ID);
            }
            db.pacients.Remove(pacient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Pacients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pacient pacient = db.pacients.Find(id);
            db.pacients.Remove(pacient);
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
