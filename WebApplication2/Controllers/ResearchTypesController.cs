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
    public class ResearchTypesController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: ResearchTypes
        public ActionResult Index()
        {
            return View(db.researchTypes.ToList());
        }

        // GET: ResearchTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResearchType researchType = db.researchTypes.Find(id);
            if (researchType == null)
            {
                return HttpNotFound();
            }
            return View(researchType);
        }

        // GET: ResearchTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResearchTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,description")] ResearchType researchType)
        {
            if (ModelState.IsValid)
            {
                db.researchTypes.Add(researchType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(researchType);
        }

        // GET: ResearchTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResearchType researchType = db.researchTypes.Find(id);
            if (researchType == null)
            {
                return HttpNotFound();
            }
            return View(researchType);
        }

        // POST: ResearchTypes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,description")] ResearchType researchType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(researchType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(researchType);
        }

        // GET: ResearchTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResearchType researchType = db.researchTypes.Find(id);
            if (researchType == null)
            {
                return HttpNotFound();
            }
            return View(researchType);
        }

        // POST: ResearchTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResearchType researchType = db.researchTypes.Find(id);
            db.researchTypes.Remove(researchType);
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
