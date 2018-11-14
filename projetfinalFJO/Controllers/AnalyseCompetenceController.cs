using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projetfinalFJO.Appdata;

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
            var context = _contextAccessor.HttpContext;
            //Trouver le numero en cours grace a la session
            string num = JsonConvert.DeserializeObject<Competences>(this.HttpContext.Session.GetString("Competence")).CodeCompetence;// "oui";
            //Trouver le numéro de la compétence concerné
            //Trouver la liste des éléments de compétences que contient cette compétence
            List<CompetencesElementCompetence> compE = this._context.CompetencesElementCompetence.ToList().FindAll(x => x.CodeCompetence == num);
            List<Elementcompetence> listeElements = new List<Elementcompetence>();
            foreach (CompetencesElementCompetence com in compE)
            {
                listeElements.Add(this._context.Elementcompetence.ToList().Find(x => x.ElementCompétence == com.ElementCompétence));
            }
            ViewBag.Competence = num;
            ViewBag.ListeElements = listeElements.Count;    
            ViewBag.NumeroElem = listeElements;
            ViewBag.Numero = num;
            ViewBag.CreerAnalyse = new AnalyseCompétence();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody][Bind("NiveauTaxonomique,Reformulation,Context,SavoirFaireProgramme,SavoirEtreProgramme,AdresseCourriel,CodeCompetence")] AnalyseCompétence analyse)
        {
            if (ModelState.IsValid)
            {
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