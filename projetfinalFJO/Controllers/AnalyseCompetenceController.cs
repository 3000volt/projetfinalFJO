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
    [Authorize(Roles = "Admin,Sous_Commite,Srdp,Commite_Programme")]
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
            try
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
                List<string> listeNiveauTaxonomique = new List<string> { "Se rappeler", "Comprendre", "Appliquer", "Analyser", "Évaluer", "Créer" };

                ViewBag.Contexte = "Prechoisi";
                ViewBag.Competence = num;
                ViewBag.Description = this._context.Competences.ToList().Find(x => x.CodeCompetence == num).Titre;
                ViewBag.ListeElements = listeElements.Count;
                ViewBag.NumeroElem = elements;
                ViewBag.Numero = num;
                ViewBag.Taxonomie = new SelectList(listeNiveauTaxonomique);
                ViewBag.Idfamille = new SelectList(_context.Famillecompetence, "NomFamille", "NomFamille");
                ViewBag.Sequence = new SelectList(_context.Sequences, "NomSequence", "NomSequence");
                ViewBag.CreerAnalyse = new AnalyseCompétence();
                return View();
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }

        }

        public ActionResult CreerAnalyseListe()
        {
            try
            {
                var context = _contextAccessor.HttpContext;

                List<string> listeNiveauTaxonomique = new List<string> { "Se rappeler", "Comprendre", "Appliquer", "Analyser", "Évaluer", "Créer" };
                //Avoir la liste de tout les compétences
                ViewBag.Contexte = "NonChoisi";
                //Avoir ela liste des compétences du programme de l'actualsiation en cours
                List<Competences> listeComp = this._context.Competences.ToList().FindAll(x => x.NoProgramme == this.HttpContext.Session.GetString("programme"));
                ViewBag.Competence = new SelectList(listeComp, "CodeCompetence", "CodeCompetence");
                ViewBag.Taxonomie = new SelectList(listeNiveauTaxonomique);
                ViewBag.Idfamille = new SelectList(_context.Famillecompetence, "NomFamille", "NomFamille");
                ViewBag.Sequence = new SelectList(_context.Sequences, "NomSequence", "NomSequence");
                ViewBag.CreerAnalyse = new AnalyseCompétence();
                return View("CreerAnalyse");
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody][Bind("NiveauTaxonomique,Reformulation,Context,SavoirFaireProgramme,SavoirEtreProgramme,CodeCompetence")] AnalyseCompétence analyse)
        {
            try
            {
                analyse.AdresseCourriel = this.HttpContext.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    analyse.NoProgramme = this.HttpContext.Session.GetString("programme");
                    _context.Add(analyse);
                    await _context.SaveChangesAsync();
                    return Ok("élément ajouté avec succès");
                }

                return BadRequest("élément non ajouté");
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }

        }

        public ActionResult ListeAnalyse(string search)
        {
            try
            {
                //Récupérer toute la liste des analyses
                List<AnalyseCompetenceVM> liste = new List<AnalyseCompetenceVM>();
                var listeAnalyseComplete = _context.AnalyseCompétence.Where(x => x.AdresseCourriel.StartsWith(search) || x.CodeCompetence.StartsWith(search) || search == null).ToList();
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
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }

        }

        public ActionResult ListeElements(string code, string email)
        {
            try
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

                foreach (AnalyseElementsCompetence a in analyses)
                {
                    if (elements.Any(x => x.ElementCompétence == a.ElementCompétence))
                    {
                        //listeElemCompComplete.Add(analyses.Find(x => x.ElementCompétence == a.ElementCompétence));
                        listeElemCompComplete.Add(a);

                    }
                }
                List<string> listeNiveauTaxonomique = new List<string> { "Se rappeler", "Comprendre", "Appliquer", "Analyser", "Évaluer", "Créer" };
                ViewBag.Taxonomie = new SelectList(listeNiveauTaxonomique);
                //Affecter la session au code de compétence en cours
                this.HttpContext.Session.SetString("CodeCompetence", code);
                return View(listeElemCompComplete);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
        }

        public ActionResult ListeElements2()
        {
            try
            {
                //Trouver le code a l'aide de la session
                string code = this.HttpContext.Session.GetString("CodeCompetence");
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

                foreach (AnalyseElementsCompetence a in analyses)
                {
                    if (elements.Any(x => x.ElementCompétence == a.ElementCompétence))
                    {
                        //listeElemCompComplete.Add(analyses.Find(x => x.ElementCompétence == a.ElementCompétence));
                        listeElemCompComplete.Add(a);

                    }
                }
                List<string> listeNiveauTaxonomique = new List<string> { "Se rappeler", "Comprendre", "Appliquer", "Analyser", "Évaluer", "Créer" };
                ViewBag.Taxonomie = new SelectList(listeNiveauTaxonomique);
                return View("ListeElements", listeElemCompComplete);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
        }

        public ActionResult ConsulterAnalyse(string code, string email)
        {
            try
            {
                //Sélectionner l'analyse en question
                AnalyseCompétence analyse = this._context.AnalyseCompétence.ToList().Find(x => x.CodeCompetence == code && x.AdresseCourriel == email);
                //Mettre l'analyse dans une session
                this.HttpContext.Session.SetString("analsyeModif", JsonConvert.SerializeObject(analyse));
                return View(analyse);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }

        }

        [HttpGet]
        public ActionResult ModifierAnalyse(string code, string email)
        {
            try
            {
                //Sélectionner l'analyse en question
                AnalyseCompétence analyse = this._context.AnalyseCompétence.ToList().Find(x => x.CodeCompetence == code && x.AdresseCourriel == email);
                //Mettre l'analyse dans une session
                this.HttpContext.Session.SetString("analsyeModif", JsonConvert.SerializeObject(analyse));
                //ViewBag pour le niveau taxonomique
                List<string> listeNiveauTaxonomique = new List<string> { "Se rappeler", "Comprendre", "Appliquer", "Analyser", "Évaluer", "Créer" };
                ViewBag.Taxonomie = new SelectList(listeNiveauTaxonomique);
                return View(analyse);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }

        }

        [HttpPost]
        public IActionResult ModifierAnalyse(AnalyseCompétence analyse)
        {
            try
            {
                //Sélectionner l'analyse en question
                AnalyseCompétence analyseModif = JsonConvert.DeserializeObject<AnalyseCompétence>(this.HttpContext.Session.GetString("analsyeModif"));
                //Changer les valeurs modifiées
                analyseModif.NiveauTaxonomique = analyse.NiveauTaxonomique;
                analyseModif.Reformulation = analyse.Reformulation;
                analyseModif.SavoirEtreProgramme = analyse.SavoirEtreProgramme;
                analyseModif.SavoirFaireProgramme = analyse.SavoirFaireProgramme;
                analyseModif.Context = analyse.Context;
                //Sauvegarder
                this._context.Update(analyseModif);
                this._context.SaveChanges();
                //Retourner à la liste d'analyse
                return RedirectToAction(nameof(ListeAnalyse));
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
        }

        [HttpGet]
        public ActionResult SupprimerAnalyse(string code, string email)
        {
            try
            {
                //Sélectionner l'analyse en question
                AnalyseCompétence analyse = this._context.AnalyseCompétence.ToList().Find(x => x.CodeCompetence == code && x.AdresseCourriel == email);
                return View(analyse);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }


        }

        [HttpPost]
        public ActionResult SupprimerAnalyse(int id)
        {
            try
            {
                //Supprimer de la bd
                AnalyseCompétence analyse = this._context.AnalyseCompétence.ToList().Find(x => x.IdAnalyseAc == id);
                this._context.Remove(analyse);
                this._context.SaveChanges();
                return RedirectToAction("ListeAnalyse");
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }

        }

        [HttpPost]
        public string AfficherDescription(string codeComp)
        {

            //variable de la description
            string Titre;
            //l'associer a la description du code correspondant dans la BD
            Titre = this._context.Competences.ToList().Find(x => x.CodeCompetence == codeComp).Titre;
            //Retoruenr la valeur
            return Titre;
        }

        [HttpPost]
        public IActionResult AjouterFamille([FromBody]Famillecompetence famille)
        {
            try
            {
                famille.NoProgramme = this.HttpContext.Session.GetString("programme");
                if (famille != null)
                {
                    this._context.Famillecompetence.Add(famille);
                    this._context.SaveChanges();
                    return Ok("élément ajouté avec succès");

                }
                return BadRequest("élément non ajouté");
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }

        }

        [HttpPost]
        public IActionResult AjouterSequence([FromBody]Sequences sequence)
        {
            try
            {
                sequence.NoProgramme = this.HttpContext.Session.GetString("programme");
                if (sequence != null)
                {
                    this._context.Sequences.Add(sequence);
                    this._context.SaveChanges();
                    return Ok("élément ajouté avec succès");


                }
                return BadRequest("élément non ajouté");
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AssocierFamille([FromBody]Competences competence)
        {
            try
            {
                //Prendre l'objet de la compétence concerné
                Competences comp = this._context.Competences.ToList().Find(x => x.CodeCompetence == competence.CodeCompetence);
                //Associer la bonne famille
                comp.NomFamille = competence.NomFamille;
                //Sauvegarder dans la BD
                this._context.SaveChanges();
                return Ok("Famille associer avec succes a la compétence");
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AssocierSequence([FromBody]Competences competence)
        {
            try
            {
                //Prendre l'objet de la compétence concerné
                Competences comp = this._context.Competences.ToList().Find(x => x.CodeCompetence == competence.CodeCompetence);
                //Associer la bonne famille
                comp.NomSequence = competence.NomSequence;
                //Sauvegarder dans la BD
                this._context.SaveChanges();
                return Ok("Séquence associer avec succes a la compétence");
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }

        }

        public PartialViewResult PartialAjouterFamille()
        {
            try
            {
                ViewData["NomFamille"] = new SelectList(_context.Famillecompetence, "NomFamille", "NomFamille");
                return PartialView("_partialAjouterFamille");
            }
            catch (Exception e)
            {
                return PartialView("\\Views\\Shared\\page_erreur.cshtml");
            }

        }

        public PartialViewResult PartialListeFamille()
        {
            try
            {
                ViewData["NomFamille"] = new SelectList(_context.Famillecompetence, "NomFamille", "NomFamille");
                return PartialView("_partialListeFamille");
            }
            catch (Exception e)
            {
                return PartialView("\\Views\\Shared\\page_erreur.cshtml");
            }


        }

        public PartialViewResult PartialAjouterSequence()
        {
            try
            {
                ViewData["NomSequence"] = new SelectList(_context.Sequences, "NomSequence", "NomSequence");
                return PartialView("_partialAjouterSequence");
            }
            catch (Exception e)
            {
                return PartialView("\\Views\\Shared\\page_erreur.cshtml");
            }

        }

        public PartialViewResult PartialListeSequence()
        {
            try
            {
                ViewData["NomSequence"] = new SelectList(_context.Sequences, "NomSequence", "NomSequence");
                return PartialView("_partialListeSequence");
            }
            catch (Exception e)
            {
                return PartialView("\\Views\\Shared\\page_erreur.cshtml");
            }

        }
    }
}