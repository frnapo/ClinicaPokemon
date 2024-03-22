using ClinicaPokemon.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ClinicaPokemon.Controllers
{
    public class DettagliVenditaController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();

        public ActionResult Index()
        {
            var dettagliVendita = db.DettagliVendita.Include(d => d.Prodotti).Include(d => d.Vendite);
            return View(dettagliVendita.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DettagliVendita dettagliVendita = db.DettagliVendita.Find(id);
            if (dettagliVendita == null)
            {
                return HttpNotFound();
            }
            return View(dettagliVendita);
        }

        public ActionResult Create()
        {
            ViewBag.FK_idProdotto = new SelectList(db.Prodotti, "idProdotto", "NomeProdotto");
            ViewBag.FK_idVendita = new SelectList(db.Vendite, "idVendita", "idVendita");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDettagli,FK_idProdotto,FK_idVendita,Quantita")] DettagliVendita dettagliVendita)
        {
            if (ModelState.IsValid)
            {
                db.DettagliVendita.Add(dettagliVendita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_idProdotto = new SelectList(db.Prodotti, "idProdotto", "NomeProdotto", dettagliVendita.FK_idProdotto);
            ViewBag.FK_idVendita = new SelectList(db.Vendite, "idVendita", "idVendita", dettagliVendita.FK_idVendita);
            return View(dettagliVendita);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DettagliVendita dettagliVendita = db.DettagliVendita.Find(id);
            if (dettagliVendita == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_idProdotto = new SelectList(db.Prodotti, "idProdotto", "NomeProdotto", dettagliVendita.FK_idProdotto);
            ViewBag.FK_idVendita = new SelectList(db.Vendite, "idVendita", "idVendita", dettagliVendita.FK_idVendita);
            return View(dettagliVendita);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDettagli,FK_idProdotto,FK_idVendita,Quantita")] DettagliVendita dettagliVendita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dettagliVendita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_idProdotto = new SelectList(db.Prodotti, "idProdotto", "NomeProdotto", dettagliVendita.FK_idProdotto);
            ViewBag.FK_idVendita = new SelectList(db.Vendite, "idVendita", "idVendita", dettagliVendita.FK_idVendita);
            return View(dettagliVendita);
        }

        // GET: DettagliVendita/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DettagliVendita dettagliVendita = db.DettagliVendita.Find(id);
            if (dettagliVendita == null)
            {
                return HttpNotFound();
            }
            return View(dettagliVendita);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DettagliVendita dettagliVendita = db.DettagliVendita.Find(id);
            db.DettagliVendita.Remove(dettagliVendita);
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

        [HttpGet]

        public async Task<ActionResult> ProdottiPerData(DateTime DataVendita)
        {
            var search = await db.DettagliVendita
                .Include(v => v.Vendite)
                .Include(v => v.Prodotti)
                .Include(v => v.FK_idVendita)
                .Where(v => v.Vendite.DataVendita == DataVendita)
                .Select(v => new { v.Prodotti.NomeProdotto, v.Vendite.DataVendita, v.FK_idVendita })
                .ToListAsync();

            return Json(search, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> ProdottiPerCodFiscale(string codFiscale)
        {
            var search = await db.DettagliVendita
                .Include(d => d.Vendite)
                .Include(d => d.Vendite.Utenti)
                .Include(d => d.Prodotti)
                .Where(d => d.Vendite.Utenti.CodFiscale == codFiscale)
                .Select(d => new { d.Prodotti.NomeProdotto })
                .ToListAsync();

            return Json(search, JsonRequestBehavior.AllowGet);
        }
    }
}
