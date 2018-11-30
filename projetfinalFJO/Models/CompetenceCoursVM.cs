using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetfinalFJO.Models
{
    public class CompetenceCoursVM
    {
        public string NomCours { get; set; }
        public string CodeCompetence { get; set; }
        public int NbHCoursCompetence { get; set; }
        public bool Complete { get; set; }
    }
}
