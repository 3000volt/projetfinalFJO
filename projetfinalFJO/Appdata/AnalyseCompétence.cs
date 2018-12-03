using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class AnalyseCompétence
    {
        [Display(Name = "Niveau taxonomique")]
        public string NiveauTaxonomique { get; set; }
        public string Reformulation { get; set; }
        [Display(Name = "Contexte")]
        public string Context { get; set; }
        [Display(Name = "Savoir faire")]
        public string SavoirFaireProgramme { get; set; }
        [Display(Name = "Savoir être")]
        public string SavoirEtreProgramme { get; set; }
        [Display(Name = "Validation")]
        public bool? ValidationApprouve { get; set; }
        [Display(Name = "Numéro d'analyse")]
        public int IdAnalyseAc { get; set; }
        [Display(Name = "Courriel")]
        public string AdresseCourriel { get; set; }
        [Display(Name = "Code de compétence")]
        public string CodeCompetence { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }

        [Display(Name = "Courriel")]
        public Utilisateur AdresseCourrielNavigation { get; set; }
        [Display(Name = "Code de compétence")]
        public Competences CodeCompetenceNavigation { get; set; }
        [Display(Name = "Numéro de programme")]
        public Programmes NoProgrammeNavigation { get; set; }
    }
}
