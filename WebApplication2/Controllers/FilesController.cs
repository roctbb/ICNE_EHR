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
    public class FilesController : Controller
    {
        private PacientDBContext db = new PacientDBContext();

        
        // GET: Diagnoses/Details/5
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Files file = db.Files.Where(p => p.ID == id).First();
            if (file == null)
            {
                return HttpNotFound();
            }
            return File(file.Content, file.ContentType, file.FileName);
        }

        public ActionResult pacientCreateByDate(int id)
        {
            newFile nf = new newFile();
            nf.pacientID = id;
            return PartialView(nf);
        }

        [HttpPost]
        public ActionResult Create(newFile data)
        {

            Pacient pacient = db.pacients.Include(p => p.visits.Select(v => v.diagnoses)).Include(p => p.doctor).Where(p => p.ID == data.pacientID).First();
            if (pacient == null)
                return RedirectToAction("Index", "Pacients");
            VisitDate visit = pacient.visits.Where(v => v.date == data.initialDate).FirstOrDefault();
            
            if (visit == null)
            {
                visit = new VisitDate();
                visit.doctorID = pacient.doctor.ID;
                visit.date = data.initialDate;
                visit.files = new List<Files>();
                pacient.visits.Add(visit);
                db.SaveChanges();

            }

            if (data.FileAttach != null)
            {
                Files nf = new Files();
                byte[] uploadedFile = new byte[data.FileAttach.InputStream.Length];
                data.FileAttach.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                nf.FileName = data.FileAttach.FileName;
                nf.ContentType = data.FileAttach.ContentType;
                nf.Content = uploadedFile;

                visit.files.Add(nf);
                db.SaveChanges();
            }
            return Redirect("/Pacients/Details/" + data.pacientID.ToString() + "#FilesTab");
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult pacientDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Files file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            //Diagnosis Diagnosis = db.anamneses.Find(id);
            db.Files.Remove(file);
            db.SaveChanges();
            //return View(Diagnosis);
            return PartialView();
        }
    }
}
