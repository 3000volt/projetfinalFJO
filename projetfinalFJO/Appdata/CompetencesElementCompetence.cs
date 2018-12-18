using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class CompetencesElementCompetence
    {
        [Display(Name = "Code de compétence")]
        public string CodeCompetence { get; set; }
        [Display(Name = "Élément de compétence")]
        public string ElementCompétence { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }

        [Display(Name = "Code de compétence")]
        public Competences CodeCompetenceNavigation { get; set; }
        [Display(Name = "Élément de compétence")]
        public Elementcompetence ElementCompétenceNavigation { get; set; }
        [Display(Name = "Numéro de programme")]
        public Programmes NoProgrammeNavigation { get; set; }
    }
}
