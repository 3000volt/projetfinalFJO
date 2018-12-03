using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class Membresdesactualisations
    {
        [Display(Name = "Numéro d'actualisation")]
        public int NumActualisation { get; set; }
        [Display(Name = "Courriel")]
        public string AdresseCourriel { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }

        public Utilisateur AdresseCourrielNavigation { get; set; }
        public Programmes NoProgrammeNavigation { get; set; }
        public ActualisationInformation NumActualisationNavigation { get; set; }
    }
}
