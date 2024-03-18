using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicaPokemon.Models;

namespace ClinicaPokemon.Controllers
{
    public class RicoverisController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();

        // GET: Ricoveris
        public ActionResult Index()
        {
            var ricoveri = db.Ricoveri.Include(r => r.Animali);
            return View(ricoveri.ToList());
        }

        // GET: Ricoveris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ricoveri ricoveri = db.Ricoveri.Find(id);
            if (ricoveri == null)
            {
                return HttpNotFound();
            }
            return View(ricoveri);
        }

        // GET: Ricoveris/Create
        public ActionResult Create()
        {
            ViewBag.FK_idAnimale = new SelectList(db.Animali, "idAnimale", "Nome");
            return View();
        }

        // POST: Ricoveris/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idRicovero,FK_idAnimale,DataInizioRicovero,FotoAnimale,PrezzoRicovero,Attivo")] Ricoveri ricoveri)
        {
            if (ModelState.IsValid)
            {
                db.Ricoveri.Add(ricoveri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_idAnimale = new SelectList(db.Animali, "idAnimale", "Nome", ricoveri.FK_idAnimale);
            return View(ricoveri);
        }

        // GET: Ricoveris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ricoveri ricoveri = db.Ricoveri.Find(id);
            if (ricoveri == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_idAnimale = new SelectList(db.Animali, "idAnimale", "Nome", ricoveri.FK_idAnimale);
            return View(ricoveri);
        }

        // POST: Ricoveris/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idRicovero,FK_idAnimale,DataInizioRicovero,FotoAnimale,PrezzoRicovero,Attivo")] Ricoveri ricoveri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ricoveri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_idAnimale = new SelectList(db.Animali, "idAnimale", "Nome", ricoveri.FK_idAnimale);
            return View(ricoveri);
        }

        // GET: Ricoveris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ricoveri ricoveri = db.Ricoveri.Find(id);
            if (ricoveri == null)
            {
                return HttpNotFound();
            }
            return View(ricoveri);
        }

        // POST: Ricoveris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ricoveri ricoveri = db.Ricoveri.Find(id);
            db.Ricoveri.Remove(ricoveri);
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
