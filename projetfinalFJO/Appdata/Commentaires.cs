using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class Commentaires
    {
        public int NumCom { get; set; }
        public string TexteCom { get; set; }
        public string Categorie { get; set; }
        public string AdresseCourriel { get; set; }

        public Utilisateur AdresseCourrielNavigation { get; set; }
    }
}
