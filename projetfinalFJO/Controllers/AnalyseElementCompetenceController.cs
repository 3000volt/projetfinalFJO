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

namespace projetfinalFJO.Controllers
{
    [Authorize(Roles = "Admin,Sous_Commite,Srdp,Commite_Programme")]
    public class AnalyseElementCompetenceController : Controller
    {
        private readonly ActualisationContext _context;

        public AnalyseElementCompetenceController(ActualisationContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody][Bind("NiveauTaxonomique,Reformulation,Context,SavoirFaireProgramme,SavoirEtreProgramme,AdresseCourriel,ElementCompétence,NoProgramme")] AnalyseElementsCompetence analyse)
        {
            try
            {
                //Prendre le courriel du user actif
                analyse.AdresseCourriel = this.HttpContext.User.Identity.Name;
                analyse.NoProgramme = this.HttpContext.Session.GetString("programme");
                if (ModelState.IsValid)
                {
                    _context.Add(analyse);
                    await _context.SaveChangesAsync();
                    return Ok("élément ajouté avec succès");
                }

                return BadRequest("élément non ajouté");
            }
            catch (Exception e)
            {
                return BadRequest("Erreur");
            }
           
        }

        public ActionResult CreerAnalyseListe()
        {
            try
            {
                //Prendre le numéro du code de compétence dans la session
                string codeCompetence = this.HttpContext.Session.GetString("CodeCompetence");
                List<string> listeNiveauTaxonomique = new List<string> { "Se rappeler", "Comprendre", "Appliquer", "Analyser", "Évaluer", "Créer" };
                //Avoir la liste de tout les compétences
                ViewBag.Contexte = "ElemNonChoisi";
                //Avoir ela liste des compétences du programme de l'actualsiation en cours
                List<Elementcompetence> listeElemComp = this._context.CompetencesElementCompetence.ToList().FindAll(x => x.CodeCompetence == codeCompetence).Select(element => new Elementcompetence() { ElementCompétence = element.ElementCompétence }).ToList();
                ViewBag.Element = new SelectList(listeElemComp, "ElementCompétence", "ElementCompétence");//, "ElementCompétence", "ElementCompétence"
                ViewBag.Taxonomie = new SelectList(listeNiveauTaxonomique);
                ViewBag.CreerAnalyse = new AnalyseCompétence();
                //pour n,afficher qu'un formulaire
                List<string> liste1 = new List<string>();
                liste1.Add("");
                ViewBag.NumeroElem = liste1;
                return View("../AnalyseCompetence/CreerAnalyse");
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
                AnalyseElementsCompetence analyse = this._context.AnalyseElementsCompetence.ToList().Find(x => x.ElementCompétence == code && x.AdresseCourriel == email);
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
                AnalyseElementsCompetence analyseModif = JsonConvert.DeserializeObject<AnalyseElementsCompetence>(this.HttpContext.Session.GetString("analsyeModif"));
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
                return View("ListeElements");
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
        }

    }
}