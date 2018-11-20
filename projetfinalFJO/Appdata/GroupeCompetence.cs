using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class GroupeCompetence
    {
        public string NomGroupe { get; set; }
        public string CodeCompetence { get; set; }
        public string NomSession { get; set; }
        public string NoProgramme { get; set; }

        public Competences CodeCompetenceNavigation { get; set; }
        public Programmes NoProgrammeNavigation { get; set; }
        public Groupe NomGroupeNavigation { get; set; }
        public Session NomSessionNavigation { get; set; }
    }
}
