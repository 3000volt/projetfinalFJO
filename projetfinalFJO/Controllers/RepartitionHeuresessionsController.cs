﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetfinalFJO.Appdata;

namespace projetfinalFJO.Controllers
{
    public class RepartitionHeuresessionsController : Controller
    {
        private readonly ActualisationContext _context;

        public RepartitionHeuresessionsController(ActualisationContext context)
        {
            _context = context;
        }

        // GET: RepartitionHeuresessions
        public async Task<IActionResult> Index()
        {
            var actualisationContext = _context.RepartitionHeuresession.Include(r => r.AdresseCourrielNavigation).Include(r => r.CodeCompetenceNavigation).Include(r => r.IdsessionNavigation);
            return View(await actualisationContext.ToListAsync());
        }

        // GET: RepartitionHeuresessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repartitionHeuresession = await _context.RepartitionHeuresession
                .Include(r => r.AdresseCourrielNavigation)
                .Include(r => r.CodeCompetenceNavigation)
                .Include(r => r.IdsessionNavigation)
                .FirstOrDefaultAsync(m => m.IdAnalyseRhs == id);
            if (repartitionHeuresession == null)
            {
                return NotFound();
            }

            return View(repartitionHeuresession);
        }

        // GET: RepartitionHeuresessions/Create
        public IActionResult Create()
        {
            ViewData["AdresseCourriel"] = new SelectList(_context.Utilisateur, "AdresseCourriel", "AdresseCourriel");
            ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence");
            ViewData["Idsession"] = new SelectList(_context.Session, "Idsession", "NomSession");
            return View();
        }

        // POST: RepartitionHeuresessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NbhCompetenceCours,ValidationApprouve,IdAnalyseRhs,AdresseCourriel,CodeCompetence,Idsession")] RepartitionHeuresession repartitionHeuresession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(repartitionHeuresession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresseCourriel"] = new SelectList(_context.Utilisateur, "AdresseCourriel", "AdresseCourriel", repartitionHeuresession.AdresseCourriel);
            ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence", repartitionHeuresession.CodeCompetence);
            ViewData["Idsession"] = new SelectList(_context.Session, "Idsession", "NomSession", repartitionHeuresession.Idsession);
            return View(repartitionHeuresession);
        }

        // GET: RepartitionHeuresessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repartitionHeuresession = await _context.RepartitionHeuresession.FindAsync(id);
            if (repartitionHeuresession == null)
            {
                return NotFound();
            }
            ViewData["AdresseCourriel"] = new SelectList(_context.Utilisateur, "AdresseCourriel", "AdresseCourriel", repartitionHeuresession.AdresseCourriel);
            ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence", repartitionHeuresession.CodeCompetence);
            ViewData["Idsession"] = new SelectList(_context.Session, "Idsession", "NomSession", repartitionHeuresession.Idsession);
            return View(repartitionHeuresession);
        }

        // POST: RepartitionHeuresessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NbhCompetenceCours,ValidationApprouve,IdAnalyseRhs,AdresseCourriel,CodeCompetence,Idsession")] RepartitionHeuresession repartitionHeuresession)
        {
            if (id != repartitionHeuresession.IdAnalyseRhs)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repartitionHeuresession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepartitionHeuresessionExists(repartitionHeuresession.IdAnalyseRhs))
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
            ViewData["AdresseCourriel"] = new SelectList(_context.Utilisateur, "AdresseCourriel", "AdresseCourriel", repartitionHeuresession.AdresseCourriel);
            ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence", repartitionHeuresession.CodeCompetence);
            ViewData["Idsession"] = new SelectList(_context.Session, "Idsession", "NomSession", repartitionHeuresession.Idsession);
            return View(repartitionHeuresession);
        }

        // GET: RepartitionHeuresessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repartitionHeuresession = await _context.RepartitionHeuresession
                .Include(r => r.AdresseCourrielNavigation)
                .Include(r => r.CodeCompetenceNavigation)
                .Include(r => r.IdsessionNavigation)
                .FirstOrDefaultAsync(m => m.IdAnalyseRhs == id);
            if (repartitionHeuresession == null)
            {
                return NotFound();
            }

            return View(repartitionHeuresession);
        }

        // POST: RepartitionHeuresessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repartitionHeuresession = await _context.RepartitionHeuresession.FindAsync(id);
            _context.RepartitionHeuresession.Remove(repartitionHeuresession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepartitionHeuresessionExists(int id)
        {
            return _context.RepartitionHeuresession.Any(e => e.IdAnalyseRhs == id);
        }
    }
}