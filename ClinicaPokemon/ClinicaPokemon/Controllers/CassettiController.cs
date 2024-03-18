﻿using ClinicaPokemon.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ClinicaPokemon.Controllers
{
    public class CassettiController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();

        // GET: Cassetti

        public ActionResult Index(int? FK_idArmadietto)
        {
            if (FK_idArmadietto == null)
            {
                return View(db.Cassetti.ToList());
            }
            else
            {
                var cassetti = db.Cassetti.Where(c => c.FK_idArmadietto == FK_idArmadietto);
                return View(cassetti.ToList());
            }
        }

        //public ActionResult Index()
        //{
        //    var cassetti = db.Cassetti.Include(c => c.Armadietti);
        //    return View(cassetti.ToList());
        //}

        // GET: Cassetti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cassetti cassetti = db.Cassetti.Find(id);
            if (cassetti == null)
            {
                return HttpNotFound();
            }
            return View(cassetti);
        }

        // GET: Cassetti/Create
        public ActionResult Create()
        {
            ViewBag.FK_idArmadietto = new SelectList(db.Armadietti, "idArmadietto", "idArmadietto");
            return View();
        }

        // POST: Cassetti/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCassetto,NumeroCassetto,FK_idArmadietto")] Cassetti cassetti)
        {
            if (ModelState.IsValid)
            {
                db.Cassetti.Add(cassetti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_idArmadietto = new SelectList(db.Armadietti, "idArmadietto", "idArmadietto", cassetti.FK_idArmadietto);
            return View(cassetti);
        }

        // GET: Cassetti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cassetti cassetti = db.Cassetti.Find(id);
            if (cassetti == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_idArmadietto = new SelectList(db.Armadietti, "idArmadietto", "idArmadietto", cassetti.FK_idArmadietto);
            return View(cassetti);
        }

        // POST: Cassetti/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCassetto,NumeroCassetto,FK_idArmadietto")] Cassetti cassetti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cassetti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_idArmadietto = new SelectList(db.Armadietti, "idArmadietto", "idArmadietto", cassetti.FK_idArmadietto);
            return View(cassetti);
        }

        // GET: Cassetti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cassetti cassetti = db.Cassetti.Find(id);
            if (cassetti == null)
            {
                return HttpNotFound();
            }
            return View(cassetti);
        }

        // POST: Cassetti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cassetti cassetti = db.Cassetti.Find(id);
            db.Cassetti.Remove(cassetti);
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
