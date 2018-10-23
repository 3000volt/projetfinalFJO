using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class RepartirHeureCompetence
    {
        public int NbHsessionCompetence { get; set; }
        public string CodeCompetence { get; set; }
        public int Idsession { get; set; }
        public bool? ValidationApprouve { get; set; }

        public Competences CodeCompetenceNavigation { get; set; }
        public Session IdsessionNavigation { get; set; }
    }
}
