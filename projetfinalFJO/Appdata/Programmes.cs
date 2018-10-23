using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class Programmes
    {
        public Programmes()
        {
            ActualisationInformation = new HashSet<ActualisationInformation>();
            Cours = new HashSet<Cours>();
        }

        public string NoProgramme { get; set; }
        public string NomProgramme { get; set; }
        public int? NbHeure { get; set; }
        public int? NbUnite { get; set; }
        public int? NbCompetencesObligatoires { get; set; }
        public int? NbCompetencesOptionnelles { get; set; }
        public string CondtionsAdmission { get; set; }

        public ICollection<ActualisationInformation> ActualisationInformation { get; set; }
        public ICollection<Cours> Cours { get; set; }
    }
}
