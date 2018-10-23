using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class Elementcompetence
    {
        public Elementcompetence()
        {
            CompetencesElementCompetence = new HashSet<CompetencesElementCompetence>();
        }

        public string ElementCompétence { get; set; }
        public string CriterePerformance { get; set; }
        public int Idelementcomp { get; set; }

        public ICollection<CompetencesElementCompetence> CompetencesElementCompetence { get; set; }
    }
}
