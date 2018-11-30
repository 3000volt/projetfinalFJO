using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetfinalFJO.Models
{
    public class AnalyseViewModel
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
        [Display(Name = "Numéro d'analyse d'actualisation")]
        public int IdAnalyseAc { get; set; }
        [Display(Name = "Courriel")]
        public string AdresseCourriel { get; set; }
        [Display(Name = "Code de la compétence")]
        public string CodeCompetence { get; set; }
        public string Famille { get; set; }
        [Display(Name = "Séquence")]
        public string Sequence { get; set; }
    }
}
