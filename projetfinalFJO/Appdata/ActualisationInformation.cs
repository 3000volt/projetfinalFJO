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
        [Display(Name = "Nom d'actualisation")]
        public string NomActualisation { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }
        [Display(Name = "Aprobation")]
        public bool? Approuve { get; set; }
        [Display(Name = "Numéro de programme")]
        public Programmes NoProgrammeNavigation { get; set; }
        public ICollection<Membresdesactualisations> Membresdesactualisations { get; set; }
    }
}
