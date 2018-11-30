using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetfinalFJO.Models
{
    public class UtilisateurViewModel
    {
        [Display(Name = "Courriel")]
        public string AdresseCourriel { get; set; }
        [Display(Name = "Date d'enregistrement")]
        public DateTime RegisterDate { get; set; }
        public string Nom { get; set; }
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }
        [Display(Name = "Rôle")]
        public string Role { get; set; }
    }
}
