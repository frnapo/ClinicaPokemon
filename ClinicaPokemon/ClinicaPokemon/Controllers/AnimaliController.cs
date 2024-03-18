using ClinicaPokemon.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ClinicaPokemon.Controllers
{
    [Authorize]
    public class AnimaliController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();

        // GET: Animali
        public ActionResult Index()
        {
            var animali = db.Animali.Include(a => a.Utenti);
            return View(animali.ToList());
        }

        // GET: Animali/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animali animali = db.Animali.Find(id);
            if (animali == null)
            {
                return HttpNotFound();
            }
            return View(animali);
        }

        // GET: Animali/Create
        public ActionResult Create()
        {
            ViewBag.FK_idUtente = new SelectList(db.Utenti.Select(u => new
            {
                idUtente = u.idUtente,
                NomeCompleto = u.Cognome + " " + u.Nome
            }), "idUtente", "NomeCompleto");
            return View();
        }

        // POST: Animali/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAnimale,Nome,Tipologia,Colore,DataNascita,Microchip,NrMicro,FK_idUtente")] Animali animali)
        {
            if (ModelState.IsValid)
            {
                db.Animali.Add(animali);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_idUtente = new SelectList(db.Utenti.Select(u => new
            {
                idUtente = u.idUtente,
                NomeCompleto = u.Cognome + " " + u.Nome
            }), "idUtente", "NomeCompleto", animali.FK_idUtente);

            return View(animali);
        }

        // GET: Animali/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animali animali = db.Animali.Find(id);
            if (animali == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_idUtente = new SelectList(db.Utenti, "idUtente", "Username", animali.FK_idUtente);
            return View(animali);
        }

        // POST: Animali/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAnimale,Nome,Tipologia,Colore,DataNascita,Microchip,NrMicro,FK_idUtente")] Animali animali)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animali).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_idUtente = new SelectList(db.Utenti, "idUtente", "Username", animali.FK_idUtente);
            return View(animali);
        }

        // GET: Animali/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animali animali = db.Animali.Find(id);
            if (animali == null)
            {
                return HttpNotFound();
            }
            return View(animali);
        }

        // POST: Animali/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Animali animali = db.Animali.Find(id);
            db.Animali.Remove(animali);
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
