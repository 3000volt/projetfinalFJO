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
        [Display(Name = "Élément de compétence")]
        public string ElementCompétence { get; set; }
        [Display(Name = "Critère de performance")]
        public string CriterePerformance { get; set; }
        [Display(Name = "Numéro programme")]
        public string NoProgramme { get; set; }

        public Programmes NoProgrammeNavigation { get; set; }
        public ICollection<AnalyseElementsCompetence> AnalyseElementsCompetence { get; set; }
        public ICollection<CompetencesElementCompetence> CompetencesElementCompetence { get; set; }
    }
}
