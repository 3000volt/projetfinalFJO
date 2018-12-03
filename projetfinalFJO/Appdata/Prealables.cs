using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class Prealables
    {
        [Display(Name = "Numéro du cours préalable")]
        public string NoCoursPrealable { get; set; }
        [Display(Name = "Numéro de cours")]
        public string NoCours { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }

        public Cours NoCoursNavigation { get; set; }
        public Programmes NoProgrammeNavigation { get; set; }
    }
}
