using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using projetfinalFJO.Appdata;
using projetfinalFJO.Models;
using projetfinalFJO.Models.Authentification;

namespace projetfinalFJO.Controllers
{
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Sous_Commite")]
    [Authorize(Roles = "Srdp")]
    [Authorize(Roles = "Commite_Programme")]
    public class HomeController : Controller
    {
        //Propriétés
        private IConfiguration config;
        private ActualisationContext contexteActu;
        private readonly LoginDbContext contextLogin;
        private readonly UserManager<LoginUser> _userManager;

        //Constructeur du controleur
        public HomeController(IConfiguration iConfig, UserManager<LoginUser> userManager, LoginDbContext log)
        {
            this.config = iConfig;
            this.contexteActu = new ActualisationContext(config.GetConnectionString("DefaultConnection"));
            this._userManager = userManager;
            this.contextLogin = log;
        }

        public IActionResult Index()
        {
            //Retourner les actualisations que l'utilisateur participe
            List<Membresdesactualisations> membreActusPresent = this.contexteActu.Membresdesactualisations.ToList().FindAll(x => x.AdresseCourriel == this.User.Identity.Name);
            List<ActualisationInformation> actus = new List<ActualisationInformation>();
            foreach (Membresdesactualisations membre in membreActusPresent)
            {
                actus.Add(new ActualisationInformation{
                    NumActualisation = membre.NumActualisation,
                    NomActualisation = this.contexteActu.ActualisationInformation.ToList().Find(x => x.NumActualisation == membre.NumActualisation).NomActualisation,
                    NoProgramme = this.contexteActu.ActualisationInformation.ToList().Find(x => x.NumActualisation == membre.NumActualisation).NoProgramme,
                    Approuve = this.contexteActu.ActualisationInformation.ToList().Find(x => x.NumActualisation == membre.NumActualisation).Approuve
                });
            }
            List<ActualisationViewModel> actuListe = actus.Select(x => new ActualisationViewModel
            {
                NumActualisation = x.NumActualisation,
                NomActualisation = x.NomActualisation,
                NoProgramme = x.NoProgramme,
                NomProgramme = Selection(x.NoProgramme),
                Approuve = x.Approuve
            }).ToList();
            return View(actuListe);
        }

        //Méthode privé pour retirer le nom du programme
        private string Selection(string num)
        {
            var listeProgrammes = this.contexteActu.Programmes.ToList();
            Programmes prog = listeProgrammes.Find(x => x.NoProgramme == num);
            string numero = prog.NomProgramme;
            return numero;
        }

        public ActionResult ChoixActualisation(int num, string prog)
        {
            int numSession = num;
            this.HttpContext.Session.SetString("programme", prog);
            //Créer une session pour garder en mémoire l'actualisation en cours
            this.HttpContext.Session.SetString("NumActualisation", numSession.ToString());
            return RedirectToAction("Accueil");
        }

        public ActionResult Accueil()
        {
            int numActu = int.Parse(this.HttpContext.Session.GetString("NumActualisation"));
            var actu = this.contexteActu.ActualisationInformation.ToList().Find(x => x.NumActualisation == numActu);
            //Transformer en View Model
            ActualisationViewModel actuVM = new ActualisationViewModel
            {
                NumActualisation = actu.NumActualisation,
                NomActualisation = actu.NomActualisation,
                NoProgramme = actu.NoProgramme,
                NomProgramme = Selection(actu.NoProgramme),
                Approuve = actu.Approuve
            };
            return View(actuVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
