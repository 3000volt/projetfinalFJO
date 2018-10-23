using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class RepartitionHeuresession
    {
        public int NbhCompetenceCours { get; set; }
        public bool? ValidationApprouve { get; set; }
        public int IdAnalyseRhs { get; set; }
        public string AdresseCourriel { get; set; }
        public string CodeCompetence { get; set; }
        public string NoCours { get; set; }
        public int Idsession { get; set; }

        public Utilisateur AdresseCourrielNavigation { get; set; }
        public Competences CodeCompetenceNavigation { get; set; }
        public Session IdsessionNavigation { get; set; }
        public Cours NoCoursNavigation { get; set; }
    }
}
