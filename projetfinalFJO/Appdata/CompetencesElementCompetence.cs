using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class CompetencesElementCompetence
    {
        public string CodeCompetence { get; set; }
        public string ElementCompétence { get; set; }
        public string NoProgramme { get; set; }

        public Competences CodeCompetenceNavigation { get; set; }
        public Elementcompetence ElementCompétenceNavigation { get; set; }
        public Programmes NoProgrammeNavigation { get; set; }
    }
}
