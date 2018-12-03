using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class Commentaires
    {
        [Display(Name = "Numéro de commentaire")]
        public int NumCom { get; set; }
        [Display(Name = "Commentaire")]
        public string TexteCom { get; set; }
        [Display(Name = "Catégorie")]
        public string Categorie { get; set; }
        [Display(Name = "Courriel")]
        public string AdresseCourriel { get; set; }
        [Display(Name = "numéro de programme")]
        public string NoProgramme { get; set; }

        public Utilisateur AdresseCourrielNavigation { get; set; }
        public Programmes NoProgrammeNavigation { get; set; }
    }
}
