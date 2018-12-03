using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
            //Mettre la session a null pour recuperer le bon layout
            HttpContext.Session.SetString("ActualisationActif", "Innactif");
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


        public ActionResult MembresActualisation(int numActu, string programme)
        {

            //Mettre la session a cette compétence

            if (!this.HttpContext.Session.Keys.Contains("programme"))
            {
                this.HttpContext.Session.SetString("programme", programme);
            }
            else if (programme != this.HttpContext.Session.GetString("programme"))
            {
                this.HttpContext.Session.SetString("programme", programme);
            }

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
            string numprogr = null;
            //Trouver le numero en cours grace a la session
            if (this.HttpContext.Session.Keys.Contains("programme"))
            {
                numprogr = this.HttpContext.Session.GetString("programme");
            }
            //Créer l'objet du membre nouvellement ajouté
            Membresdesactualisations nouveauM = new Membresdesactualisations
            {
                NumActualisation = numAct,
                AdresseCourriel = courriel,
                NoProgramme = numprogr,
            };
            //Ajouter le nouveau membre a la bd de l'actualisation

            this.contexteActu.Membresdesactualisations.Add(nouveauM);
            this.contexteActu.SaveChanges();

            //Retour a la vue des membres de l'actualisation en cours
            return RedirectToAction("MembresActualisation", new { numActu = num, programme = numprogr });
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
        public ActionResult CreerActualisation(ActualisationVM actu)
        {
            ActualisationInformation actualisation = new ActualisationInformation()
            {
                NomActualisation = actu.NomActualisation,
                NoProgramme = actu.NoProgramme,
                Approuve = false
            };
            //Ajouter l'actualisation à la BD
            //this.contexteActu.ActualisationInformation.Add(actualisation);

            this.contexteActu.ActualisationInformation.Add(actualisation);
            this.contexteActu.SaveChanges();
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
        public ActionResult SupprimerActualisation2(string password, int NumActualisation) //,string username, string password
        {
            //S'assurer que les mots de passe est valide
            string courriel = this.HttpContext.User.Identity.Name;
            //if(this.contexteActu.Utilisateur.ToList().Find(x=>x.AdresseCourriel == courriel). )
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

        public ActionResult Actualiser(int numActu)
        {
            var actu = this.contexteActu.ActualisationInformation.ToList().Find(x => x.NumActualisation == numActu);
            //Transformer en View Model
            ActualisationViewModel actuVM = new ActualisationViewModel
            {
                NumActualisation = actu.NumActualisation,
                NomActualisation = actu.NomActualisation,
                NoProgramme = actu.NoProgramme,
                NomProgramme = Selection(actu.NoProgramme),
                Approuve = actu.Approuve
            };
            //Transformer la session pour avoir accès au layout d'actualisation
            HttpContext.Session.SetString("ActualisationActif", "Actif");
            //Associer la session d'actualisation en cours
            this.HttpContext.Session.SetString("programme", actuVM.NoProgramme);
            //Retourner a la page d'actualisation
            return View("../Home/Accueil", actuVM);
        }

        [HttpGet]
        public ActionResult Modifier(int numActu)
        {
            ActualisationInformation actu = this.contexteActu.ActualisationInformation.ToList().Find(x => x.NumActualisation == numActu);
            return View(actu);
        }

        [HttpPost]
        public ActionResult Modifier(IFormCollection iAction)
        {
            ActualisationInformation actu = new ActualisationInformation()
            {
                NumActualisation = Convert.ToInt32(iAction["NumActualisation"][0]),
                NomActualisation = iAction["NomActualisation"].ElementAt(0),
                NoProgramme = iAction["NoProgramme"].ElementAt(0),
                Approuve = Convert.ToBoolean(iAction["Approuve"][0])
            };

            //Modifier dans la BD
            this.contexteActu.Update(actu);
            this.contexteActu.SaveChanges();
            return RedirectToAction("Actualisation");
        }

        public ActionResult Details(int numActu)
        {
            ActualisationInformation actu = this.contexteActu.ActualisationInformation.ToList().Find(x => x.NumActualisation == numActu);
            return View(actu);
        }

    }
}