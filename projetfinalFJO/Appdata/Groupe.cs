using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class Groupe
    {
        public Groupe()
        {
            Cours = new HashSet<Cours>();
            GroupeCompetence = new HashSet<GroupeCompetence>();
        }

        public string NomGroupe { get; set; }

        public ICollection<Cours> Cours { get; set; }
        public ICollection<GroupeCompetence> GroupeCompetence { get; set; }
    }
}
