﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetfinalFJO.Appdata;
using projetfinalFJO.Models;

namespace projetfinalFJO.Controllers
{
    [Authorize(Roles = "Admin,Sous_Commite,Srdp,Commite_Programme")]

    public class ProgrammesController : Controller
    {
        private readonly ActualisationContext _context;

        public ProgrammesController(ActualisationContext context)
        {
            _context = context;
        }

        // GET: Programmes
        public async Task<IActionResult> List_Programme()
        {
            try
            {
                return View(await _context.Programmes.ToListAsync());
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // GET: Programmes/Details/5
        public async Task<IActionResult> InfoProgramme(string id)
        {
            
            try
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
                ProgrammesDetail details = new ProgrammesDetail();
                details.program = programmes;
                details.ListComp = _context.Competences.ToList().FindAll(x => x.NoProgramme == id);
                details.comp = new Competences();
                var test = details;


                return View(test);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
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
        public async Task<IActionResult> Create([Bind("NoProgramme,NomProgramme,NbHeureFormationGenerale,NbUniteFormationGenerale,NbHeureFormationTechnique,NbUniteFormationTechnique,NbCompetencesObligatoires,NbCompetencesOptionnelles,CondtionsAdmission")] Programmes programmes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(programmes);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(List_Programme));
                }
                return View(programmes);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
        }

        // GET: Programmes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            try
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
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
        }

        // POST: Programmes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NoProgramme,NomProgramme,NbHeureFormationGenerale,NbUniteFormationGenerale,NbHeureFormationTechnique,NbUniteFormationTechnique,NbCompetencesObligatoires,NbCompetencesOptionnelles,CondtionsAdmission")] Programmes programmes)
        {
            try
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
                    return RedirectToAction(nameof(List_Programme));
                }
                return View(programmes);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
        }

        // GET: Programmes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            try
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
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
        }

        // POST: Programmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var programmes = await _context.Programmes.FindAsync(id);
                _context.Programmes.Remove(programmes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List_Programme));
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }

        }

        private bool ProgrammesExists(string id)
        {
            return _context.Programmes.Any(e => e.NoProgramme == id);
        }
    }
}