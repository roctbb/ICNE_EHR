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
    public class MedicineTypesController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: MedicineTypes
        public ActionResult Index()
        {
            return View(db.medicineTypes.ToList());
        }

        // GET: MedicineTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineType medicineType = db.medicineTypes.Find(id);
            if (medicineType == null)
            {
                return HttpNotFound();
            }
            return View(medicineType);
        }

        // GET: MedicineTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicineTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,description")] MedicineType medicineType)
        {
            if (ModelState.IsValid)
            {
                db.medicineTypes.Add(medicineType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medicineType);
        }

        // GET: MedicineTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineType medicineType = db.medicineTypes.Find(id);
            if (medicineType == null)
            {
                return HttpNotFound();
            }
            return View(medicineType);
        }

        // POST: MedicineTypes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,description")] MedicineType medicineType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicineType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medicineType);
        }

        // GET: MedicineTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineType medicineType = db.medicineTypes.Find(id);
            if (medicineType == null)
            {
                return HttpNotFound();
            }
            return View(medicineType);
        }

        // POST: MedicineTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MedicineType medicineType = db.medicineTypes.Find(id);
            db.medicineTypes.Remove(medicineType);
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
