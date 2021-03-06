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
    public class CompetencesElementCompetencesController : Controller
    {
        private readonly ActualisationContext _context;

        public CompetencesElementCompetencesController(ActualisationContext context)
        {
            _context = context;
        }

        // GET: CompetencesElementCompetences
        public async Task<IActionResult> ListComp_Elem()
        {
            try
            {
                var actualisationContext = _context.CompetencesElementCompetence.Include(c => c.CodeCompetenceNavigation).Include(c => c.ElementCompétenceNavigation).Where(x => x.NoProgramme.Equals(this.HttpContext.Session.GetString("programme")));
                return View(await actualisationContext.ToListAsync());
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // GET: CompetencesElementCompetences/Details/5
        public async Task<IActionResult> Details(string id)
        {
            try
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
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // GET: CompetencesElementCompetences/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence");
                ViewData["Idelementcomp"] = new SelectList(_context.Elementcompetence, "Idelementcomp", "CriterePerformance");
                return View();
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
           
        }

        // POST: CompetencesElementCompetences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody][Bind("CodeCompetence,ElementCompétence,NoProgramme")] CompetencesElementCompetence competencesElementCompetence)
        {
            try
            {
                competencesElementCompetence.NoProgramme = this.HttpContext.Session.GetString("programme");
                if (ModelState.IsValid)
                {
                    _context.Add(competencesElementCompetence);
                    await _context.SaveChangesAsync();
                    //retoune les critères de performance de la compétence
                    return Ok(_context.Elementcompetence.ToList().Find(x => x.ElementCompétence == competencesElementCompetence.ElementCompétence).CriterePerformance);
                }
                ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence", competencesElementCompetence.CodeCompetence);
                ViewData["Idelementcomp"] = new SelectList(_context.Elementcompetence, "Idelementcomp", "Idelementcomp", competencesElementCompetence.ElementCompétence);
                return BadRequest("élément non ajouté");
            }
            catch (Exception e)
            {
                return BadRequest("Probleme de clée primaire");
            }
            
        }

        // GET: CompetencesElementCompetences/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            try
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
                ViewData["Idelementcomp"] = new SelectList(_context.Elementcompetence, "Idelementcomp", "Idelementcomp", competencesElementCompetence.ElementCompétence);//changé CriterePerformance par Idelementcomp
                return View(competencesElementCompetence);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // POST: CompetencesElementCompetences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodeCompetence,ElementCompétence,NoProgramme")] CompetencesElementCompetence competencesElementCompetence)
        {
            try
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
                    return RedirectToAction(nameof(ListComp_Elem));
                }
                ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence", competencesElementCompetence.CodeCompetence);
                ViewData["Idelementcomp"] = new SelectList(_context.Elementcompetence, "Idelementcomp", "CriterePerformance", competencesElementCompetence.ElementCompétence);
                return View(competencesElementCompetence);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // GET: CompetencesElementCompetences/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            try
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
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // POST: CompetencesElementCompetences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var competencesElementCompetence = await _context.CompetencesElementCompetence.FindAsync(id);
                _context.CompetencesElementCompetence.Remove(competencesElementCompetence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListComp_Elem));
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
           
        }

        private bool CompetencesElementCompetenceExists(string id)
        {
            return _context.CompetencesElementCompetence.Any(e => e.CodeCompetence == id);
        }

        // POST: CompetencesElementCompetences/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> supprimer([FromBody][Bind("CodeCompetence,ElementCompétence,NoProgramme")] CompetencesElementCompetence comp)
        {
            try
            {
                comp.NoProgramme = this.HttpContext.Session.GetString("programme");
                if (ModelState.IsValid)
                {
                    _context.CompetencesElementCompetence.Remove(comp);
                    await _context.SaveChangesAsync();
                    return Ok(comp.ElementCompétence);
                }

                return BadRequest("élément non supprimer");
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
           
        }
    }
}
