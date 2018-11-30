using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class ActualisationInformation
    {
        public ActualisationInformation()
        {
            Membresdesactualisations = new HashSet<Membresdesactualisations>();
        }
        [Display(Name = "Numéro d'actualisation")]
        public int NumActualisation { get; set; }
        [Display(Name = "Nom de l'actualisation")]
        public string NomActualisation { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }
        [Display(Name = "Approuvée?")]
        public bool? Approuve { get; set; }

        public Programmes NoProgrammeNavigation { get; set; }
        public ICollection<Membresdesactualisations> Membresdesactualisations { get; set; }
    }
}
