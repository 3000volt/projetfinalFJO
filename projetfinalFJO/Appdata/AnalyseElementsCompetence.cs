using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class AnalyseElementsCompetence
    {
        public string NiveauTaxonomique { get; set; }
        public string Reformulation { get; set; }
        public string Context { get; set; }
        public string SavoirFaireProgramme { get; set; }
        public string SavoirEtreProgramme { get; set; }
        public bool? ValidationApprouve { get; set; }
        public int IdAnalyseAc { get; set; }
        public string AdresseCourriel { get; set; }
        public int Idelementcomp { get; set; }

        public Elementcompetence IdelementcompNavigation { get; set; }
    }
}
