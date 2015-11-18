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
    public class AnalysisTypesController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: AnalysisTypes
        public ActionResult Index()
        {
            return View(db.analysisTypes.ToList());
        }

        // GET: AnalysisTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnalysisType analysisType = db.analysisTypes.Find(id);
            if (analysisType == null)
            {
                return HttpNotFound();
            }
            return View(analysisType);
        }

        // GET: AnalysisTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnalysisTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name")] AnalysisType analysisType)
        {
            if (ModelState.IsValid)
            {
                db.analysisTypes.Add(analysisType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(analysisType);
        }

        // GET: AnalysisTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnalysisType analysisType = db.analysisTypes.Find(id);
            if (analysisType == null)
            {
                return HttpNotFound();
            }
            return View(analysisType);
        }

        // POST: AnalysisTypes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name")] AnalysisType analysisType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(analysisType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(analysisType);
        }

        // GET: AnalysisTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnalysisType analysisType = db.analysisTypes.Find(id);
            if (analysisType == null)
            {
                return HttpNotFound();
            }
            return View(analysisType);
        }

        // POST: AnalysisTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnalysisType analysisType = db.analysisTypes.Find(id);
            db.analysisTypes.Remove(analysisType);
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
