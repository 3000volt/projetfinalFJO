using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetfinalFJO.Appdata;

namespace projetfinalFJO.Controllers
{
    public class GroupeCompetencesController : Controller
    {
        private readonly ActualisationContext _context;

        public GroupeCompetencesController(ActualisationContext context)
        {
            _context = context;
        }

        // GET: GroupeCompetences
        public async Task<IActionResult> Index()
        {
            var actualisationContext = _context.GroupeCompetence.Include(g => g.CodeCompetenceNavigation).Include(g => g.NomGroupeNavigation);
            return View(await actualisationContext.ToListAsync());
        }

        // GET: GroupeCompetences/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupeCompetence = await _context.GroupeCompetence
                .Include(g => g.CodeCompetenceNavigation)
                .Include(g => g.NomGroupeNavigation)
                .FirstOrDefaultAsync(m => m.NomGroupe == id);
            if (groupeCompetence == null)
            {
                return NotFound();
            }

            return View(groupeCompetence);
        }

        // GET: GroupeCompetences/Create
        public IActionResult Create()
        {
            ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence");
            ViewData["NomGroupe"] = new SelectList(_context.Groupe, "NomGroupe", "NomGroupe");
            return View();
        }

        // POST: GroupeCompetences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomGroupe,CodeCompetence,NoProgramme")] GroupeCompetence groupeCompetence)
        {
            groupeCompetence.NoProgramme = this.HttpContext.Session.GetString("programme");
            if (ModelState.IsValid)
            {
                _context.Add(groupeCompetence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence", groupeCompetence.CodeCompetence);
            ViewData["NomGroupe"] = new SelectList(_context.Groupe, "NomGroupe", "NomGroupe", groupeCompetence.NomGroupe);
            return View(groupeCompetence);
        }

        // GET: GroupeCompetences/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupeCompetence = await _context.GroupeCompetence.FindAsync(id);
            if (groupeCompetence == null)
            {
                return NotFound();
            }
            ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence", groupeCompetence.CodeCompetence);
            ViewData["NomGroupe"] = new SelectList(_context.Groupe, "NomGroupe", "NomGroupe", groupeCompetence.NomGroupe);
            return View(groupeCompetence);
        }

        // POST: GroupeCompetences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NomGroupe,CodeCompetence,NoProgramme")] GroupeCompetence groupeCompetence)
        {
            if (id != groupeCompetence.NomGroupe)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupeCompetence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupeCompetenceExists(groupeCompetence.NomGroupe))
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
            ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence", groupeCompetence.CodeCompetence);
            ViewData["NomGroupe"] = new SelectList(_context.Groupe, "NomGroupe", "NomGroupe", groupeCompetence.NomGroupe);
            return View(groupeCompetence);
        }

        // GET: GroupeCompetences/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupeCompetence = await _context.GroupeCompetence
                .Include(g => g.CodeCompetenceNavigation)
                .Include(g => g.NomGroupeNavigation)
                .FirstOrDefaultAsync(m => m.NomGroupe == id);
            if (groupeCompetence == null)
            {
                return NotFound();
            }

            return View(groupeCompetence);
        }

        // POST: GroupeCompetences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var groupeCompetence = await _context.GroupeCompetence.FindAsync(id);
            _context.GroupeCompetence.Remove(groupeCompetence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupeCompetenceExists(string id)
        {
            return _context.GroupeCompetence.Any(e => e.NomGroupe == id);
        }
    }
}
