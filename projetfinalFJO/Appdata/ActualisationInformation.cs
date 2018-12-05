using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class ActualisationInformation
    {
        public ActualisationInformation()
        {
            Membresdesactualisations = new HashSet<Membresdesactualisations>();
        }

        public int NumActualisation { get; set; }
        public string NomActualisation { get; set; }
        public string NoProgramme { get; set; }
        public bool? Approuve { get; set; }

        public Programmes NoProgrammeNavigation { get; set; }
        public ICollection<Membresdesactualisations> Membresdesactualisations { get; set; }
    }
}
