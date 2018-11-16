using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class Sequences
    {
        public Sequences()
        {
            Competences = new HashSet<Competences>();
        }

        public string NomSequence { get; set; }

        public ICollection<Competences> Competences { get; set; }
    }
}
