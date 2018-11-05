using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class HomeController : Controller
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
        public HomeController(IConfiguration iConfig, UserManager<LoginUser> userManager, LoginDbContext log)
        {
            this.config = iConfig;
            this.contexteActu = new ActualisationContext(config.GetConnectionString("DefaultConnection"));
            this._userManager = userManager;
            this.contextLogin = log;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult GererUtilisateur()
        {
            //instancier les listes
            List<UtilisateurViewModel> listeUtilisateurs = new List<UtilisateurViewModel>();
            string userEmail;
            string userID;
            //Liste de tout les utilisateurs avec leur roles
            var liste = this.contextLogin.UserRoles.ToList();

            foreach (LoginUser util in this.contextLogin.Users.ToList())
            {
                //Récupérer le Email du user
                userEmail = util.UserName;
                //Trouver le ID du user
                userID = util.Id;
                //Trouver le nom et prenom de l'utilistaeur
                string nomUt = this.contexteActu.Utilisateur.ToList().Find(x => x.AdresseCourriel == userEmail).Nom;
                string prenomUt = this.contexteActu.Utilisateur.ToList().Find(x => x.AdresseCourriel == userEmail).Prenom;
                //Trouver le ID du role du user
                string roleID = liste.Find(x => x.UserId == userID).RoleId;
                //Trouver le nom du role
                string nomRole = this.contextLogin.Roles.ToList().Find(x => x.Id == roleID).Name;
                DateTime dateEnr = this.contexteActu.Utilisateur.ToList().Find(x => x.AdresseCourriel == userEmail).RegisterDate;
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
