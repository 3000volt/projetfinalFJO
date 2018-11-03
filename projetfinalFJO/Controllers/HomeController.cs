using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using projetfinalFJO.Appdata;
using projetfinalFJO.Models;
using projetfinalFJO.Models.Authentification;

namespace projetfinalFJO.Controllers
{
    public class HomeController : Controller
    {
        //Propriétés
        private IConfiguration config;
        private ActualisationContext contexteActu;
        private readonly LoginDbContext contextLogin;
        private List<ActualisationInformation> listeActualisation;
        private List<Programmes> listeProgrammes;
        private List<Utilisateur> listeUtilisateurActu;
        private List<LoginUser> listeUtilisateurLogin;
        private List<LoginRole> listeRoleLogin;
        private readonly UserManager<LoginUser> _userManager;

        //Constructeur du controleur
        public HomeController(IConfiguration iConfig, UserManager<LoginUser> userManager, LoginDbContext log)
        {
            this.config = iConfig;
            this.contexteActu = new ActualisationContext();
            this._userManager = userManager;
            this.contextLogin = log;
        }

        public IActionResult Index()
        {
            return View();
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



        public IActionResult GererUtilisateur()
        {
            //instancier les listes
            //Liste des utilisateurs (Actualisation)
            this.listeUtilisateurActu = this.contexteActu.Utilisateur.ToList();
            //liste des utilisateurs (Login)
            this.listeUtilisateurLogin = this.contextLogin.Users.ToList();
            //Liste des roles (Login)
            this.listeRoleLogin = this.contextLogin.Roles.ToList();
            List<UtilisateurViewModel> listeUtilisateurs = new List<UtilisateurViewModel>();
            string userEmail;
            string userID;
            var liste = this.contextLogin.UserRoles.ToList();

            //for(int i = 0; i < this.listeUtilisateurLogin.Count(); i++)
            //{
            // userId = this.listeUtilisateurLogin.Select(x=>x. ==)
            //}
            foreach (LoginUser util in this.listeUtilisateurLogin)
            {
                //Récupérer le Email du user
                userEmail = util.UserName;
                //Trouver le ID du user
                userID = util.Id;
                //Trouver le nom et prenom de l'utilistaeur
                string nomUt = listeUtilisateurActu.Find(x => x.AdresseCourriel == userEmail).Nom;
                string prenomUt = listeUtilisateurActu.Find(x => x.AdresseCourriel == userEmail).Prenom;
                //Trouver le ID du role du user
                string roleID = liste.Find(x => x.UserId == userID).RoleId;
                //Trouver le nom du role
                string nomRole = listeRoleLogin.Find(x => x.Id == roleID).Name;
                DateTime dateEnr = listeUtilisateurActu.Find(x => x.AdresseCourriel == userEmail).RegisterDate;
                //Ajouter a la liste du ViewModel
                listeUtilisateurs.Add(new UtilisateurViewModel
                {
                    AdresseCourriel = userEmail,
                    RegisterDate = dateEnr,
                    Nom = nomUt,
                    Prenom = prenomUt,
                    Role = nomRole
                   
                    
                });
            }
            return View(listeUtilisateurs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
