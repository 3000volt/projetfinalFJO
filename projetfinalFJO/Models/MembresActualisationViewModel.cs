using projetfinalFJO.Appdata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetfinalFJO.Models
{
    public class MembresActualisationViewModel
    {
        public string Nom { get; set; }
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }
        public string Courriel { get; set; }
        [Display(Name = "Rôle")]
        public string Role { get; set; }
    }
}
