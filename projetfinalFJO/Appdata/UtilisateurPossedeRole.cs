using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class UtilisateurPossedeRole
    {
        public string AdresseCourriel { get; set; }
        public int Idrole { get; set; }

        public Utilisateur AdresseCourrielNavigation { get; set; }
        public Roles IdroleNavigation { get; set; }
    }
}
