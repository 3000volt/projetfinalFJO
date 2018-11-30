using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class RepartirHeureCompetence
    {
        [Display(Name = "Nombre d'heure total de la compétence")]
        public int NbHtotalCompetence { get; set; }
        [Display(Name = "Code de la compétence")]
        public string CodeCompetence { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }
        [Display(Name = "Validation approuvée")]
        public bool? ValidationApprouve { get; set; }

        public Competences CodeCompetenceNavigation { get; set; }
        public Programmes NoProgrammeNavigation { get; set; }
    }
}
