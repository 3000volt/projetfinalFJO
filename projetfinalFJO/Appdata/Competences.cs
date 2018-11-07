using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class Competences
    {
        public string CodeCompetence { get; set; }
        public bool? ObligatoireCégep { get; set; }
        public string Description { get; set; }
        public string ContextRealisation { get; set; }
        public int? Idfamille { get; set; }
        public string NoProgramme { get; set; }
    }
}
