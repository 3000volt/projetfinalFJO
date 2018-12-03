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
    public class ElementcompetencesController : Controller
    {
        private readonly ActualisationContext _context;

        public ElementcompetencesController(ActualisationContext context)
        {
            _context = context;
        }

        // GET: Elementcompetences
        public async Task<IActionResult> Index()
        {
            return View(await _context.Elementcompetence.Where(x => x.NoProgramme.Equals(this.HttpContext.Session.GetString("programme"))).ToListAsync());
        }

        // GET: Elementcompetences/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elementcompetence = await _context.Elementcompetence
                .FirstOrDefaultAsync(m => m.ElementCompétence == id);
            if (elementcompetence == null)
            {
                return NotFound();
            }

            return View(elementcompetence);
        }

        // GET: Elementcompetences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Elementcompetences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody][Bind("ElementCompétence,CriterePerformance,NoProgramme")] Elementcompetence elementcompetence)
        {
            elementcompetence.NoProgramme = this.HttpContext.Session.GetString("programme");
            if (ModelState.IsValid)
            {
                _context.Add(elementcompetence);
                await _context.SaveChangesAsync();
                return Ok("élément ajouté avec succès");
            }
            return BadRequest("élément non ajouté");
        }


        // GET: Elementcompetences/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elementcompetence = await _context.Elementcompetence.FindAsync(id);
            if (elementcompetence == null)
            {
                return NotFound();
            }
            return View(elementcompetence);
        }

        // POST: Elementcompetences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ElementCompétence,CriterePerformance,NoProgramme")] Elementcompetence elementcompetence)
        {
            if (id != elementcompetence.ElementCompétence)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(elementcompetence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElementcompetenceExists(elementcompetence.ElementCompétence))
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
            return View(elementcompetence);
        }

        // GET: Elementcompetences/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elementcompetence = await _context.Elementcompetence
                .FirstOrDefaultAsync(m => m.ElementCompétence == id);
            if (elementcompetence == null)
            {
                return NotFound();
            }

            return View(elementcompetence);
        }

        // POST: Elementcompetences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var elementcompetence = await _context.Elementcompetence.FindAsync(id);
            _context.Elementcompetence.Remove(elementcompetence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElementcompetenceExists(string id)
        {
            return _context.Elementcompetence.Any(e => e.ElementCompétence == id);
        }
    }
}
