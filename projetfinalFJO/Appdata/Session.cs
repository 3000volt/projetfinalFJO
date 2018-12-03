using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class Session
    {
        public Session()
        {
            Cours = new HashSet<Cours>();
            GroupeCompetence = new HashSet<GroupeCompetence>();
            RepartitionHeuresession = new HashSet<RepartitionHeuresession>();
        }
        [Display(Name = "Session")]
        public string NomSession { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }

        public Programmes NoProgrammeNavigation { get; set; }
        public ICollection<Cours> Cours { get; set; }
        public ICollection<GroupeCompetence> GroupeCompetence { get; set; }
        public ICollection<RepartitionHeuresession> RepartitionHeuresession { get; set; }
    }
}
