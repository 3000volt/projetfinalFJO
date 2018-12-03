using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            analyse.NoProgramme = this.HttpContext.Session.GetString("programme");
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