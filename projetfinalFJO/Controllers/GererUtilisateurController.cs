using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using projetfinalFJO.Appdata;
using projetfinalFJO.Models;
using projetfinalFJO.Models.Authentification;

namespace projetfinalFJO.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GererUtilisateurController : Controller
    {
        //Propriétés
        private IConfiguration config;
        private ActualisationContext contexteActu;
        private readonly LoginDbContext contextLogin;
        private readonly UserManager<LoginUser> _userManager;

        //Constructeur du controleur
        public GererUtilisateurController(IConfiguration iConfig, UserManager<LoginUser> userManager, LoginDbContext log)
        {
            this.config = iConfig;
            this.contexteActu = new ActualisationContext(config.GetConnectionString("DefaultConnection"));
            this._userManager = userManager;
            this.contextLogin = log;
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
            ViewBag.Role = new SelectList(this.contextLogin.Roles.ToList(), "Id", "Name");
            return View(listeUtilisateurs);
        }

        [HttpGet]
        public ActionResult SupprimerUtilisateur(string courriel)
        {
            //Trouver l'utilistaur
            Utilisateur util = this.contexteActu.Utilisateur.ToList().Find(x => x.AdresseCourriel == courriel);
            return View(util);
        }

        [HttpPost]
        public ActionResult SupprimerUtilisateur2(string courriel)
        {
            //Utilisateur d'actualisation
            this.contexteActu.SupprimerUtilisateur(courriel);
            //ContextLogin
            this.contextLogin.SupprimerUtilisateur(courriel);
            return RedirectToAction("GererUtilisateur");
        }

        public ActionResult ChangerRole(string nomRole, string courriel)
        {
            //Trouver les Id respectifs
            string userId = this.contextLogin.Users.ToList().Find(x => x.UserName == courriel).Id;
            string roleId = this.contextLogin.Roles.ToList().Find(x => x.Name == nomRole).Id;
            //Mettre a jour la BD
            this.contextLogin.ModifierRole(userId, roleId);
            //Retourner la vue de la liste des utilisateurs
            return RedirectToAction("GererUtilisateur");

        }
    }
}