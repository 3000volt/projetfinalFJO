using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetfinalFJO.Models
{
    public class AnalyseViewModel
    {
        public string NiveauTaxonomique { get; set; }
        public string Reformulation { get; set; }
        public string Context { get; set; }
        public string SavoirFaireProgramme { get; set; }
        public string SavoirEtreProgramme { get; set; }
        public int IdAnalyseAc { get; set; }
        public string AdresseCourriel { get; set; }
        public string CodeCompetence { get; set; }
        public string Famille { get; set; }
        public string Sequence { get; set; }
    }
}
