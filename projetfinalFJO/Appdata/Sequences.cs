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

        [Display(Name ="Nom de la séquence")]
        [Required(ErrorMessage ="Champs requis")]
        public string NomSequence { get; set; }
        public string NoProgramme { get; set; }

        public Programmes NoProgrammeNavigation { get; set; }
        public ICollection<Competences> Competences { get; set; }
    }
}
