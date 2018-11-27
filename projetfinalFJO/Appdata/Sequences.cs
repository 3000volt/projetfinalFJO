using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class Sequences
    {
        public Sequences()
        {
            Competences = new HashSet<Competences>();
        }
        [Display(Name = "Nom de la séquence")]
        public string NomSequence { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }

        public Programmes NoProgrammeNavigation { get; set; }
        public ICollection<Competences> Competences { get; set; }
    }
}
