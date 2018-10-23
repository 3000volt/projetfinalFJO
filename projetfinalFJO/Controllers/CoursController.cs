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
    public class CoursController : Controller
    {
        private readonly ActualisationContext _context;

        public CoursController(ActualisationContext context)
        {
            _context = context;
        }

        // GET: Cours
        public async Task<IActionResult> Index()
        {
            var actualisationContext = _context.Cours.Include(c => c.IdsessionNavigation).Include(c => c.NoProgrammeNavigation);
            return View(await actualisationContext.ToListAsync());
        }

        // GET: Cours/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cours = await _context.Cours
                .Include(c => c.IdsessionNavigation)
                .Include(c => c.NoProgrammeNavigation)
                .FirstOrDefaultAsync(m => m.NoCours == id);
            if (cours == null)
            {
                return NotFound();
            }

            return View(cours);
        }

        // GET: Cours/Create
        public IActionResult Create()
        {
            ViewData["Idsession"] = new SelectList(_context.Session, "Idsession", "NomSession");
            ViewData["NoProgramme"] = new SelectList(_context.Programmes, "NoProgramme", "NoProgramme");
            return View();
        }

        // POST: Cours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoCours,NomCours,PonderationCours,DepartementCours,TypedeCours,NoProgramme,Idsession")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cours);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idsession"] = new SelectList(_context.Session, "Idsession", "NomSession", cours.Idsession);
            ViewData["NoProgramme"] = new SelectList(_context.Programmes, "NoProgramme", "NoProgramme", cours.NoProgramme);
            return View(cours);
        }

        // GET: Cours/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cours = await _context.Cours.FindAsync(id);
            if (cours == null)
            {
                return NotFound();
            }
            ViewData["Idsession"] = new SelectList(_context.Session, "Idsession", "NomSession", cours.Idsession);
            ViewData["NoProgramme"] = new SelectList(_context.Programmes, "NoProgramme", "NoProgramme", cours.NoProgramme);
            return View(cours);
        }

        // POST: Cours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NoCours,NomCours,PonderationCours,DepartementCours,TypedeCours,NoProgramme,Idsession")] Cours cours)
        {
            if (id != cours.NoCours)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cours);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoursExists(cours.NoCours))
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
            ViewData["Idsession"] = new SelectList(_context.Session, "Idsession", "NomSession", cours.Idsession);
            ViewData["NoProgramme"] = new SelectList(_context.Programmes, "NoProgramme", "NoProgramme", cours.NoProgramme);
            return View(cours);
        }

        // GET: Cours/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cours = await _context.Cours
                .Include(c => c.IdsessionNavigation)
                .Include(c => c.NoProgrammeNavigation)
                .FirstOrDefaultAsync(m => m.NoCours == id);
            if (cours == null)
            {
                return NotFound();
            }

            return View(cours);
        }

        // POST: Cours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cours = await _context.Cours.FindAsync(id);
            _context.Cours.Remove(cours);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoursExists(string id)
        {
            return _context.Cours.Any(e => e.NoCours == id);
        }
    }
}
