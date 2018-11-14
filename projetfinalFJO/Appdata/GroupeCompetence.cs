using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class GroupeCompetence
    {
        public string NomGroupe { get; set; }
        public string CodeCompetence { get; set; }

        public Competences CodeCompetenceNavigation { get; set; }
        public Groupe NomGroupeNavigation { get; set; }
    }
}
