using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class Roles
    {
        public Roles()
        {
            RolesPrivilege = new HashSet<RolesPrivilege>();
            UtilisateurPossedeRole = new HashSet<UtilisateurPossedeRole>();
        }

        public string TypeRole { get; set; }
        public int Idrole { get; set; }

        public ICollection<RolesPrivilege> RolesPrivilege { get; set; }
        public ICollection<UtilisateurPossedeRole> UtilisateurPossedeRole { get; set; }
    }
}
