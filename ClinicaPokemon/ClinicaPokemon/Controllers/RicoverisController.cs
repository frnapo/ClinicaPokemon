using ClinicaPokemon.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

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


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public async Task<ActionResult> GetRicoveriAttivi()
        {
            var ricoveri = await db.Ricoveri.Include(r => r.Animali).Where(r => r.Attivo == true).ToListAsync();
            var result = ricoveri.Select(r => new
            {
                idRicovero = r.idRicovero,
                FK_idAnimale = r.FK_idAnimale,
                DataInizioRicovero = r.DataInizioRicovero,
                FotoAnimale = r.FotoAnimale,
                PrezzoRicovero = r.PrezzoRicovero,
                Attivo = r.Attivo,
                NomeAnimale = r.Animali.Nome
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetRicoveriNonAttivi()
        {
            var ricoveri = db.Ricoveri.Include(r => r.Animali).Where(r => r.Attivo == true);
            foreach (var ricovero in ricoveri)
            {
                ricovero.Attivo = false;
                db.Entry(ricovero).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
