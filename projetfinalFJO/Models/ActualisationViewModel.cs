using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetfinalFJO.Models
{
    public class ActualisationViewModel
    {
        [Display(Name ="Numéro d'actualisation")]
        public int NumActualisation { get; set; }
        [Display(Name = "Nom d'actualisation")]
        public string NomActualisation { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }
        [Display(Name = "Programme")]
        public string NomProgramme { get; set; }
        [Display(Name = "Approuvée?")]
        public bool? Approuve { get; set; } 

    }
}
