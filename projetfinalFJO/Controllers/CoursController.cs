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
    public class CoursController : Controller
    {
        //propriétés
        private readonly ActualisationContext _contexte;

        public CoursController(ActualisationContext contexte)
        {
            _contexte = contexte;
        }

        /// <summary>
        /// Affiche la liste de cours
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ListeCours()
        {
            return View(await _contexte.Cours.ToListAsync());
        }
        /// <summary>
        /// Affiche la vue pour ajouter un cours
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AjouterCours()
        {
            return View();
        }
        /// <summary>
        /// Ajoute un cours
        /// </summary>
        /// <param name="cours"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AjouterCours(Cours cours)
        {
            //ViewData[""] = new SelectList(_contexte.Groupe, "", "");
            ViewData["NoProgramme"] = new SelectList(_contexte.Programmes, "NoProgramme", "NoProgramme");

            return View();
        }
        /// <summary>
        /// Affiche les détails du cours
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cours = await _contexte.Cours.FirstOrDefaultAsync(x => x.NoCours == id);
            if (cours == null)
            {
                return NotFound();
            }

            return View(cours);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ModifierCours()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SupprimerCours()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cours"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SupprimerCours(Cours cours)
        {
            return View();
        }
    }
}