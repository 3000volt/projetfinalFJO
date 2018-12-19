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
        public async Task<IActionResult> ListeCompetence()
        {
            try
            {
                return View(await _context.Competences.Where(x => x.NoProgramme == this.HttpContext.Session.GetString("programme")).ToListAsync());
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
           
        }

        // GET: Competences/Details/5
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                ViewData["competence"] = await _context.Competences
                    .Include(c => c.NomFamilleNavigation)
                    .Include(c => c.NoProgrammeNavigation)
                    .FirstOrDefaultAsync(m => m.CodeCompetence == id);
                ViewData["listelement"] = _context.CompetencesElementCompetence.ToList().FindAll(x => x.CodeCompetence == id);
                return View();
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // GET: Competences/Create
        public IActionResult Create()
        {
            try
            {
                ViewBag.competence = new Competences();
                return View();
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // POST: Competences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody][Bind("CodeCompetence,ObligatoireCégep,Titre,Description,ContextRealisation")] Competences competences)
        {
            try
            {
                competences.NoProgramme = this.HttpContext.Session.GetString("programme");
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
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromBody][Bind("CodeCompetence,ObligatoireCégep,Titre,Description,ContextRealisation")] Competences competences)
        {
            try
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
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
           
        }

        // GET: Competences/Edit/5
        public async Task<IActionResult> ModifierCompetence(string id)
        {
            try
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
                ViewBag.Idfamille = new SelectList(_context.Famillecompetence, "NomFamille", "NomFamille");
                ViewBag.NomSequence = new SelectList(_context.Sequences, "NomSequence", "NomSequence");
                return View(competences);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // POST: Competences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifierCompetence(string id, [Bind("CodeCompetence,ObligatoireCégep,Titre,Description,ContextRealisation,NomFamille,NoProgramme")] Competences competences)
        {
            try
            {
                //Prend rel enuméro du programme
                competences.NoProgramme = this.HttpContext.Session.GetString("programme");

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
                return View(competences);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            

            
        }

        // GET: Competences/Delete/5
        public async Task<IActionResult> Supprimer_Compétence(string id)
        {
            try
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
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // POST: Competences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {

                var competences = await _context.Competences.FindAsync(id);
                var element_et_competences = _context.CompetencesElementCompetence.ToList().FindAll(x=>x.CodeCompetence==competences.CodeCompetence);
                foreach(CompetencesElementCompetence c in element_et_competences)
                {
                    _context.CompetencesElementCompetence.Remove(c);
                }
                await _context.SaveChangesAsync();
                _context.Competences.Remove(competences);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListeCompetence));
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }

        }

        private bool CompetencesExists(string id)
        {
            return _context.Competences.Any(e => e.CodeCompetence == id);
        }

        public PartialViewResult partialElementcompetence()
        {
            try
            {
                ViewBag.Elementcompetence = new Elementcompetence();
                return PartialView("_PartialElementcompetence");
            }
            catch (Exception e)
            {
                return PartialView("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }
        public PartialViewResult partialCompElement()
        {
            try
            {
                ViewBag.SessionRep = new CompetencesElementCompetence();
                ViewData["CodeCompetence"] = new SelectList(_context.Competences.Where(x => x.NoProgramme.Equals(this.HttpContext.Session.GetString("programme"))), "CodeCompetence", "CodeCompetence");
                ViewData["ElementCompétence"] = new SelectList(_context.Elementcompetence.Where(x => x.NoProgramme.Equals(this.HttpContext.Session.GetString("programme"))), "ElementCompétence", "ElementCompétence");
                return PartialView("_PartialCompElement");
            }
            catch (Exception e)
            {
                return PartialView("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }
    }
}
