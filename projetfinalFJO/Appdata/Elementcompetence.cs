using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class Elementcompetence
    {
        public Elementcompetence()
        {
            AnalyseElementsCompetence = new HashSet<AnalyseElementsCompetence>();
            CompetencesElementCompetence = new HashSet<CompetencesElementCompetence>();
        }

        public string ElementCompétence { get; set; }
        public string CriterePerformance { get; set; }

        public ICollection<AnalyseElementsCompetence> AnalyseElementsCompetence { get; set; }
        public ICollection<CompetencesElementCompetence> CompetencesElementCompetence { get; set; }
    }
}
