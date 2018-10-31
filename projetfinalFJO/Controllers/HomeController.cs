using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using projetfinalFJO.Appdata;
using projetfinalFJO.Models;

namespace projetfinalFJO.Controllers
{
    public class HomeController : Controller
    {
        //Propriétés
        private IConfiguration config;
        private ActualisationContext contexte;
        List<ActualisationInformation> listeActualisation;
        List<Programmes> listeProgrammes;

        //Constructeur du controleur
        public HomeController(IConfiguration iConfig)
        {
            this.config = iConfig;
            this.contexte = new ActualisationContext();
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Actualisation()
        {
            listeActualisation = this.contexte.ActualisationInformation.ToList();
            ActualisationViewModel actu = new ActualisationViewModel();

            List<ActualisationViewModel> actuListe = listeActualisation.Select(x => new ActualisationViewModel
            {
                NumActualisation = x.NumActualisation,
                NomActualisation = x.NomActualisation,
                NoProgramme = x.NoProgramme,
                NomProgramme = Selection(x.NoProgramme),
                Approuve = x.Approuve
            }).ToList();
            return View(actuListe);
        }

        private string Selection(string num)
        {
            listeProgrammes = this.contexte.Programmes.ToList();
            Programmes prog = listeProgrammes.Find(x => x.NoProgramme == num);
            string numero = prog.NomProgramme;
            return numero;
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
