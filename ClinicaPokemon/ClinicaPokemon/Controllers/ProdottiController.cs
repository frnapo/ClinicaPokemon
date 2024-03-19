using ClinicaPokemon.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ClinicaPokemon.Controllers
{
    public class ProdottiController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();

        // GET: Prodotti
        // GET: Prodotti
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                var prodotti = db.Prodotti
                    .Where(p => p.FK_idCassetto == id.Value)
                    .Include(p => p.Armadietti)
                    .Include(p => p.Cassetti)
                    .Include(p => p.DittaFornitrice)
                    .Include(p => p.UsoProdotti);
                return View(prodotti.ToList());
            }
            else
            {
                var prodotti = db.Prodotti
                    .Include(p => p.Armadietti)
                    .Include(p => p.Cassetti)
                    .Include(p => p.DittaFornitrice)
                    .Include(p => p.UsoProdotti);
                return View(prodotti.ToList());
            }
        }


        // GET: Prodotti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // GET: Prodotti/Create
        public ActionResult Create()
        {
            ViewBag.FK_idArmadietto = new SelectList(db.Armadietti, "idArmadietto", "idArmadietto");
            ViewBag.FK_idCassetto = new SelectList(db.Cassetti, "idCassetto", "idCassetto");
            ViewBag.FK_idDittaFornitrice = new SelectList(db.DittaFornitrice, "idDittaFornitrice", "Nome");
            ViewBag.FK_idUsoProdotto = new SelectList(db.UsoProdotti, "idUsoProdotto", "Uso");
            return View();
        }

        // POST: Prodotti/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProdotto,NomeProdotto,Tipo,FK_idUsoProdotto,FK_idDittaFornitrice,FK_idCassetto,FK_idArmadietto")] Prodotti prodotti)
        {
            if (ModelState.IsValid)
            {
                db.Prodotti.Add(prodotti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_idArmadietto = new SelectList(db.Armadietti, "idArmadietto", "idArmadietto", prodotti.FK_idArmadietto);
            ViewBag.FK_idCassetto = new SelectList(db.Cassetti, "idCassetto", "idCassetto", prodotti.FK_idCassetto);
            ViewBag.FK_idDittaFornitrice = new SelectList(db.DittaFornitrice, "idDittaFornitrice", "Nome", prodotti.FK_idDittaFornitrice);
            ViewBag.FK_idUsoProdotto = new SelectList(db.UsoProdotti, "idUsoProdotto", "Uso", prodotti.FK_idUsoProdotto);
            return View(prodotti);
        }

        // GET: Prodotti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_idArmadietto = new SelectList(db.Armadietti, "idArmadietto", "idArmadietto", prodotti.FK_idArmadietto);
            ViewBag.FK_idCassetto = new SelectList(db.Cassetti, "idCassetto", "idCassetto", prodotti.FK_idCassetto);
            ViewBag.FK_idDittaFornitrice = new SelectList(db.DittaFornitrice, "idDittaFornitrice", "Nome", prodotti.FK_idDittaFornitrice);
            ViewBag.FK_idUsoProdotto = new SelectList(db.UsoProdotti, "idUsoProdotto", "Uso", prodotti.FK_idUsoProdotto);
            return View(prodotti);
        }

        // POST: Prodotti/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProdotto,NomeProdotto,Tipo,FK_idUsoProdotto,FK_idDittaFornitrice,FK_idCassetto,FK_idArmadietto")] Prodotti prodotti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prodotti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_idArmadietto = new SelectList(db.Armadietti, "idArmadietto", "idArmadietto", prodotti.FK_idArmadietto);
            ViewBag.FK_idCassetto = new SelectList(db.Cassetti, "idCassetto", "idCassetto", prodotti.FK_idCassetto);
            ViewBag.FK_idDittaFornitrice = new SelectList(db.DittaFornitrice, "idDittaFornitrice", "Nome", prodotti.FK_idDittaFornitrice);
            ViewBag.FK_idUsoProdotto = new SelectList(db.UsoProdotti, "idUsoProdotto", "Uso", prodotti.FK_idUsoProdotto);
            return View(prodotti);
        }

        // GET: Prodotti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // POST: Prodotti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prodotti prodotti = db.Prodotti.Find(id);
            db.Prodotti.Remove(prodotti);
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

        // Gestione AddToCart

        public ActionResult AddToCart(int id, int Quantita)
        {
            var prodotto = db.Prodotti.Find(id);


            if (prodotto != null)
            {
                var cart = Session["cart"] as List<Prodotti> ?? new List<Prodotti>();
                prodotto.Quantita = Quantita;
                if (cart.Any(p => p.idProdotto == id))
                {
                    var prodottoInCart = cart.First(p => p.idProdotto == id);
                    prodottoInCart.Quantita += Quantita;
                }
                else
                {
                    cart.Add(prodotto);
                    Session["cart"] = cart;
                    TempData["AddCart"] = "Prodotto aggiunto correttamente";
                }

            }
            return RedirectToAction("Index");
        }
    }
}

