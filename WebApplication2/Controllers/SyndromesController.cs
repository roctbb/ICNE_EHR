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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,symptomes,comments,month,year,week,day,minutes,seconds")] Syndrome syndrome)
        {
            if (ModelState.IsValid)
            {
                db.syndromes.Add(syndrome);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(syndrome);
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

        // GET: Syndromes/Delete/5
        public ActionResult Delete(int? id)
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
            return View(syndrome);
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
