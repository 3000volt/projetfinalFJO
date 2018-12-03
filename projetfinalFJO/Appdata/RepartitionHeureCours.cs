using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class RepartitionHeureCours
    {
        [Display(Name = "Nombre d'heure par cours")]
        public int Nbreheurcours { get; set; }
        [Display(Name = "Numéro d'analyse")]
        public int IdAnalyseRhc { get; set; }
        [Display(Name = "Courriel")]
        public string AdresseCourriel { get; set; }
        [Display(Name = "Numéro de cours")]
        public string NoCours { get; set; }
        [Display(Name = "Cde de compétence")]
        public string CodeCompetence { get; set; }
        [Display(Name = "Validation")]
        public bool? ValidationApprouve { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }

        public Utilisateur AdresseCourrielNavigation { get; set; }
        public Competences CodeCompetenceNavigation { get; set; }
        public Cours NoCoursNavigation { get; set; }
        public Programmes NoProgrammeNavigation { get; set; }
    }
}
