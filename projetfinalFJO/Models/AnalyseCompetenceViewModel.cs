using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetfinalFJO.Models
{
    public class AnalyseCompetenceViewModel
    {
        public string NiveauTaxonomique { get; set; }
        [Display(Name = "Reformulation de la compétence")]
        public string Reformulation { get; set; }
        public string Context { get; set; }
        public string SavoirFaireProgramme { get; set; }
        public string SavoirEtreProgramme { get; set; }
        public int IdAnalyseAc { get; set; }
        public string AdresseCourriel { get; set; }
        public string CodeCompetence { get; set; }
        public string Famille { get; set; }
        public string Sequence { get; set; }
        public string Titre { get; set; }

    }
}
