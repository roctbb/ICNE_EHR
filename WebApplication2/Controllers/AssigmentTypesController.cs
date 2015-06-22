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
    public class AssigmentTypesController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: AssigmentTypes
        public ActionResult Index()
        {
            return View(db.assigmentTypes.ToList());
        }

        // GET: AssigmentTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssigmentType assigmentType = db.assigmentTypes.Find(id);
            if (assigmentType == null)
            {
                return HttpNotFound();
            }
            return View(assigmentType);
        }

        // GET: AssigmentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssigmentTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,description")] AssigmentType assigmentType)
        {
            if (ModelState.IsValid)
            {
                db.assigmentTypes.Add(assigmentType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assigmentType);
        }

        // GET: AssigmentTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssigmentType assigmentType = db.assigmentTypes.Find(id);
            if (assigmentType == null)
            {
                return HttpNotFound();
            }
            return View(assigmentType);
        }

        // POST: AssigmentTypes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,description")] AssigmentType assigmentType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assigmentType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assigmentType);
        }

        // GET: AssigmentTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssigmentType assigmentType = db.assigmentTypes.Find(id);
            if (assigmentType == null)
            {
                return HttpNotFound();
            }
            return View(assigmentType);
        }

        // POST: AssigmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssigmentType assigmentType = db.assigmentTypes.Find(id);
            db.assigmentTypes.Remove(assigmentType);
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
