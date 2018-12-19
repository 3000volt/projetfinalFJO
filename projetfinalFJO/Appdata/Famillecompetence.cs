using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class Famillecompetence
    {
        public Famillecompetence()
        {
            Competences = new HashSet<Competences>();
        }

        [Display(Name = "Nom de la famille")]
        [Required(ErrorMessage = "Champs requis")]
        public string NomFamille { get; set; }
        public string NoProgramme { get; set; }

        public Programmes NoProgrammeNavigation { get; set; }
        public ICollection<Competences> Competences { get; set; }
    }
}
