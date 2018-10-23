using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class AnalyseCompétence
    {
        public bool Obligatoire { get; set; }
        public string NiveauTaxonomique { get; set; }
        public string Reformulation { get; set; }
        public string Context { get; set; }
        public string SavoirFaireProgramme { get; set; }
        public string SavoirEtreProgramme { get; set; }
        public bool? ValidationApprouve { get; set; }
        public int IdAnalyseAc { get; set; }
        public string AdresseCourriel { get; set; }
        public string CodeCompetence { get; set; }

        public Utilisateur AdresseCourrielNavigation { get; set; }
        public Competences CodeCompetenceNavigation { get; set; }
    }
}
