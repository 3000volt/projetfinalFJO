using projetfinalFJO.Appdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetfinalFJO.Models
{
    public class ProgrammesDetail
    {
        public Programmes program { get; set; }
        public List<Competences> ListComp { get; set; }
        public Competences comp { get; set; }
    }
}
