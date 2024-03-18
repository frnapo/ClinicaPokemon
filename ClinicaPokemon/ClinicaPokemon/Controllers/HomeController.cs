using ClinicaPokemon.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Web.Security;

namespace ClinicaPokemon.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Utenti utente)
        {
            using (var context = new ClinicaDbContext())
            {
                var existingUser = context.Utenti.FirstOrDefault(u => u.Username == utente.Username);
                if (existingUser != null)
                {
                    TempData["RegistrazioneFallita"] = "Utente già registrato";
                    return View();
                }

                utente.Psw = HashPassword(utente.Psw);

                context.Utenti.Add(utente);
                context.SaveChanges();
            }
            TempData["RegistrazioneRiuscita"] = "Registrazione avvenuta con successo";
            return RedirectToAction("Login");
        }

        public string HashPassword(string password)
        {
            // Genera un sale casuale
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // Crea l'hash della password usando PBKDF2
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Combina sale e hash
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Converte in stringa base64
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Utenti utente)
        {
            using (var context = new ClinicaDbContext())
            {
                var existingUser = context.Utenti.FirstOrDefault(u => u.Username == utente.Username);
                if (existingUser == null)
                {
                    TempData["LoginFallito"] = "Utente non registrato";
                    return View();
                }

                if (VerifyPassword(utente.Psw, existingUser.Psw))
                {
                    FormsAuthentication.SetAuthCookie(utente.Username, false);
                    TempData["LoginRiuscito"] = "Login avvenuto con successo";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["LoginFallito"] = "Password errata";
                    return View();
                }
            }
        }

        public bool VerifyPassword(string enteredPassword, string savedPasswordHash)
        {
            // Converte l'hash salvato in un array di byte
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            // Estrae il sale
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Crea l'hash della password fornita utilizzando lo stesso sale
            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Confronta l'hash della password fornita con l'hash salvato
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            TempData["Logout"] = "Sei stato disconesso";
            return RedirectToAction("Index");
        }
    }
}