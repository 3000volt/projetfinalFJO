using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using projetfinalFJO.Appdata;

namespace projetfinalFJO.Controllers
{
    [Authorize(Roles = "Admin,Sous_Commite,Srdp,Commite_Programme")]
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
        public async Task<IActionResult> ListeCours(string search)
        {
            return View(await _contexte.Cours.Where(x => x.NomCours.StartsWith(search) || search == null).ToListAsync());
        }

        [HttpPost]
        public List<string> ChargerGroupe(string NomSession)
        {
            List<GroupeCompetence> listeGroupeCompetence = this._contexte.GroupeCompetence.ToList();
            listeGroupeCompetence.RemoveAll(x => x.NomSession != NomSession);
            List<string> listeGroupe = new List<string>();
            foreach (GroupeCompetence g in listeGroupeCompetence)
            {
                //voir si le groupe est deja dans la liste
                if (!listeGroupe.Contains(g.NomGroupe))
                {
                    listeGroupe.Add(g.NomGroupe);
                }
            }
            return listeGroupe;
        }

        public async Task<IActionResult> ListeCoursGeneral()
        {
            return View(await _contexte.Cours.Where(x => x.TypedeCours == "Général").ToListAsync());
        }
        //[HttpGet]
        //public async Task<IActionResult> AssocierCoursGeneralSession()
        //{
        //    ViewBag.Cours = new SelectList(_contexte.Cours.Where(x => x.TypedeCours == "Général"), "NomCours","NomCours");
        //    ViewBag.ViewBag.NomSession = new SelectList(_contexte.Session, "NomSession", "NomSession");
        //    return View();
        //}

        /// <summary>
        /// Affiche la vue pour ajouter un cours
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AjouterCours()
        {
            //peupler les listes
            ViewBag.NoProgramme = new SelectList(_contexte.Programmes, "NoProgramme", "NoProgramme");
            ViewBag.NomSession = new SelectList(_contexte.Session, "NomSession", "NomSession");
            //Ajuster le selectlist du groupe a la session sélectionné
            //

            ViewBag.NomGroupe = new SelectList(_contexte.Groupe, "NomGroupe", "NomGroupe");
            ViewBag.Cours = new Cours();
            //ViewBag.Groupe = new SelectList(new List<string>());
            return View();
        }
        /// <summary>
        /// Ajoute un cours
        /// </summary>
        /// <param name="cours"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjouterCours([FromBody][Bind("NoCours,NomCours,PonderationCours,DepartementCours,NoProgramme,NomSession,NomGroupe")]Cours cours)
        {
           
            if (ModelState.IsValid)
            {
                 cours.TypedeCours = "Technique";
                //HttpContext.Session.SetString("Cours",JsonConvert.SerializeObject(cours));
                _contexte.Add(cours);
                //Ajouter a la table CoursCompetences
                //Trouver la liste contenant tout les codeCompetence du groupe concerné
                List<GroupeCompetence> listeGroupe = this._contexte.GroupeCompetence.ToList().FindAll(x => x.NomGroupe == cours.NomGroupe);
                foreach (GroupeCompetence g in listeGroupe)
                {
                    //Ajouter a la BD chaque Competence au cours a la table en approprié
                    this._contexte.CoursCompetences.Add(new CoursCompetences()
                    {
                        NoCours = cours.NoCours,
                        CodeCompetence = g.CodeCompetence,
                        Complete = true, //TODO -- GÉRER CA!!!!!!
                        NoProgramme = cours.NoProgramme
                    });
                }
                //Sauvegarder les modifications
                await _contexte.SaveChangesAsync();
                return Json(Url.Action("ListeCours", "Cours"));
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
            if (id == null)
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
        public async Task<IActionResult> SupprimerCoursPost(string NoCours)
        {
            var cours = await _contexte.Cours.FindAsync(NoCours);
            _contexte.Cours.Remove(cours);
            await _contexte.SaveChangesAsync();
            return RedirectToAction("ListeCours");
        }

        public PartialViewResult AfficherGroupe()
        {
            ViewData["Groupes"] = _contexte.Groupe.Where(x => x.NoProgramme.Equals(this.HttpContext.Session.GetString("programme"))).ToList();
            ViewData["GroupeCompetence"] = _contexte.GroupeCompetence.Where(x => x.NoProgramme.Equals(this.HttpContext.Session.GetString("programme"))).ToList();
            return PartialView("_AddPartialListeGroupe");
        }
    }
}