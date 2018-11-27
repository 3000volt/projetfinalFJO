using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class GroupeCompetence
    {
        [Display(Name = "Nom du groupe")]
        public string NomGroupe { get; set; }
        [Display(Name = "Code de compétence")]
        public string CodeCompetence { get; set; }
        [Display(Name = "Session")]
        public string NomSession { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }

        public Competences CodeCompetenceNavigation { get; set; }
        public Programmes NoProgrammeNavigation { get; set; }
        public Groupe NomGroupeNavigation { get; set; }
        public Session NomSessionNavigation { get; set; }
    }
}
