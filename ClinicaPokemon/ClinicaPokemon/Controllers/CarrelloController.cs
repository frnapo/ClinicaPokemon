using ClinicaPokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ClinicaPokemon.Controllers
{
    public class CarrelloController : Controller
    {

        // GET: Carrello
        public ActionResult Index()
        {

            var cart = Session["cart"] as List<Prodotti>;
            if (cart == null || !cart.Any()) // Determina se una sequenza contiene elementi
            {
                return RedirectToAction("Index", "Prodotti");
            }
            return View(cart);
        }

        [HttpPost]
        public ActionResult Order()
        {
            ClinicaDbContext db = new ClinicaDbContext();

            var userId = db.Utenti.FirstOrDefault(u => u.Username == User.Identity.Name).idUtente;

            var cart = Session["cart"] as List<Prodotti>;
            if (cart != null && cart.Any())
            {
                Vendite vendite = new Vendite();

                vendite.FK_idUtente = userId;
                vendite.DataVendita = DateTime.Now;
                db.Vendite.Add(vendite);
                db.SaveChanges();

                foreach (var product in cart)
                {
                    DettagliVendita newDettagli = new DettagliVendita();
                    newDettagli.FK_idProdotto = product.idProdotto;
                    newDettagli.FK_idVendita = vendite.idVendita;
                    newDettagli.Quantita = Convert.ToInt32(product.Quantita);

                    db.DettagliVendita.Add(newDettagli);
                    db.SaveChanges();

                }
                cart.Clear();
            }
            TempData["Order"] = "Ordine effettuato con successo!";

            return RedirectToAction("Index", "Prodotti");
        }

        public ActionResult Delete(int? id)
        {
            var cart = Session["cart"] as List<Prodotti>;
            if (cart != null)
            {
                var productToRemove = cart.FirstOrDefault(p => p.idProdotto == id);
                if (productToRemove != null)
                {
                    if (productToRemove.Quantita > 1)
                    {
                        productToRemove.Quantita--;
                    }
                    else
                    {
                        cart.Remove(productToRemove);
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult CartClear()
        {
            var cart = Session["cart"] as List<Prodotti>;
            if (cart != null)
            {
                cart.Clear();
            }
            TempData["CreateMess"] = "Il carrello è stato svuotato";
            return RedirectToAction("Index", "Prodotti");
        }
    }
}