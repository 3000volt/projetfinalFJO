﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using projetfinalFJO.Appdata;
using projetfinalFJO.Models;

namespace projetfinalFJO.Controllers
{
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Sous_Commite")]
    [Authorize(Roles = "Srdp")]
    public class AnalyseCompetenceController : Controller
    {
        private readonly ActualisationContext _context;
        private IHttpContextAccessor _contextAccessor;

        public AnalyseCompetenceController(ActualisationContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            //contextAccessor -> https://stackoverflow.com/questions/51943383/accessing-session-state-outside-controller
        }

        public ActionResult CreerAnalyse()
        {
            string num = null;
            var context = _contextAccessor.HttpContext;
            //Trouver le numero en cours grace a la session
            if (this.HttpContext.Session.Keys.Contains("Competence"))
            {
                num = JsonConvert.DeserializeObject<Competences>(this.HttpContext.Session.GetString("Competence")).CodeCompetence;// "oui";
            }
            //Trouver le numéro de la compétence concerné
            //Trouver la liste des éléments de compétences que contient cette compétence
            List<CompetencesElementCompetence> compE = this._context.CompetencesElementCompetence.ToList().FindAll(x => x.CodeCompetence == num);
            List<Elementcompetence> listeElements = new List<Elementcompetence>();
            List<string> elements = new List<string>();
            foreach (CompetencesElementCompetence com in compE)
            {
                listeElements.Add(this._context.Elementcompetence.ToList().Find(x => x.ElementCompétence == com.ElementCompétence));
                elements.Add((this._context.Elementcompetence.ToList().Find(x => x.ElementCompétence == com.ElementCompétence).ElementCompétence));
            }
            ViewBag.Competence = num;
            ViewBag.ListeElements = listeElements.Count;
            ViewBag.NumeroElem = elements;
            ViewBag.Numero = num;
            ViewBag.Idfamille = new SelectList(_context.Famillecompetence, "NomFamille", "NomFamille");
            ViewBag.Sequence = new SelectList(_context.Sequences, "NomSequence", "NomSequence");
            ViewBag.CreerAnalyse = new AnalyseCompétence();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody][Bind("NiveauTaxonomique,Reformulation,Context,SavoirFaireProgramme,SavoirEtreProgramme,AdresseCourriel,CodeCompetence")] AnalyseCompétence analyse)
        {
            
            if (ModelState.IsValid)
            {
                analyse.NoProgramme = this.HttpContext.Session.GetString("programme");
                _context.Add(analyse);
                await _context.SaveChangesAsync();
                return Ok("élément ajouté avec succès");
            }
          
            return BadRequest("élément non ajouté");
        }
        public PartialViewResult PartialAjouterFamille()
        {
            //ViewBag.groupe = new GroupeCompetence();
            ViewData["NomFamille"] = new SelectList(_context.Famillecompetence, "NomFamille", "NomFamille");
            return PartialView("_partialAjouterFamille");
        }

        public PartialViewResult PartialListeFamille()
        {
            //ViewBag.groupe = new GroupeCompetence();
            ViewData["NomFamille"] = new SelectList(_context.Famillecompetence, "NomFamille", "NomFamille");
            return PartialView("_partialListeFamille");
        }

        public PartialViewResult PartialAjouterSequence()
        {
            //ViewBag.groupe = new GroupeCompetence();
            ViewData["NomSequence"] = new SelectList(_context.Sequences, "NomSequence", "NomSequence");
            return PartialView("_partialAjouterSequence");
        }

        public PartialViewResult PartialListeSequence()
        {
            //ViewBag.groupe = new GroupeCompetence();
            ViewData["NomSequence"] = new SelectList(_context.Sequences, "NomSequence", "NomSequence");
            return PartialView("_partialListeSequence");
        }

        public ActionResult ListeAnalyse()
        {
            //Récupérer toute la liste des analyses
            List<AnalyseCompetenceVM> liste = new List<AnalyseCompetenceVM>();
            var listeAnalyseComplete = this._context.AnalyseCompétence.Where(x => x.NoProgramme.Equals(this.HttpContext.Session.GetString("programme"))).ToList();
            foreach (AnalyseCompétence analyse in listeAnalyseComplete)
            {
                liste.Add(new AnalyseCompetenceVM
                {
                    CodeCompetence = analyse.CodeCompetence,
                    NiveauTaxonomique = analyse.NiveauTaxonomique,
                    ValidationApprouve = analyse.ValidationApprouve,
                    AdresseCourriel = analyse.AdresseCourriel
                });
            }
            return View(liste);
        }

        public ActionResult ListeElements(string code, string email)
        {
            //Liste de tout les analyses dans la bd des elements
            List<AnalyseElementsCompetence> analyses = this._context.AnalyseElementsCompetence.ToList();
            List<string> numeroAnalyse = new List<string>();
            foreach (AnalyseElementsCompetence a in analyses)
            {
                numeroAnalyse.Add(a.ElementCompétence);
            }
            List<CompetencesElementCompetence> elements = new List<CompetencesElementCompetence>();
            elements = this._context.CompetencesElementCompetence.ToList().FindAll(x => x.CodeCompetence == code);
            List<AnalyseElementsCompetence> listeElemCompComplete = new List<AnalyseElementsCompetence>();
            foreach (CompetencesElementCompetence comp in elements)
            {
                if (analyses.Any(x => x.ElementCompétence == comp.ElementCompétence))
                {
                    listeElemCompComplete.Add(analyses.Find(x => x.ElementCompétence == comp.ElementCompétence));
                }
            }
            //List<CompetencesElementCompetence> elements = new List<CompetencesElementCompetence>();
            //elements = this._context.CompetencesElementCompetence.ToList().FindAll(x => x.CodeCompetence == code);
            //List<AnalyseElementsCompetence> listeElemComp = new List<AnalyseElementsCompetence>();
            //foreach (CompetencesElementCompetence element in elements)
            //{
            //    //Récupréer le num de l'elements
            //    string numElement = element.ElementCompétence;// this._context.CompetencesElementCompetence.ToList().Find(x => x.CodeCompetence == element.CodeCompetence).ElementCompétence;
            //    //Ajouter a l aliste
            //    listeElemComp.Add(this._context.AnalyseElementsCompetence.ToList().Find(x => x.ElementCompétence == numElement && x.AdresseCourriel == email));
            //}
            //foreach(AnalyseElementsCompetence anal in analyses.Where(x=>x.ElementCompétence == ))
            //listeElemComp = this._context.AnalyseElementsCompetence.ToList().FindAll(x => x.ElementCompétence == numElement && x.AdresseCourriel == email);
            return View(listeElemCompComplete);
        }


        public ActionResult ConsulterAnalyse(string code, string email)
        {
            //Sélectionner l'analyse en question
            AnalyseCompétence analyse = this._context.AnalyseCompétence.ToList().Find(x => x.CodeCompetence == code && x.AdresseCourriel == email);
            return View(analyse);
        }

        public ActionResult ModifierAnalyse(string code, string email)
        {
            //Sélectionner l'analyse en question
            AnalyseCompétence analyse = this._context.AnalyseCompétence.ToList().Find(x => x.CodeCompetence == code && x.AdresseCourriel == email);
            return View(analyse);
        }

        [HttpGet]
        public ActionResult SupprimerAnalyse(string code, string email)
        {
            //Sélectionner l'analyse en question
            AnalyseCompétence analyse = this._context.AnalyseCompétence.ToList().Find(x => x.CodeCompetence == code && x.AdresseCourriel == email);
            return View(analyse);
        }

        [HttpPost]
        public ActionResult SupprimerAnalyse(int id)
        {
            //Supprimer de la bd
            AnalyseCompétence analyse = this._context.AnalyseCompétence.ToList().Find(x => x.IdAnalyseAc == id);
            this._context.Remove(analyse);
            this._context.SaveChanges();
            return RedirectToAction("ListeAnalyse");
        }
    }
}