using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class Membresdesactualisations
    {
        public int NumActualisation { get; set; }
        public string AdresseCourriel { get; set; }

        public Utilisateur AdresseCourrielNavigation { get; set; }
        public ActualisationInformation NumActualisationNavigation { get; set; }
    }
}
