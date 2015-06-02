﻿using System;
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
    public class ReviewsController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        // GET: Reviews
        public ActionResult Index()
        {
            return View(db.reviews.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult pacientDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/views/Reviews/pacientDetails.cshtml", review);
        }


        // POST: Reviews/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? visitID, Review review)
        {
            if (visitID == null)
                return RedirectToAction("Index", "Pacients");

            VisitDate visit = db.visits.Include(v=>v.reviews).Where(v => v.ID == visitID).First();

            if (visit==null )
                return RedirectToAction("Index", "Pacients");

            Pacient pacient = db.Pacients.Where(p => p.visits.Any(v => v.ID == visitID)).First();
            if (pacient == null)
                return RedirectToAction("Index", "Pacients");
            if (ModelState.IsValid)
            {
                visit.reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Details", "Pacients", new { id = pacient.ID });
            }
            return RedirectToAction("Details","Pacients", new {id=pacient.ID });
            
        }

        // GET: Reviews/Edit/5
        public ActionResult pacientEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return PartialView(review);
        }

        // POST: Reviews/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult pacientEdit([Bind(Include = "ID,comments")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return pacientDetails(review.ID);
            }
            return PartialView(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.reviews.Find(id);
            db.reviews.Remove(review);
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
