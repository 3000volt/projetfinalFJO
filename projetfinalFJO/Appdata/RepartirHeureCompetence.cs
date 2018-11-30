using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class RepartirHeureCompetence
    {
        public int NbHtotalCompetence { get; set; }
        public string CodeCompetence { get; set; }
        public string NoProgramme { get; set; }
        public bool? ValidationApprouve { get; set; }

        public Competences CodeCompetenceNavigation { get; set; }
        public Programmes NoProgrammeNavigation { get; set; }
    }
}
