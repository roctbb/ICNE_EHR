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
        public ActionResult pacientCreate(int visitID, int num)
        {
            newReview na = new newReview();
            na.visitID = visitID;
            na.num = num;
            na.review = new Review();
            return PartialView(na);
        }

        // POST: Reviews/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(newReview data)
        {
            VisitDate visit = db.visits.Include(v => v.reviews).Where(v => v.ID == data.visitID).First();

            if (visit == null)
                return RedirectToAction("Index", "Pacients");

            Pacient pacient = db.pacients.Where(p => p.visits.Any(v => v.ID == data.visitID)).First();
            if (pacient == null)
                return RedirectToAction("Index", "Pacients");

            if (ModelState.IsValid)
            {
             
                visit.reviews.Add(data.review);

                db.SaveChanges();
                return PartialView("/views/Reviews/pacientDetails.cshtml", data.review);
            }
            return PartialView("/views/Reviews/pacientCreate.cshtml", data);

        }
        public ActionResult pacientCreateByDate(int id)
        {
            newReview na = new newReview();
            na.review = new Review();
            na.pacientID = id;
            return PartialView(na);
        }
        public ActionResult CreateByDate(newReview data)
        {
            Pacient pacient = db.pacients.Include(p => p.visits.Select(v => v.reviews)).Include(p => p.doctor).Where(p => p.ID == data.pacientID).First();
            if (pacient == null)
                return RedirectToAction("Index", "Pacients");
            VisitDate visit = pacient.visits.Where(v => v.date == data.initialDate).FirstOrDefault();
            if (visit != null)
            {
                visit.reviews.Add(data.review);
                db.SaveChanges();
                return PartialView("/views/Reviews/pacientDetails.cshtml", data.review);
            }
            else
            {
                visit = new VisitDate();
                visit.doctorID = pacient.doctor.ID;
                visit.date = data.initialDate;
                visit.reviews = new List<Review>();
                visit.reviews.Add(data.review);
                pacient.visits.Add(visit);
                db.SaveChanges();
                return PartialView("/views/Reviews/pacientDetails.cshtml", data.review);

            }
            //return PartialView("/views/Review/pacientCreate.cshtml", data);

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
        public ActionResult pacientDelete(int? id)
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
            //Research Research = db.anamneses.Find(id);
            db.reviews.Remove(review);
            db.SaveChanges();
            //return View(Research);
            return PartialView();
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
