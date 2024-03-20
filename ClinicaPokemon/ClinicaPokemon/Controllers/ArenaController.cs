using ClinicaPokemon.Models;
using System.Linq;
using System.Web.Mvc;

namespace ClinicaPokemon.Controllers
{
    public class ArenaController : Controller
    {
        private ClinicaDbContext db = new ClinicaDbContext();

        public ActionResult Index()
        {
            var pokemonSelectList = new SelectList(db.Animali.ToList(), "IdAnimale", "Nome");
            ViewBag.Pokemon1SelectList = pokemonSelectList;
            ViewBag.Pokemon2SelectList = pokemonSelectList;

            return View();
        }

        public ActionResult GetPokemonDetails(int id)
        {
            var pokemon = db.Animali.FirstOrDefault(a => a.idAnimale == id);
            if (pokemon == null)
            {
                return HttpNotFound();
            }
            return Json(new { nome = pokemon.Nome, immagine = Url.Content($"{pokemon.Immagine}") }, JsonRequestBehavior.AllowGet);
        }
    }

}




