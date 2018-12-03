using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class CoursCompetences
    {
        [Display(Name = "")]
        public int? NbHcoursCompetence { get; set; }
        [Display(Name = "Numéro du cours")]
        public string NoCours { get; set; }
        [Display(Name = "Code de compétence")]
        public string CodeCompetence { get; set; }
        [Display(Name = "Completion")]
        public bool Complete { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }

        [Display(Name = "Code de compétence")]
        public Competences CodeCompetenceNavigation { get; set; }
        [Display(Name = "Numéro du cours")]
        public Cours NoCoursNavigation { get; set; }
        [Display(Name = "Numéro de programme")]
        public Programmes NoProgrammeNavigation { get; set; }
    }
}
