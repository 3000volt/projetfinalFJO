using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class Privileges
    {
        public Privileges()
        {
            RolesPrivilege = new HashSet<RolesPrivilege>();
        }

        public string NomPrivilege { get; set; }
        public int Idprivilege { get; set; }

        public ICollection<RolesPrivilege> RolesPrivilege { get; set; }
    }
}
