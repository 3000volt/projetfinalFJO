using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class Cours
    {
        public Cours()
        {
            CoursCompetences = new HashSet<CoursCompetences>();
            Prealables = new HashSet<Prealables>();
            RepartitionHeureCours = new HashSet<RepartitionHeureCours>();
            RepartitionHeuresession = new HashSet<RepartitionHeuresession>();
        }

        public string NoCours { get; set; }
        public string NomCours { get; set; }
        public string PonderationCours { get; set; }
        public string DepartementCours { get; set; }
        public string TypedeCours { get; set; }
        public string NoProgramme { get; set; }
        public int? Idsession { get; set; }

        public Session IdsessionNavigation { get; set; }
        public Programmes NoProgrammeNavigation { get; set; }
        public ICollection<CoursCompetences> CoursCompetences { get; set; }
        public ICollection<Prealables> Prealables { get; set; }
        public ICollection<RepartitionHeureCours> RepartitionHeureCours { get; set; }
        public ICollection<RepartitionHeuresession> RepartitionHeuresession { get; set; }
    }
}
