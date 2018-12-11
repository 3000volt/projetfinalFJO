using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class Elementcompetence
    {
        public Elementcompetence()
        {
            AnalyseElementsCompetence = new HashSet<AnalyseElementsCompetence>();
            CompetencesElementCompetence = new HashSet<CompetencesElementCompetence>();
        }

        [Display(Name = "Éléments de compétence : ")]
        public string ElementCompétence { get; set; }
        [Display(Name = "Critères de performances : ")]
        public string CriterePerformance { get; set; }
        public string NoProgramme { get; set; }

        public Programmes NoProgrammeNavigation { get; set; }
        public ICollection<AnalyseElementsCompetence> AnalyseElementsCompetence { get; set; }
        public ICollection<CompetencesElementCompetence> CompetencesElementCompetence { get; set; }
    }
}
