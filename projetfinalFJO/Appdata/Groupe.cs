using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class Groupe
    {
        public Groupe()
        {
            Cours = new HashSet<Cours>();
            GroupeCompetence = new HashSet<GroupeCompetence>();
        }
        [Display(Name = "Groupe")]
        public string NomGroupe { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }

        public Programmes NoProgrammeNavigation { get; set; }
        public ICollection<Cours> Cours { get; set; }
        public ICollection<GroupeCompetence> GroupeCompetence { get; set; }
    }
}
