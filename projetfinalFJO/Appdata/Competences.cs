using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class Competences
    {
        public Competences()
        {
            AnalyseCompétence = new HashSet<AnalyseCompétence>();
            CompetencesElementCompetence = new HashSet<CompetencesElementCompetence>();
            CoursCompetences = new HashSet<CoursCompetences>();
            RepartirHeureCompetence = new HashSet<RepartirHeureCompetence>();
            RepartitionHeureCours = new HashSet<RepartitionHeureCours>();
            RepartitionHeuresession = new HashSet<RepartitionHeuresession>();
        }

        public string CodeCompetence { get; set; }
        public bool? ObligatoireCégep { get; set; }
        public string Description { get; set; }
        public string ContextRealisation { get; set; }
        public int? Idfamille { get; set; }

        public Famillecompetence IdfamilleNavigation { get; set; }
        public ICollection<AnalyseCompétence> AnalyseCompétence { get; set; }
        public ICollection<CompetencesElementCompetence> CompetencesElementCompetence { get; set; }
        public ICollection<CoursCompetences> CoursCompetences { get; set; }
        public ICollection<RepartirHeureCompetence> RepartirHeureCompetence { get; set; }
        public ICollection<RepartitionHeureCours> RepartitionHeureCours { get; set; }
        public ICollection<RepartitionHeuresession> RepartitionHeuresession { get; set; }
    }
}
