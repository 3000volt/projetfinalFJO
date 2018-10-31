using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetfinalFJO.Models
{
    public class ActualisationViewModel
    {
        public int NumActualisation { get; set; }
        public string NomActualisation { get; set; }
        public string NoProgramme { get; set; }

        public string NomProgramme { get; set; }

        public bool Approuve { get; set; } 

    }
}
