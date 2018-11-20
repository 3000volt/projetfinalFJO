using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class Membresdesactualisations
    {
        public int NumActualisation { get; set; }
        public string AdresseCourriel { get; set; }
        public string NoProgramme { get; set; }

        public Utilisateur AdresseCourrielNavigation { get; set; }
        public Programmes NoProgrammeNavigation { get; set; }
        public ActualisationInformation NumActualisationNavigation { get; set; }
    }
}
