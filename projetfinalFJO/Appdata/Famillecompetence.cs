using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class Famillecompetence
    {
        public Famillecompetence()
        {
            Competences = new HashSet<Competences>();
        }

        public string NomFamille { get; set; }

        public ICollection<Competences> Competences { get; set; }
    }
}
