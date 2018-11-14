using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class Session
    {
        public Session()
        {
            Cours = new HashSet<Cours>();
            RepartirHeureCompetence = new HashSet<RepartirHeureCompetence>();
            RepartitionHeureCours = new HashSet<RepartitionHeureCours>();
            RepartitionHeuresession = new HashSet<RepartitionHeuresession>();
        }

        public string NomSession { get; set; }

        public ICollection<Cours> Cours { get; set; }
        public ICollection<RepartirHeureCompetence> RepartirHeureCompetence { get; set; }
        public ICollection<RepartitionHeureCours> RepartitionHeureCours { get; set; }
        public ICollection<RepartitionHeuresession> RepartitionHeuresession { get; set; }
    }
}
