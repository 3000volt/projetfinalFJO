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
        [Display(Name ="Numéro d'actualisation")]
        public int NumActualisation { get; set; }
        [Display(Name = "Titre")]
        public string NomActualisation { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }
        [Display(Name = "Approbation")]
        public bool? Approuve { get; set; }

        [Display(Name = "Numéro de programme")]
        public Programmes NoProgrammeNavigation { get; set; }
        [Display(Name = "Membres de l'actualisation")]
        public ICollection<Membresdesactualisations> Membresdesactualisations { get; set; }
    }
}
