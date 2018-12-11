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

        public ActionResult ListeElementCompetence(string id)
        {
            //Il faut retourner la liste des élément sde compétences de la compétence concernée
            List<CompetencesElementCompetence> listeCompElemComp = this._context.CompetencesElementCompetence.ToList().FindAll(x => x.CodeCompetence == id);
            List<string> listeNomElem = new List<string>();
            foreach (CompetencesElementCompetence compElemComp in listeCompElemComp)
            {
                listeNomElem.Add(compElemComp.ElementCompétence);
            }
            List<Elementcompetence> listeElemComp = new List<Elementcompetence>();
            foreach (string listeElem in listeNomElem)
            {
                listeElemComp.Add(this._context.Elementcompetence.ToList().Find(x => x.ElementCompétence == listeElem));
            }
            //Ajouter une session de la compétence
            this.HttpContext.Session.SetString("Competence", id);
            return View(listeElemComp);
        }

        public ActionResult ListeElementCompetence2()
        {
            string id = this.HttpContext.Session.GetString("Competence");
            //Il faut retourner la liste des élément sde compétences de la compétence concernée
            List<CompetencesElementCompetence> listeCompElemComp = this._context.CompetencesElementCompetence.ToList().FindAll(x => x.CodeCompetence == id);
            List<string> listeNomElem = new List<string>();
            foreach (CompetencesElementCompetence compElemComp in listeCompElemComp)
            {
                listeNomElem.Add(compElemComp.ElementCompétence);
            }
            List<Elementcompetence> listeElemComp = new List<Elementcompetence>();
            foreach (string listeElem in listeNomElem)
            {
                listeElemComp.Add(this._context.Elementcompetence.ToList().Find(x => x.ElementCompétence == listeElem));
            }
            return View("ListeElementCompetence", listeElemComp);
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
        public IActionResult Creer()
        {
            return View();
        }

        // POST: Elementcompetences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Creer(Elementcompetence elementcompetence)//[FromBody][Bind("ElementCompétence,CriterePerformance")]
        {
            elementcompetence.NoProgramme = this.HttpContext.Session.GetString("programme");

            if (ModelState.IsValid)
            {
                //Ajouter a la table CompetenceElementCompetence
                CompetencesElementCompetence compElemComp = new CompetencesElementCompetence()
                {
                    CodeCompetence = this.HttpContext.Session.GetString("Competence"),//TODO
                    ElementCompétence = elementcompetence.ElementCompétence,
                    NoProgramme = this.HttpContext.Session.GetString("programme")
                };
                _context.Add(compElemComp);
                //Ajouter a la table element de compétence
                _context.Add(elementcompetence);
                await _context.SaveChangesAsync();
                return RedirectToAction("ListeElementCompetence", new { id = this.HttpContext.Session.GetString("Competence") });
            }
            return BadRequest("élément non ajouté");
        }


        // GET: Elementcompetences/Edit/5
        public async Task<IActionResult> Modifier(string id)
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
        public async Task<IActionResult> Modifier(string id, [Bind("ElementCompétence,CriterePerformance,NoProgramme")] Elementcompetence elementcompetence)
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
                return View("ListeElementCompetence", new { id = this.HttpContext.Session.GetString("Competence") });
            }
            return View(elementcompetence);
        }

        // GET: Elementcompetences/Delete/5
        public async Task<IActionResult> Supprimer(string id)
        {
            if (id == null)
            {
                return NotFound();//
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
        [HttpPost, ActionName("Supprimer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SupprimerConfirme(Elementcompetence elementcompetence)//string id, [Bind("ElementCompétence,CriterePerformance,NoProgramme")]
        {
            //var elementcompetence = await _context.Elementcompetence.FindAsync(id);
            _context.Elementcompetence.Remove(elementcompetence);
            await _context.SaveChangesAsync();
            return View("ListeElementCompetence", new { id = this.HttpContext.Session.GetString("Competence") });
        }

        private bool ElementcompetenceExists(string id)
        {
            return _context.Elementcompetence.Any(e => e.ElementCompétence == id);
        }
    }
}
