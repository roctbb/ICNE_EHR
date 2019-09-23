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
