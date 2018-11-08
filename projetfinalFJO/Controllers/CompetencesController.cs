using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetfinalFJO.Appdata;

namespace projetfinalFJO.Controllers
{
    public class CompetencesController : Controller
    {
        private readonly ActualisationContext _context;

        public CompetencesController(ActualisationContext context)
        {
            _context = context;
        }

        // GET: Competences
        public async Task<IActionResult> Index()
        {
            var actualisationContext = _context.Competences.Include(c => c.IdfamilleNavigation).Include(c => c.NoProgrammeNavigation);
            return View(await actualisationContext.ToListAsync());
        }

        // GET: Competences/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competences = await _context.Competences
                .Include(c => c.IdfamilleNavigation)
                .Include(c => c.NoProgrammeNavigation)
                .FirstOrDefaultAsync(m => m.CodeCompetence == id);
            if (competences == null)
            {
                return NotFound();
            }

            return View(competences);
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
        public async Task<IActionResult> Create([FromBody][Bind("CodeCompetence,ObligatoireCégep,Description,ContextRealisation,Idfamille,NoProgramme")] Competences competences)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competences);
                await _context.SaveChangesAsync();
                return Ok("élément ajouté avec succès");
            }
            ViewData["Idfamille"] = new SelectList(_context.Famillecompetence, "Idfamille", "NomFamille", competences.Idfamille);
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
            ViewData["Idfamille"] = new SelectList(_context.Famillecompetence, "Idfamille", "NomFamille", competences.Idfamille);
            ViewData["NoProgramme"] = new SelectList(_context.Programmes, "NoProgramme", "NoProgramme", competences.NoProgramme);
            return View(competences);
        }

        // POST: Competences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodeCompetence,ObligatoireCégep,Description,ContextRealisation,Idfamille,NoProgramme")] Competences competences)
        {
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idfamille"] = new SelectList(_context.Famillecompetence, "Idfamille", "NomFamille", competences.Idfamille);
            ViewData["NoProgramme"] = new SelectList(_context.Programmes, "NoProgramme", "NoProgramme", competences.NoProgramme);
            return View(competences);
        }

        // GET: Competences/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competences = await _context.Competences
                .Include(c => c.IdfamilleNavigation)
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
            return RedirectToAction(nameof(Index));
        }

        private bool CompetencesExists(string id)
        {
            return _context.Competences.Any(e => e.CodeCompetence == id);
        }
    }
}
