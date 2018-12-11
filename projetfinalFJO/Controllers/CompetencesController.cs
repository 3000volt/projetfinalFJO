using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using projetfinalFJO.Appdata;

namespace projetfinalFJO.Controllers
{
    [Authorize(Roles = "Admin,Sous_Commite,Srdp,Commite_Programme")]
    public class CompetencesController : Controller
    {
        private readonly ActualisationContext _context;

        public CompetencesController(ActualisationContext context)
        {
            _context = context;
        }

        // GET: Competences
        public async Task<IActionResult> ListeCompetence(string search)
        {
            return View(await _context.Competences.Where(x => x.Description.StartsWith(search) || x.NoProgramme.StartsWith(search) || search == null).ToListAsync());
        }

        // GET: Competences/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competences = await _context.Competences
                .Include(c => c.NomFamilleNavigation)
                .Include(c => c.NoProgrammeNavigation)
                .FirstOrDefaultAsync(m => m.CodeCompetence == id);

            if (competences == null)
            {
                return NotFound();
            }

            return View(competences);
        }

        public ActionResult ListeElementCompetence(string id)
        {
            //Il faut retourner la liste des élément sde compétences de la compétence concernée
            List<CompetencesElementCompetence> listeCompElemComp = this._context.CompetencesElementCompetence.ToList().FindAll(x => x.CodeCompetence == id);
            List<string> listeNomElem = new List<string>();
            foreach(CompetencesElementCompetence compElemComp in listeCompElemComp)
            {
                listeNomElem.Add(compElemComp.ElementCompétence);
            }
            List<Elementcompetence> listeElemComp = new List<Elementcompetence>();
            foreach(string listeElem in listeNomElem)
            {
                listeElemComp.Add(this._context.Elementcompetence.ToList().Find(x => x.ElementCompétence == listeElem));
            }
            return View(listeElemComp);
        }

        // GET: Competences/Create
        public IActionResult Create()
        {
            ViewData["Idfamille"] = new SelectList(_context.Famillecompetence, "Idfamille", "NomFamille");
            ViewData["NoProgramme"] = new SelectList(_context.Programmes, "NoProgramme", "NoProgramme");
            ViewBag.competence = new Competences();
            ViewBag.element = new Elementcompetence();
            return View();
        }

        // POST: Competences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody][Bind("CodeCompetence,ObligatoireCégep,Titre,Description,ContextRealisation")] Competences competences)
        {
            competences.NoProgramme = this.HttpContext.Session.GetString("programme");
            //competences.NoProgramme = this.HttpContext.Session.GetString("programme");
            if (ModelState.IsValid)
            {
                //Mettre la session a cette compétence
                this.HttpContext.Session.SetString("Competence", JsonConvert.SerializeObject(competences));
                //Ajouter a la bd
                _context.Add(competences);
                await _context.SaveChangesAsync();
                return Ok("élément ajouté avec succès");
            }
            ViewData["Idfamille"] = new SelectList(_context.Famillecompetence, "Idfamille", "NomFamille", competences.NomFamille);
            ViewData["NoProgramme"] = new SelectList(_context.Programmes, "NoProgramme", "NoProgramme", competences.NoProgramme);
            return BadRequest("élément non ajouté");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromBody][Bind("CodeCompetence,ObligatoireCégep,Titre,Description,ContextRealisation")] Competences competences)
        {
            competences.NoProgramme = this.HttpContext.Session.GetString("programme");
            if (ModelState.IsValid)
            {
                _context.Update(competences);
                // https://stackoverflow.com/questions/25894587/how-to-update-record-using-entity-framework-6
                await _context.SaveChangesAsync();
                return Ok("élément ajouté avec succès");
            }
            ViewData["Idfamille"] = new SelectList(_context.Famillecompetence, "Idfamille", "NomFamille", competences.NomFamille);
            ViewData["NoProgramme"] = new SelectList(_context.Programmes, "NoProgramme", "NoProgramme", competences.NoProgramme);
            return BadRequest("élément non ajouté");
        }

        // GET: Competences/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competences = await _context.Competences.FindAsync(id);
            if (competences == null)
            {
                return NotFound();
            }
            ViewData["Idfamille"] = new SelectList(_context.Famillecompetence, "Idfamille", "NomFamille", competences.NomFamille);
            ViewData["Sequence"] = new SelectList(_context.Sequences, "IdSequence", "NomSequence", competences.NomSequence);
            //ViewData["NoProgramme"] = new SelectList(_context.Programmes, "NoProgramme", "NoProgramme", competences.NoProgramme);
            return View(competences);
        }

        // POST: Competences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodeCompetence,ObligatoireCégep,Titre,Description,ContextRealisation,Idfamille,NoProgramme")] Competences competences)
        {
            //Prend rel enuméro du programme
            competences.NoProgramme = this.HttpContext.Session.GetString("NoPorgramme");
            if (id != competences.CodeCompetence)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competences);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetencesExists(competences.CodeCompetence))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ListeCompetence));
            }
            ViewData["Idfamille"] = new SelectList(_context.Famillecompetence, "Idfamille", "NomFamille", competences.NomFamille);
            ViewData["NoProgramme"] = new SelectList(_context.Programmes, "NoProgramme", "NoProgramme", competences.NoProgramme);
            return View(competences);
        }

        // GET: Competences/Delete/5
        public async Task<IActionResult> Supprimer_Compétence(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competences = await _context.Competences
                .Include(c => c.NomFamilleNavigation)
                .Include(c => c.NoProgrammeNavigation)
                .FirstOrDefaultAsync(m => m.CodeCompetence == id);
            if (competences == null)
            {
                return NotFound();
            }

            return View(competences);
        }

        // POST: Competences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var competences = await _context.Competences.FindAsync(id);
            _context.Competences.Remove(competences);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListeCompetence));
        }

        private bool CompetencesExists(string id)
        {
            return _context.Competences.Any(e => e.CodeCompetence == id);
        }

    }
}
