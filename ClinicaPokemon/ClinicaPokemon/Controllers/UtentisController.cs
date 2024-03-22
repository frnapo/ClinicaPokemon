using ClinicaPokemon.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ClinicaPokemon.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UtentisController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();


        public ActionResult Index()
        {
            return View(db.Utenti.ToList());
        }

        public ActionResult Promuovi(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utenti utente = db.Utenti.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            if (utente.Ruolo < 4)
            {
                utente.Ruolo += 1;
            }
            db.Entry(utente).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Declassa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utenti utente = db.Utenti.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            if (utente.Ruolo > 1)
            {
                utente.Ruolo -= 1;
            }
            db.Entry(utente).State = EntityState.Modified;
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
