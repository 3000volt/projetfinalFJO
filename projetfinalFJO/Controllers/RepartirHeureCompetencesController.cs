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
    public class RepartirHeureCompetencesController : Controller
    {
        private readonly ActualisationContext _context;

        public RepartirHeureCompetencesController(ActualisationContext context)
        {
            _context = context;
        }

        // GET: RepartirHeureCompetences
        public async Task<IActionResult> Index()
        {
            var actualisationContext = _context.RepartirHeureCompetence.Include(r => r.CodeCompetenceNavigation);
            return View(await actualisationContext.ToListAsync());
        }

        // GET: RepartirHeureCompetences/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repartirHeureCompetence = await _context.RepartirHeureCompetence
                .Include(r => r.CodeCompetenceNavigation)
                .FirstOrDefaultAsync(m => m.CodeCompetence == id);
            if (repartirHeureCompetence == null)
            {
                return NotFound();
            }

            return View(repartirHeureCompetence);
        }

        // GET: RepartirHeureCompetences/Create
        public IActionResult Create()
        {

            ViewData["AdresseCourriel"] = new SelectList(_context.Utilisateur, "AdresseCourriel", "AdresseCourriel");
            ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence");
           
            ViewBag.CompHeureRe = new RepartirHeureCompetence();
            ViewBag.SessionRep = new RepartitionHeuresession();
            return View();
        }

        // POST: RepartirHeureCompetences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NbHsessionCompetence,CodeCompetence,NomSession,ValidationApprouve,NoProgramme")] RepartirHeureCompetence repartirHeureCompetence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(repartirHeureCompetence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence", repartirHeureCompetence.CodeCompetence);
            return View(repartirHeureCompetence);
        }

        // GET: RepartirHeureCompetences/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repartirHeureCompetence = await _context.RepartirHeureCompetence.FindAsync(id);
            if (repartirHeureCompetence == null)
            {
                return NotFound();
            }
            ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence", repartirHeureCompetence.CodeCompetence);
            return View(repartirHeureCompetence);
        }

        // POST: RepartirHeureCompetences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NbHsessionCompetence,CodeCompetence,NomSession,ValidationApprouve,NoProgramme")] RepartirHeureCompetence repartirHeureCompetence)
        {
            if (id != repartirHeureCompetence.CodeCompetence)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repartirHeureCompetence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepartirHeureCompetenceExists(repartirHeureCompetence.CodeCompetence))
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
            ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence", repartirHeureCompetence.CodeCompetence);
            return View(repartirHeureCompetence);
        }

        // GET: RepartirHeureCompetences/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repartirHeureCompetence = await _context.RepartirHeureCompetence
                .Include(r => r.CodeCompetenceNavigation)
                .FirstOrDefaultAsync(m => m.CodeCompetence == id);
            if (repartirHeureCompetence == null)
            {
                return NotFound();
            }

            return View(repartirHeureCompetence);
        }

        // POST: RepartirHeureCompetences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var repartirHeureCompetence = await _context.RepartirHeureCompetence.FindAsync(id);
            _context.RepartirHeureCompetence.Remove(repartirHeureCompetence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepartirHeureCompetenceExists(string id)
        {
            return _context.RepartirHeureCompetence.Any(e => e.CodeCompetence == id);
        }


        //https://stackoverflow.com/questions/25077472/load-asp-net-mvc-partial-view-on-jqueryui-tab-selection
        //partial view qui se refresh lorsque le tab correspondant a la view est appellée
        public PartialViewResult partialGroupe()
        {
          
            ViewBag.groupe2 = new Groupe();
            return PartialView("_PartialGroupe");
        }
        public PartialViewResult partialGroupeComp()
        {
            ViewBag.groupe = new GroupeCompetence();
            ViewData["CodeCompetence"] = new SelectList(_context.Competences, "CodeCompetence", "CodeCompetence");
            ViewData["NomGroupe"] = new SelectList(_context.Groupe, "NomGroupe", "NomGroupe");
            return PartialView("_PartialGroupeComp");
        }
    }
}
