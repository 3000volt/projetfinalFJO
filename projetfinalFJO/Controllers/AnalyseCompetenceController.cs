using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using projetfinalFJO.Appdata;
using projetfinalFJO.Models;

namespace projetfinalFJO.Controllers
{
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
        public async Task<IActionResult> Create([FromBody][Bind("NiveauTaxonomique,Reformulation,Context,SavoirFaireProgramme,SavoirEtreProgramme,AdresseCourriel,Famille,Sequence,CodeCompetence,NoProgramme")] AnalyseViewModel analyseVM)
        {
            if (ModelState.IsValid)
            {
                //Mettre à jour la famille et sequence de la competence
                //Trouver la competence
                var competence = this._context.Competences.ToList().Find(x => x.CodeCompetence == analyseVM.CodeCompetence);
                //Trouver la famille
                competence.NomFamille = analyseVM.Famille;
                //Trouver la séquence
                competence.NomSequence = analyseVM.Sequence;
                this._context.Competences.Update(competence);
                //Convertir en simple analyse
                AnalyseCompétence analyse = new AnalyseCompétence
                {
                    NiveauTaxonomique = analyseVM.NiveauTaxonomique,
                    Reformulation = analyseVM.Reformulation,
                    Context = analyseVM.Context,
                    SavoirFaireProgramme = analyseVM.SavoirFaireProgramme,
                    SavoirEtreProgramme = analyseVM.SavoirEtreProgramme,
                    AdresseCourriel = analyseVM.AdresseCourriel,
                    CodeCompetence = analyseVM.CodeCompetence
                };
                _context.Add(analyse);
                await _context.SaveChangesAsync();
                return Ok("élément ajouté avec succès");
            }
            //ViewData["Idfamille"] = new SelectList(_context.Famillecompetence, "Idfamille", "NomFamille", competences.Idfamille);
            //ViewData["NoProgramme"] = new SelectList(_context.Programmes, "NoProgramme", "NoProgramme", competences.NoProgramme);
            return BadRequest("élément non ajouté");
        }

    }
}