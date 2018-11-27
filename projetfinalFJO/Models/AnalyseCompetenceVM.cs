using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetfinalFJO.Models
{
    public class AnalyseCompetenceVM
    {
        [Display(Name = "Code de compétence")]
        public string CodeCompetence { get; set; }
        [Display(Name = "Niveau taxonomique")]
        public string NiveauTaxonomique { get; set; }
        [Display(Name = "Validation approuvée")]
        public bool? ValidationApprouve { get; set; }
        [Display(Name = "Adresse courriel")]
        public string AdresseCourriel { get; set; }

    }
}
