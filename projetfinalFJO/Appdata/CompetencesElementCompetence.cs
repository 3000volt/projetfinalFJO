using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class CompetencesElementCompetence
    {
        public string CodeCompetence { get; set; }
        public int Idelementcomp { get; set; }

        public Competences CodeCompetenceNavigation { get; set; }
        public Elementcompetence IdelementcompNavigation { get; set; }
    }
}
