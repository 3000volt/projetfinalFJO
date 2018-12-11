using System;
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

        public ActionResult CreerAnalyseListe()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody][Bind("NiveauTaxonomique,Reformulation,Context,SavoirFaireProgramme,SavoirEtreProgramme,CodeCompetence")] AnalyseCompétence analyse)
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

        public ActionResult ListeAnalyse(string search)
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
            //foreach (CompetencesElementCompetence comp in elements)
            //{
            //    if (analyses.Any(x => x.ElementCompétence == comp.ElementCompétence))
            //    {
            //        listeElemCompComplete.Add(analyses.Find(x => x.ElementCompétence == comp.ElementCompétence));
            //    }
            //}
            foreach (AnalyseElementsCompetence a in analyses)
            {
                if (elements.Any(x => x.ElementCompétence == a.ElementCompétence))
                {
                    listeElemCompComplete.Add(analyses.Find(x => x.ElementCompétence == a.ElementCompétence));
                }
            }
            List<string> listeNiveauTaxonomique = new List<string> { "Se rappeler", "Comprendre", "Appliquer", "Analyser", "Évaluer", "Créer" };
            ViewBag.Taxonomie = new SelectList(listeNiveauTaxonomique);
            //Affecter la session au code de compétence en cours
            this.HttpContext.Session.SetString("CodeCompetence", code);
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
            famille.NoProgramme = this.HttpContext.Session.GetString("programme");
            if (famille != null)
            {
                this._context.Famillecompetence.Add(famille);
                this._context.SaveChanges();
                return Ok("élément ajouté avec succès");

            }
            return BadRequest("élément non ajouté");
        }

        [HttpPost]
        public IActionResult AjouterSequence([FromBody]Sequences sequence)
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

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AssocierFamille([FromBody]Competences competence)
        {
            //Prendre l'objet de la compétence concerné
            Competences comp = this._context.Competences.ToList().Find(x => x.CodeCompetence == competence.CodeCompetence);
            //Associer la bonne famille
            comp.NomFamille = competence.NomFamille;
            //Sauvegarder dans la BD
            this._context.SaveChanges();
            return Ok("Famille associer avec succes a la compétence");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AssocierSequence([FromBody]Competences competence)
        {
            //Prendre l'objet de la compétence concerné
            Competences comp = this._context.Competences.ToList().Find(x => x.CodeCompetence == competence.CodeCompetence);
            //Associer la bonne famille
            comp.NomSequence = competence.NomSequence;
            //Sauvegarder dans la BD
            this._context.SaveChanges();
            return Ok("Séquence associer avec succes a la compétence");
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
    }
}