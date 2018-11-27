using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class AnalyseElementsCompetence
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
        [Display(Name = "Validation approuvée?")]
        public bool? ValidationApprouve { get; set; }
        [Display(Name = "Numéro d'analyse")]
        public int IdAnalyseAc { get; set; }
        [Display(Name = "Courriel")]
        public string AdresseCourriel { get; set; }
        [Display(Name = "Élément de compétence")]
        public string ElementCompétence { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }

        public Utilisateur AdresseCourrielNavigation { get; set; }
        public Elementcompetence ElementCompétenceNavigation { get; set; }
        public Programmes NoProgrammeNavigation { get; set; }
    }
}
