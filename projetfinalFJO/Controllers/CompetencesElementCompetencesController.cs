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
    public class CompetencesElementCompetencesController : Controller
    {
        private readonly ActualisationContext _context;

        public CompetencesElementCompetencesController(ActualisationContext context)
        {
            _context = context;
        }

        // GET: CompetencesElementCompetences
        public async Task<IActionResult> Index()
        {
            var actualisationContext = _context.CompetencesElementCompetence.Include(c => c.CodeCompetenceNavigation).Include(c => c.ElementCompétenceNavigation);
            return View(await actualisationContext.ToListAsync());
        }

        // GET: CompetencesElementCompetences/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competencesElementCompetence = await _context.CompetencesElementCompetence
                .Include(c => c.CodeCompetenceNavigation)
                .Include(c => c.ElementCompétenceNavigation)
                .FirstOrDefaultAsync(m => m.CodeCompetence == id);
            if (competencesElementCompetence == null)
            {
                return NotFound();
            }

            return View(competencesElementCompetence);
        }

        // GET: CompetencesElementCompetences/Create
        public IActionResult Create()
        {
            ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence");
            ViewData["Idelementcomp"] = new SelectList(_context.Elementcompetence, "Idelementcomp", "CriterePerformance");
            return View();
        }

        // POST: CompetencesElementCompetences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody][Bind("CodeCompetence,ElementCompétence")] CompetencesElementCompetence competencesElementCompetence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competencesElementCompetence);
                await _context.SaveChangesAsync();
                return Ok("élément ajouté avec succès");
            }
            ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence", competencesElementCompetence.CodeCompetence);
            ViewData["Idelementcomp"] = new SelectList(_context.Elementcompetence, "Idelementcomp", "Idelementcomp", competencesElementCompetence.ElementCompétence);
            return BadRequest("élément non ajouté");
        }

        // GET: CompetencesElementCompetences/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competencesElementCompetence = await _context.CompetencesElementCompetence.FindAsync(id);
            if (competencesElementCompetence == null)
            {
                return NotFound();
            }
            ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence", competencesElementCompetence.CodeCompetence);
            ViewData["Idelementcomp"] = new SelectList(_context.Elementcompetence, "Idelementcomp", "CriterePerformance", competencesElementCompetence.ElementCompétence);
            return View(competencesElementCompetence);
        }

        // POST: CompetencesElementCompetences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodeCompetence,ElementCompétence")] CompetencesElementCompetence competencesElementCompetence)
        {
            if (id != competencesElementCompetence.CodeCompetence)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competencesElementCompetence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetencesElementCompetenceExists(competencesElementCompetence.CodeCompetence))
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
            ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence", competencesElementCompetence.CodeCompetence);
            ViewData["Idelementcomp"] = new SelectList(_context.Elementcompetence, "Idelementcomp", "CriterePerformance", competencesElementCompetence.ElementCompétence);
            return View(competencesElementCompetence);
        }

        // GET: CompetencesElementCompetences/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competencesElementCompetence = await _context.CompetencesElementCompetence
                .Include(c => c.CodeCompetenceNavigation)
                .Include(c => c.ElementCompétenceNavigation)
                .FirstOrDefaultAsync(m => m.CodeCompetence == id);
            if (competencesElementCompetence == null)
            {
                return NotFound();
            }

            return View(competencesElementCompetence);
        }

        // POST: CompetencesElementCompetences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var competencesElementCompetence = await _context.CompetencesElementCompetence.FindAsync(id);
            _context.CompetencesElementCompetence.Remove(competencesElementCompetence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetencesElementCompetenceExists(string id)
        {
            return _context.CompetencesElementCompetence.Any(e => e.CodeCompetence == id);
        }
    }
}
