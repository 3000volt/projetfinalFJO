using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using projetfinalFJO.Appdata;

namespace projetfinalFJO.Controllers
{
    public class CoursController : Controller
    {
        //propriétés
        private readonly ActualisationContext _contexte;

        //Constructeur
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
            return View(await _contexte.Cours.Where(x => x.NoProgramme.Equals(this.HttpContext.Session.GetString("programme"))).ToListAsync());
        }
        /// <summary>
        /// Affiche la vue pour ajouter un cours
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AjouterCours()
        {  //peupler les listes
            ViewBag.NomGroupe = new SelectList(_contexte.Groupe, "NomGroupe", "NomGroupe");
            ViewBag.NoProgramme = new SelectList(_contexte.Programmes, "NoProgramme", "NoProgramme");
            ViewBag.NomSession = new SelectList(_contexte.Session,"NomSession","NomSession");
            ViewBag.Cours = new Cours();
            return View();
        }
        /// <summary>
        /// Ajoute un cours
        /// </summary>
        /// <param name="cours"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjouterCours([FromBody][Bind("NoCours,NomCours,PonderationCours,DepartementCours,TypedeCours,NoProgramme,NomSession,NomGroupe")]Cours cours)
        {
           if(ModelState.IsValid)
            {
                //HttpContext.Session.SetString("Cours",JsonConvert.SerializeObject(cours));
                _contexte.Add(cours);
                await _contexte.SaveChangesAsync();

                return RedirectToAction(nameof(ListeCours));
                //return Json(Url.Action("ListeCours","Cours"));
            }

            return BadRequest("Erreur,Le cours n'a pas pu être ajouté");
        }
        /// <summary>
        /// Affiche les détails du cours
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> DetailsCours(string id)
        {
            //vérifier si l'id est null
            if (id == null)
                return NotFound();
        

            //récupérer le cours
            Cours cours = await _contexte.Cours.FindAsync(id);

            //vérifier si le cours est null
            if (cours == null)
                return NotFound();

            return View(cours);
        }

        /// <summary>
        /// Affichage pour modifier un cours éxistant
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ModifierCours(string id)
        { 
            //vérifier si l'id est null
            if (id == null)
                return NotFound();
            

            //récupérer le cours
            Cours cours = await _contexte.Cours.FindAsync(id);
            
            //vérifier si le cours est null
            if (cours == null)
            {
                return NotFound();
            }
            
            //peupler les 3 listes
            ViewBag.NomGroupe = new SelectList(_contexte.Groupe, "NomGroupe", "NomGroupe");
            ViewBag.NoProgramme = new SelectList(_contexte.Programmes, "NoProgramme", "NoProgramme");
            ViewBag.NomSession = new SelectList(_contexte.Session, "NomSession", "NomSession");

            return View(cours);
        }
        /// <summary>
        /// Modifier un cours éxistant
        /// </summary>
        /// <param name="cours"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ModifierCours([Bind("NoCours,NomCours,PonderationCours,DepartementCours,TypedeCours,NoProgramme,NomSession,NomGroupe")]Cours cours)
        {
            if (ModelState.IsValid)
            {
                _contexte.Update(cours);
                await _contexte.SaveChangesAsync();

                return RedirectToAction(nameof(ListeCours));
            }
            return BadRequest("Erreur,Le cours n'a pas pu être modifié");
        }
        /// <summary>
        /// Afficher la vue pour supprimer un cours
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SupprimerCours(string id)
        {
            //vérifier si l'id est null
            if(id == null)
                return NotFound();
           
            //aller chercher le cours dans le contexte
            Cours cours = await _contexte.Cours.FindAsync(id);

            //vérifier si le cours est null
            if (cours == null)
                return NotFound();
            
            return View(cours);
        }
        /// <summary>
        /// Supprimer le cours
        /// </summary>
        /// <param name="cours"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Supprimer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SupprimerCoursPost([FromBody]string NoCours)
        {
            var cours = await _contexte.Cours.FindAsync(NoCours);
            _contexte.Cours.Remove(cours);
            await _contexte.SaveChangesAsync();
            return RedirectToAction("ListeCours");
        }
    }
}