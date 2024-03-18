using ClinicaPokemon.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ClinicaPokemon.Controllers
{
    [Authorize(Roles = "Veterinario")]
    public class VisiteController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();

        // GET: Visite
        public ActionResult Index(int? idAnimale)
        {
            var visite = db.Visite.Include(v => v.Animali);

            if (idAnimale.HasValue)
            {
                visite = visite.Where(v => v.FK_idAnimale == idAnimale.Value);
            }
            visite = visite.OrderByDescending(v => v.DataVisita);

            return View(visite.ToList());
        }

        // GET: Visite/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visite visite = db.Visite.Find(id);
            if (visite == null)
            {
                return HttpNotFound();
            }
            return View(visite);
        }

        // GET: Visite/Create
        public ActionResult Create(int? idAnimale)
        {
            if (idAnimale.HasValue)
            {
                ViewBag.FK_idAnimale = new SelectList(db.Animali, "idAnimale", "Nome", idAnimale.Value);
            }
            else
            {
                ViewBag.FK_idAnimale = new SelectList(db.Animali, "idAnimale", "Nome");
            }
            return View();
        }

        // POST: Visite/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idVisita,DataVisita,EsameObiettivo,DescrizioneCura,FK_idAnimale")] Visite visite)
        {
            if (ModelState.IsValid)
            {
                db.Visite.Add(visite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_idAnimale = new SelectList(db.Animali, "idAnimale", "Nome", visite.FK_idAnimale);
            return View(visite);
        }

        // GET: Visite/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visite visite = db.Visite.Find(id);
            if (visite == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_idAnimale = new SelectList(db.Animali, "idAnimale", "Nome", visite.FK_idAnimale);
            return View(visite);
        }

        // POST: Visite/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idVisita,DataVisita,EsameObiettivo,DescrizioneCura,FK_idAnimale")] Visite visite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_idAnimale = new SelectList(db.Animali, "idAnimale", "Nome", visite.FK_idAnimale);
            return View(visite);
        }

        // GET: Visite/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visite visite = db.Visite.Find(id);
            if (visite == null)
            {
                return HttpNotFound();
            }
            return View(visite);
        }

        // POST: Visite/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visite visite = db.Visite.Find(id);
            db.Visite.Remove(visite);
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
