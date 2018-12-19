using System;
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
    public class GroupesController : Controller
    {
        private readonly ActualisationContext _context;

        public GroupesController(ActualisationContext context)
        {
            _context = context;
        }

        // GET: Groupes
        public async Task<IActionResult> List_groupe()
        {
            try
            {
                return View(await _context.Groupe.Where(x => x.NoProgramme == this.HttpContext.Session.GetString("programme")).ToListAsync());
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
           
        }
   
        // GET: Groupes/Create
        public IActionResult Create()
        {
            //permet de créer un groupe
            try
            {
                ViewData["NoProgramme"] = new SelectList(_context.Programmes.Where(x => x.NoProgramme == this.HttpContext.Session.GetString("programme")), "NoProgramme", "NoProgramme");
                return View();
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // POST: Groupes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody][Bind("NomGroupe,NoProgramme")] Groupe groupe)
        {
            //permet de créer un groupe
            try
            {
                groupe.NoProgramme = this.HttpContext.Session.GetString("programme"); ;
                if (ModelState.IsValid)
                {
                    _context.Add(groupe);
                    await _context.SaveChangesAsync();
                    return Ok("ajout reussi");
                }
                return BadRequest("groupe non ajouté");
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
           
        }

        // POST: Groupes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CréerGroupe(/*[FromBody]*/[Bind("NomGroupe,NoProgramme")] Groupe groupe)
        {
            try
            {
                groupe.NoProgramme = this.HttpContext.Session.GetString("programme"); ;
                if (ModelState.IsValid)
                {
                    _context.Add(groupe);
                    await _context.SaveChangesAsync();
                    RedirectToAction(nameof(List_groupe));
                }
                return BadRequest("groupe non ajouté");
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // GET: Groupes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            //permet de modifier un groupe
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var groupe = await _context.Groupe.FindAsync(id);
                if (groupe == null)
                {
                    return NotFound();
                }
                return View(groupe);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // POST: Groupes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NomGroupe,NoProgramme")] Groupe groupe)
        {
            //permet de modifier un groupe
            try
            {
                if (id != groupe.NomGroupe)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(groupe);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!GroupeExists(groupe.NomGroupe))
                        {
                            return View("\\Views\\Shared\\page_erreur.cshtml");
                        }
                        else
                        {
                            return View("\\Views\\Shared\\page_erreur.cshtml");
                        }
                    }
                    return RedirectToAction(nameof(List_groupe));
                }
                return View(groupe);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
          
        }

        // GET: Groupes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            //permet de supprimer un groupe
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var groupe = await _context.Groupe
                    .FirstOrDefaultAsync(m => m.NomGroupe == id);
                if (groupe == null)
                {
                    return NotFound();
                }

                return View(groupe);
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        // POST: Groupes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            //permet de supprimer un groupe
            try
            {
                var groupe = await _context.Groupe.FindAsync(id);
                _context.Groupe.Remove(groupe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List_groupe));
            }
            catch (Exception e)
            {
                return View("\\Views\\Shared\\page_erreur.cshtml");
            }
            
        }

        private bool GroupeExists(string id)
        {
            return _context.Groupe.Any(e => e.NomGroupe == id);
        }
    }
}
