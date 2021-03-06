﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetfinalFJO.Appdata;

namespace projetfinalFJO.Controllers
{

    [Authorize(Roles = "Admin,Sous_Commite,Srdp,Commite_Programme")]
    public class RepartitionHeureCoursController : Controller
    {
        private readonly ActualisationContext _context;

        public RepartitionHeureCoursController(ActualisationContext context)
        {
            _context = context;
        }

        // GET: RepartitionHeureCours
        public async Task<IActionResult> Index()
        {
            try
            {
                var actualisationContext = _context.RepartitionHeureCours.Include(r => r.AdresseCourrielNavigation).Include(r => r.CodeCompetenceNavigation).Include(r => r.NoCoursNavigation);
                return View(await actualisationContext.Where(x => x.NoProgramme.Equals(this.HttpContext.Session.GetString("programme"))).ToListAsync());
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // GET: RepartitionHeureCours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var repartitionHeureCours = await _context.RepartitionHeureCours
                    .Include(r => r.AdresseCourrielNavigation)
                    .Include(r => r.CodeCompetenceNavigation)
                    .Include(r => r.NoCoursNavigation)
                    .FirstOrDefaultAsync(m => m.IdAnalyseRhc == id);
                if (repartitionHeureCours == null)
                {
                    return NotFound();
                }

                return View(repartitionHeureCours);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // GET: RepartitionHeureCours/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["AdresseCourriel"] = new SelectList(_context.Utilisateur, "AdresseCourriel", "AdresseCourriel");
                ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence");
                ViewData["NoCours"] = new SelectList(_context.Cours, "NoCours", "NoCours");
                return View();
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
         
        }

        // POST: RepartitionHeureCours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nbreheurcours,IdAnalyseRhc,AdresseCourriel,NoCours,CodeCompetence,NomSession,ValidationApprouve,NoProgramme")] RepartitionHeureCours repartitionHeureCours)
        {
            try
            {
                repartitionHeureCours.NoProgramme = this.HttpContext.Session.GetString("programme");
                if (ModelState.IsValid)
                {
                    _context.Add(repartitionHeureCours);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["AdresseCourriel"] = new SelectList(_context.Utilisateur, "AdresseCourriel", "AdresseCourriel", repartitionHeureCours.AdresseCourriel);
                ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence", repartitionHeureCours.CodeCompetence);
                ViewData["NoCours"] = new SelectList(_context.Cours, "NoCours", "NoCours", repartitionHeureCours.NoCours);
                return View(repartitionHeureCours);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // GET: RepartitionHeureCours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var repartitionHeureCours = await _context.RepartitionHeureCours.FindAsync(id);
                if (repartitionHeureCours == null)
                {
                    return NotFound();
                }
                ViewData["AdresseCourriel"] = new SelectList(_context.Utilisateur, "AdresseCourriel", "AdresseCourriel", repartitionHeureCours.AdresseCourriel);
                ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence", repartitionHeureCours.CodeCompetence);
                ViewData["NoCours"] = new SelectList(_context.Cours, "NoCours", "NoCours", repartitionHeureCours.NoCours);
                return View(repartitionHeureCours);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // POST: RepartitionHeureCours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nbreheurcours,IdAnalyseRhc,AdresseCourriel,NoCours,CodeCompetence,NomSession,ValidationApprouve,NoProgramme")] RepartitionHeureCours repartitionHeureCours)
        {
            try
            {
                if (id != repartitionHeureCours.IdAnalyseRhc)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(repartitionHeureCours);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!RepartitionHeureCoursExists(repartitionHeureCours.IdAnalyseRhc))
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
                ViewData["AdresseCourriel"] = new SelectList(_context.Utilisateur, "AdresseCourriel", "AdresseCourriel", repartitionHeureCours.AdresseCourriel);
                ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence", repartitionHeureCours.CodeCompetence);
                ViewData["NoCours"] = new SelectList(_context.Cours, "NoCours", "NoCours", repartitionHeureCours.NoCours);
                return View(repartitionHeureCours);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // GET: RepartitionHeureCours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var repartitionHeureCours = await _context.RepartitionHeureCours
                    .Include(r => r.AdresseCourrielNavigation)
                    .Include(r => r.CodeCompetenceNavigation)
                    .Include(r => r.NoCoursNavigation)
                    .FirstOrDefaultAsync(m => m.IdAnalyseRhc == id);
                if (repartitionHeureCours == null)
                {
                    return NotFound();
                }

                return View(repartitionHeureCours);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // POST: RepartitionHeureCours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var repartitionHeureCours = await _context.RepartitionHeureCours.FindAsync(id);
                _context.RepartitionHeureCours.Remove(repartitionHeureCours);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
           
        }

        private bool RepartitionHeureCoursExists(int id)
        {
            return _context.RepartitionHeureCours.Any(e => e.IdAnalyseRhc == id);
        }
    }
}
