using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class RepartitionHeuresession
    {
        [Display(Name = "Nombre d'heure de compétence par session")]
        public int NbhCompetenceSession { get; set; }
        [Display(Name = "Validation approuvée")]
        public bool? ValidationApprouve { get; set; }
        [Display(Name = "Numéro d'analyse")]
        public int IdAnalyseRhs { get; set; }
        [Display(Name = "Courriel")]
        public string AdresseCourriel { get; set; }
        [Display(Name = "Code de compétence")]
        public string CodeCompetence { get; set; }
        [Display(Name = "Session")]
        public string NomSession { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }

        public Utilisateur AdresseCourrielNavigation { get; set; }
        public Competences CodeCompetenceNavigation { get; set; }
        public Programmes NoProgrammeNavigation { get; set; }
        public Session NomSessionNavigation { get; set; }
    }
}
