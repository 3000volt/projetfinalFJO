using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using projetfinalFJO.Appdata;
using projetfinalFJO.Models;
using projetfinalFJO.Models.Authentification;

namespace projetfinalFJO.Controllers
{
    public class ActualisationController : Controller
    {

        //Propriétés
        private IConfiguration config;
        private ActualisationContext contexteActu;
        private readonly LoginDbContext contextLogin;
        private List<ActualisationInformation> listeActualisation;
        private List<Programmes> listeProgrammes;
        private readonly UserManager<LoginUser> _userManager;
        private List<Membresdesactualisations> listeMemrbesActualisation;

        //Constructeur du controleur
        public ActualisationController(IConfiguration iConfig, UserManager<LoginUser> userManager, LoginDbContext log)
        {
            this.config = iConfig;
            this.contexteActu = new ActualisationContext(config.GetConnectionString("DefaultConnection"));
            this._userManager = userManager;
            this.contextLogin = log;
        }

        public IActionResult Actualisation()
        {
            listeActualisation = this.contexteActu.ActualisationInformation.ToList();
            ActualisationViewModel actu = new ActualisationViewModel();

            List<ActualisationViewModel> actuListe = listeActualisation.Select(x => new ActualisationViewModel
            {
                NumActualisation = x.NumActualisation,
                NomActualisation = x.NomActualisation,
                NoProgramme = x.NoProgramme,
                NomProgramme = Selection(x.NoProgramme),
                Approuve = x.Approuve
            }).ToList();
            return View(actuListe);
        }

        //Méthode privé pour retirer le nom du programme
        private string Selection(string num)
        {
            listeProgrammes = this.contexteActu.Programmes.ToList();
            Programmes prog = listeProgrammes.Find(x => x.NoProgramme == num);
            string numero = prog.NomProgramme;
            return numero;
        }


        public ActionResult MembresActualisation(int numActu)
        {
            //Prendre les éléments du contexte ayant le numéro de l'actualisation en paramètre
            this.listeMemrbesActualisation = this.contexteActu.Membresdesactualisations.ToList().FindAll(x => x.NumActualisation == numActu);
            //Trouver les utilisateurs pour transférer leur viewModel
            List<MembresActualisationViewModel> listeUtilisateurs = new List<MembresActualisationViewModel>();
            string courriel;
            Utilisateur util;
            foreach (Membresdesactualisations me in listeMemrbesActualisation)
            {
                courriel = me.AdresseCourriel;
                //Trouver l'utilisatuer dans la liste des utilisateurs
                util = this.contexteActu.Utilisateur.ToList().Find(x => x.AdresseCourriel == courriel);
                //Id Utilisateur
                string utilisateurID = this.contextLogin.Users.ToList().Find(x => x.UserName == me.AdresseCourriel).Id;
                //Trouver son role
                //Id
                string roleId = this.contextLogin.UserRoles.ToList().Find(x => x.UserId == utilisateurID).RoleId;
                //nom
                string nomRole = this.contextLogin.Roles.ToList().Find(x => x.Id == roleId).Name;
                //Ajouter a la liste
                listeUtilisateurs.Add(new MembresActualisationViewModel
                {
                    Nom = util.Nom,
                    Prenom = util.Prenom,
                    Courriel = util.AdresseCourriel,
                    Role = nomRole
                });
            }
            ViewBag.NumActualisation = numActu;
            return View(listeUtilisateurs);
        }

        [HttpGet]
        public ActionResult AjouterMembre(int numAct)
        {
            //Retourne la liste de totu les membres
            List<Utilisateur> listeUtilisateur = this.contexteActu.Utilisateur.ToList();

            //Transferer en ViewModel
            List<UtilisateurViewModel> listeUtilisateurVM = new List<UtilisateurViewModel>();
            foreach (Utilisateur util in listeUtilisateur)
            {
                //Trouver son role
                string userId = this.contextLogin.Users.ToList().Find(x => x.UserName == util.AdresseCourriel).Id;
                //Id
                string roleId = this.contextLogin.UserRoles.ToList().Find(x => x.UserId == userId).RoleId;
                //nom
                string nomRole = this.contextLogin.Roles.ToList().Find(x => x.Id == roleId).Name;
                listeUtilisateurVM.Add(new UtilisateurViewModel
                {
                    AdresseCourriel = util.AdresseCourriel,
                    RegisterDate = util.RegisterDate,
                    Nom = util.Nom,
                    Prenom = util.Prenom,
                    Role = nomRole
                });
            }
            //Retirer les utilisateurs déjà présent dans l'actualisation
            List<Membresdesactualisations> listeMembrePresent = this.contexteActu.Membresdesactualisations.ToList().FindAll(x => x.NumActualisation == numAct);
            foreach (Membresdesactualisations membre in listeMembrePresent)
            {
                string courriel = membre.AdresseCourriel;
                //Trouver l'utilisateur
                UtilisateurViewModel ut = listeUtilisateurVM.Find(x => x.AdresseCourriel == courriel);
                //Retirer de la liste des utilisatuers ajoutables
                listeUtilisateurVM.Remove(ut);
            }
            //Conserver le numéro d'actualisation en cours
            ViewBag.NumActualisation = numAct;
            return View(listeUtilisateurVM);
        }

        public ActionResult AjouterMembre2(string courriel, int numAct, int num)
        {
            //Créer l'objet du membre nouvellement ajouté
            Membresdesactualisations nouveauM = new Membresdesactualisations
            {
                NumActualisation = numAct,
                AdresseCourriel = courriel
            };
            //Ajouter le nouveau membre a la bd de l'actualisation
            this.contexteActu.InsererMembresdesactualisations(nouveauM);
            //Créer la liste des membres de l'actualisation en cours
            //List<Membresdesactualisations> listeMembres = this.contexteActu.Membresdesactualisations.ToList().FindAll(x => x.NumActualisation == numAct);
            //List<MembresActualisationViewModel> listeUtilisateurs = new List<MembresActualisationViewModel>();
            //Utilisateur util;
            //foreach (Membresdesactualisations me in listeMembres)
            //{
            //    courriel = me.AdresseCourriel;
            //    //Trouver l'utilisatuer dans la liste des utilisateurs
            //    util = this.contexteActu.Utilisateur.ToList().Find(x => x.AdresseCourriel == courriel);
            //    //Id Utilisateur
            //    string utilisateurID = this.contextLogin.Users.ToList().Find(x => x.UserName == me.AdresseCourriel).Id;
            //    //Trouver son role
            //    //Id
            //    string roleId = this.contextLogin.UserRoles.ToList().Find(x => x.UserId == utilisateurID).RoleId;
            //    //nom
            //    string nomRole = this.contextLogin.Roles.ToList().Find(x => x.Id == roleId).Name;
            //    //Ajouter a la liste
            //    listeUtilisateurs.Add(new MembresActualisationViewModel
            //    {
            //        Nom = util.Nom,
            //        Prenom = util.Prenom,
            //        Role = nomRole
            //    });
            //}
            //Retour a la vue des membres de l'actualisation en cours
            return RedirectToAction("MembresActualisation", new { numActu = num });
        }

        [HttpGet]
        public ActionResult CreerActualisation()
        {
            //Méthode qui retourne la vue de création
            ViewBag.Prog = new SelectList(this.contexteActu.Programmes.ToList(), "NoProgramme", "NomProgramme");
            return View();
            //https://stackoverflow.com/questions/16594958/how-to-use-a-viewbag-to-create-a-dropdownlist
        }

        [HttpPost]
        public ActionResult CreerActualisation(ActualisationInformation actu)
        {
            //Ajouter l'actualisation à la BD
            this.contexteActu.InsererActualisation(actu);
            //Retourner la liste
            return RedirectToAction("Actualisation");
        }

        [HttpGet]
        public ActionResult SupprimerActualisation(int numActu)
        {
            //Trouver l'actualisation
            List<ActualisationInformation> listeActu = this.contexteActu.ActualisationInformation.ToList();
            List<ActualisationViewModel> listeActuModel = new List<ActualisationViewModel>();
            foreach (ActualisationInformation actu in listeActu)
            {
                //Convertrir en View model
                listeActuModel.Add(new ActualisationViewModel
                {
                    NumActualisation = actu.NumActualisation,
                    NomActualisation = actu.NomActualisation,
                    NoProgramme = actu.NoProgramme,

                    NomProgramme = this.contexteActu.Programmes.ToList().Find(x => x.NoProgramme == actu.NoProgramme).NomProgramme,

                    Approuve = actu.Approuve
                });
            }
            ActualisationViewModel actualisationSupprimer = listeActuModel.Find(x => x.NumActualisation == numActu);
            return View(actualisationSupprimer);
        }

        [HttpPost]
        public ActionResult SupprimerActualisation2(int NumActualisation) //,string username, string password
        {
            //Retirer de la BD
            this.contexteActu.SupprimerActualisation(NumActualisation);
            //Retourner la listed es actualsiations
            return RedirectToAction("Actualisation");
        }

        public ActionResult RetirerMembre(int numActualisation, string courriel)
        {
            //Retirer de la BD
            this.contexteActu.RetirerMembreActu(numActualisation, courriel);
            //Retourner la vue des utilisateurs de l,actualisation
            return RedirectToAction("MembresActualisation", new { numAct = numActualisation });
        }


    }
}