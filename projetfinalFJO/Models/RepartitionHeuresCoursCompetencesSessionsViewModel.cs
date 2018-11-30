using projetfinalFJO.Appdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetfinalFJO.Models
{
    public class RepartitionHeuresCoursCompetencesSessionsViewModel
    {
        //public string NomCours { get; set; }
        //public string PonderationCours { get; set; }
        //public string NomSession { get; set; }

        //public int NbHsessionCompetence { get; set; }
        //public string CodeCompetence { get; set; }

        public List<RepartitionHeuresession> ListeRepartirHeureCompetence { get; set; }

        public List<Cours> ListeCours { get; set; }

        public List<Session> ListeSession { get; set; }

        public List<CoursCompetences> ListeCoursComp { get; set; }

        public int NbHCoursCompetence { get; set; }

    }
}
