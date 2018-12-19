using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class RepartirHeureCompetence
    {
        [Display(Name ="Nombre d'heures total de la compétence")]
        public int NbHtotalCompetence { get; set; }
        [Display(Name = "Code de la compétence")]
        public string CodeCompetence { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }
        [Display(Name = "Validé")]
        public bool? ValidationApprouve { get; set; }

        [Display(Name = "Code de la compétence")]
        public Competences CodeCompetenceNavigation { get; set; }
        [Display(Name = "Numéro de programme")]
        public Programmes NoProgrammeNavigation { get; set; }
    }
}
