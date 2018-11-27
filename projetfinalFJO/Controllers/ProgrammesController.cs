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
    public class ProgrammesController : Controller
    {
        private readonly ActualisationContext _context;

        public ProgrammesController(ActualisationContext context)
        {
            _context = context;
        }

        // GET: Programmes
        public async Task<IActionResult> ListeProgrammes()
        {
            return View(await _context.Programmes.ToListAsync());
        }

        // GET: Programmes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programmes = await _context.Programmes
                .FirstOrDefaultAsync(m => m.NoProgramme == id);
            if (programmes == null)
            {
                return NotFound();
            }

            return View(programmes);
        }

        // GET: Programmes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Programmes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoProgramme,NomProgramme,NbHeure,NbUnite,NbCompetencesObligatoires,NbCompetencesOptionnelles,CondtionsAdmission")] Programmes programmes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programmes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListeProgrammes));
            }
            return View(programmes);
        }

        // GET: Programmes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programmes = await _context.Programmes.FindAsync(id);
            if (programmes == null)
            {
                return NotFound();
            }
            return View(programmes);
        }

        // POST: Programmes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NoProgramme,NomProgramme,NbHeure,NbUnite,NbCompetencesObligatoires,NbCompetencesOptionnelles,CondtionsAdmission")] Programmes programmes)
        {
            if (id != programmes.NoProgramme)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programmes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgrammesExists(programmes.NoProgramme))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ListeProgrammes));
            }
            return View(programmes);
        }

        // GET: Programmes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programmes = await _context.Programmes
                .FirstOrDefaultAsync(m => m.NoProgramme == id);
            if (programmes == null)
            {
                return NotFound();
            }

            return View(programmes);
        }

        // POST: Programmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var programmes = await _context.Programmes.FindAsync(id);
            _context.Programmes.Remove(programmes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListeProgrammes));
        }

        private bool ProgrammesExists(string id)
        {
            return _context.Programmes.Any(e => e.NoProgramme == id);
        }
    }
}
