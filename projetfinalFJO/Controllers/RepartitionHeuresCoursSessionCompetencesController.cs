using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projetfinalFJO.Appdata;
using projetfinalFJO.Models;

namespace projetfinalFJO.Controllers
{
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Sous_Commite")]
    [Authorize(Roles = "Srdp")]
    public class RepartitionHeureCoursSessionCompetencesController : Controller
    {
        private readonly ActualisationContext _context;

        public RepartitionHeureCoursSessionCompetencesController(ActualisationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //La liste de tout les cours
            List<Cours> listeCours = this._context.Cours.ToList();
            //La liste des repartitions des heures
            List<RepartitionHeuresession> listeRepartirHeureCompetence = this._context.RepartitionHeuresession.ToList();
            //La liste des cours et competences
            List<CoursCompetences> listeCoursCompetence = this._context.CoursCompetences.ToList();
            //La liste des session
            List<Session> listeSession = this._context.Session.ToList();
            //Creation du ViewModel qui nou permettra d'inserer les heures aux compétences par cours
            RepartitionHeuresCoursCompetencesSessionsViewModel repartition = new RepartitionHeuresCoursCompetencesSessionsViewModel
            {
                ListeCours = listeCours,
                ListeRepartirHeureCompetence = listeRepartirHeureCompetence,
                ListeCoursComp = listeCoursCompetence,
                ListeSession = listeSession
            };
            ViewBag.ListeSession = listeSession;

            //Retourne la vue permettant de repartir les heures par session des compétences
            return View(repartition);
        }

        [HttpPost]
        public bool CoursCompetence([FromBody][Bind("NomCours,CodeCompetence, Complete")] CompetenceCoursVM coursCompetencesVM)
        {
            List<CoursCompetences> listecoursCompetence = this._context.CoursCompetences.ToList();
            CoursCompetences coursCompetences = new CoursCompetences
            {
                NoCours = this._context.Cours.ToList().Find(x => x.NomCours == coursCompetencesVM.NomCours).NoCours,
                CodeCompetence = coursCompetencesVM.CodeCompetence,
                Complete = coursCompetencesVM.Complete
            };
            if (listecoursCompetence.Any(x => x.NoCours == coursCompetences.NoCours && x.CodeCompetence == coursCompetences.CodeCompetence))
            //if (listecoursCompetence.Contains(coursCompetences))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AjouterNbHeures([FromBody][Bind("NomCours,CodeCompetence,NbHCoursCompetence")] List<CompetenceCoursVM> ListecoursComp)
        {
            if (ModelState.IsValid)
            {
                //Obtenir tout els cours et competences differents
                List<string> listeNomCours = new List<string>();
                List<string> listeCodeCompetence = new List<string>();

                foreach (CompetenceCoursVM coursComp in ListecoursComp)
                {
                    string nomCours = coursComp.NomCours;
                    if (!listeNomCours.Contains(nomCours))
                    {
                        listeNomCours.Add(nomCours);
                    }

                    string codeCompetence = coursComp.CodeCompetence;
                    if (!listeCodeCompetence.Contains(codeCompetence))
                    {
                        listeCodeCompetence.Add(codeCompetence);
                    }
                }

                foreach (string cours in listeNomCours)
                {
                    int compteurCours = 0;
                    int totalCours = CalculerHeuresCours(this._context.Cours.ToList().Find(x => x.NomCours == cours).PonderationCours);
                    foreach (CompetenceCoursVM coursComp in ListecoursComp)
                    {
                        //Ne faire que le cours sélectionné
                        if (coursComp.NomCours == cours)
                        {
                            Cours coursSelection = _context.Cours.ToList().Find(x => x.NomCours == coursComp.NomCours);
                            string noCours = coursSelection.NoCours;
                            string codeCompetence = coursComp.CodeCompetence;
                            int nbHeure = coursComp.NbHCoursCompetence;
                            //Ajouter au grand compteur
                            compteurCours += nbHeure;
                            //Envoyer un erreur imédiatement si le nombre d'heure totale est dépassé
                            if (compteurCours > totalCours)
                            {
                                return Ok("Trop d'heures pour le cours");
                            }

                            //Effectuer les validations pour respecter la pondération du cours et le nombre d'heure de la compétence dans la session
                            //Trouver le bon cours/compétence
                            CoursCompetences upDateCoursComp = this._context.CoursCompetences.ToList().Find(x => x.NoCours == noCours && x.CodeCompetence == codeCompetence);
                            //Ajouter le nombre d'heure
                            upDateCoursComp.NbHcoursCompetence = nbHeure;

                        }

                    }

                    //S'assurer le le nombre d'heure corresponde exactement au total
                    if (compteurCours != totalCours)
                    {
                        return Ok("Il manque d'heures pour le cours");
                    }
                }

                foreach (string competence in listeCodeCompetence)
                {
                    int compteurTotalCompetence = 0;
                    string coursQuelquonque = ListecoursComp.First().NomCours;
                    string session = this._context.Cours.ToList().Find(x => x.NomCours == coursQuelquonque).NomSession;
                    int totalCompetence = this._context.RepartitionHeuresession.ToList().Find(x => x.CodeCompetence == competence && x.NomSession == session).NbhCompetenceSession;
                    foreach (CompetenceCoursVM coursComp in ListecoursComp)
                    {
                        if (coursComp.CodeCompetence == competence)
                        {
                            List<RepartirHeureCompetence> listRepartirCompetence = new List<RepartirHeureCompetence>();
                            //Liste des compétences contenues dans ce cours
                            //Trouver le noCours
                            string numCours = this._context.Cours.ToList().Find(x => x.NomCours == coursComp.NomCours).NoCours;
                            List<CoursCompetences> listeCoursCompetence = this._context.CoursCompetences.ToList().FindAll(x => x.NoCours == numCours);
                            //Faire une liste de compétence
                            //foreach (CoursCompetences coursCompetence in listeCoursCompetence)
                            //{
                            //    listRepartirCompetence.Add(new RepartirHeureCompetence
                            //    {
                            //        CodeCompetence = coursCompetence.CodeCompetence,
                            //        NomSession = this._context.Cours.ToList().Find(x => x.NoCours == numCours).NomSession,
                            //        ValidationApprouve = false
                            //    });
                            //}
                            //Faire un foreach de chaque compétence pour s'assurer que le nombre d'heures de ceux-ci soit respecté
                            int nbHeures = coursComp.NbHCoursCompetence;

                            compteurTotalCompetence += nbHeures;
                            if (compteurTotalCompetence > totalCompetence)
                            {
                                return Ok("Trop d'heure pour la compétence");
                            }

                        }

                    }


                    if (compteurTotalCompetence != totalCompetence)
                    {
                        return Ok("Il manque d'heures pour la compétence");
                    }


                }

                //Sauvegarder
                await _context.SaveChangesAsync();
                return Ok("Nombre d'heures ajouté avec succès");
            }
            return BadRequest("esti de merde");

        }


        [HttpPost]
        public int GererPonderation([FromBody][Bind("NomCours")] Cours cours)
        {
            //Prendre la ponderation
            string ponderation = this._context.Cours.ToList().Find(x => x.NomCours == cours.NomCours).PonderationCours;
            int total = CalculerHeuresCours(ponderation);
            return total;
        }

        private int CalculerHeuresCours(string pond)
        {
            //Retirer le nombre d'heure theorique
            int theorique = int.Parse(pond.Split('-')[0]);
            //Retirer le nombre d'heure pratique
            int laboratoire = int.Parse(pond.Split('-')[1]);
            int total = theorique + laboratoire;
            return total;
        }
    }


}