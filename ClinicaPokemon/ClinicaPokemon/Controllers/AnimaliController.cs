using ClinicaPokemon.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ClinicaPokemon.Controllers
{

    public class AnimaliController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();

        [Authorize(Roles = "Veterinario, Admin")]
        public ActionResult Index()
        {
            var animali = db.Animali.Include(a => a.Utenti);
            return View(animali.ToList());
        }


        [Authorize(Roles = "Veterinario, Admin")]
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
        public ActionResult Create([Bind(Include = "idAnimale,Nome,Tipologia,Colore,DataNascita,Microchip,NrMicro,FK_idUtente, DataRegistrazione, Immagine")] Animali animali, HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/assets/img"), fileName);
                    if (!Directory.Exists(Server.MapPath("~/Content/assets/img")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/assets/img"));
                    }
                    file.SaveAs(path);
                    animali.Immagine = "/Content/assets/img/" + fileName;
                }
                else
                {
                    animali.Immagine = "/Content/assets/img/Default.png";
                }

                if (ModelState.IsValid)
                {
                    db.Animali.Add(animali);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Errore durante il salvataggio del file: " + ex.Message);
            }
            ViewBag.FK_idUtente = new SelectList(db.Utenti.Select(u => new
            {
                idUtente = u.idUtente,
                NomeCompleto = u.Cognome + " " + u.Nome
            }), "idUtente", "NomeCompleto", animali.FK_idUtente);

            return View(animali);
        }


        [Authorize(Roles = "Veterinario, Utente, Admin")]
        public ActionResult CreateForUser()
        {
            var userId = db.Utenti.FirstOrDefault(u => u.Username == User.Identity.Name).idUtente;
            ViewBag.FK_idUtente = new SelectList(db.Utenti.Where(u => u.idUtente == userId).Select(u => new
            {
                idUtente = u.idUtente,
                NomeCompleto = u.Cognome + " " + u.Nome
            }), "idUtente", "NomeCompleto");
            return View("CreateForUser");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForUser([Bind(Include = "idAnimale,Nome,Tipologia,Colore,DataNascita,Microchip,NrMicro,FK_idUtente, DataRegistrazione, Immagine")] Animali animali, HttpPostedFileBase file)
        {
            var userId = db.Utenti.FirstOrDefault(u => u.Username == User.Identity.Name).idUtente;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/assets/img"), fileName);
                    if (!Directory.Exists(Server.MapPath("~/Content/assets/img")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/assets/img"));
                    }
                    file.SaveAs(path);
                    animali.Immagine = "/Content/assets/img/" + fileName;
                }
                else
                {
                    animali.Immagine = "/Content/assets/img/Default.png";
                }

                if (ModelState.IsValid && animali.FK_idUtente == userId)
                {
                    db.Animali.Add(animali);
                    db.SaveChanges();
                    TempData["Message"] = "Il tuo Pokémon è stato inserito nei nostri sistemi!";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Errore durante il salvataggio del file: " + ex.Message);
            }

            ViewBag.FK_idUtente = new SelectList(db.Utenti.Where(u => u.idUtente == userId).Select(u => new
            {
                idUtente = u.idUtente,
                NomeCompleto = u.Cognome + " " + u.Nome
            }), "idUtente", "NomeCompleto", animali.FK_idUtente);

            return View(animali);
        }




        [Authorize(Roles = "Veterinario, Admin")]
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "idAnimale,Nome,Tipologia,Colore,Microchip,NrMicro,FK_idUtente, Immagine, DataNascita")] Animali animali, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var oldAnimal = db.Animali.AsNoTracking().FirstOrDefault(a => a.idAnimale == id);
                    if (file != null && file.ContentLength > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(oldAnimal.Immagine))
                        {
                            var existingImagePath = Path.Combine(Server.MapPath("~/Content/assets/img/"), oldAnimal.Immagine);
                            if (System.IO.File.Exists(existingImagePath))
                            {
                                System.IO.File.Delete(existingImagePath);
                            }
                        }

                        var fileName = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.Ticks + Path.GetExtension(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/assets/img/"), fileName);
                        file.SaveAs(path);

                        animali.Immagine = "/Content/assets/img/" + fileName;
                    }
                    else
                    {
                        animali.Immagine = oldAnimal.Immagine;
                    }
                    db.Entry(animali).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            ViewBag.FK_idUtente = new SelectList(db.Utenti, "idUtente", "Username", animali.FK_idUtente);
            return View(animali);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult FindYourPokemon()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> FindYourPokemon(string NrMicro)
        {
            var pokemon = await db.Animali
                .Select(a => new
                {
                    a.idAnimale,
                    a.Nome,
                    a.Tipologia,
                    a.Colore,
                    a.DataNascita,
                    a.Microchip,
                    a.NrMicro,
                    a.FK_idUtente,
                    a.DataRegistrazione,
                    a.Immagine
                })
                .FirstOrDefaultAsync(a => a.NrMicro == NrMicro);
            if (pokemon == null)
            {
                return Json(new { success = false, message = "Questo pokemon non è nella nostra struttura!" });
            }
            return Json(new { success = true, message = "Il tuo pokemon è nella nostra struttura!", data = pokemon });
        }
    }
}
