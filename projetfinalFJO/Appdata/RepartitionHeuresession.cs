using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class RepartitionHeuresession
    {
        public int NbhCompetenceSession { get; set; }
        public bool? ValidationApprouve { get; set; }
        public int IdAnalyseRhs { get; set; }
        public string AdresseCourriel { get; set; }
        public string CodeCompetence { get; set; }
        public string NomSession { get; set; }
        public string NoProgramme { get; set; }

        public Utilisateur AdresseCourrielNavigation { get; set; }
        public Competences CodeCompetenceNavigation { get; set; }
        public Programmes NoProgrammeNavigation { get; set; }
        public Session NomSessionNavigation { get; set; }
    }
}
