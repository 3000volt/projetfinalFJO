using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class CoursCompetences
    {
        [Display(Name = "Nombre d'heures de la compétence")]
        public int? NbHcoursCompetence { get; set; }
        [Display(Name = "Numéro du cours")]
        public string NoCours { get; set; }
        [Display(Name = "Code de la compétence")]
        public string CodeCompetence { get; set; }
        [Display(Name = "Compétence complétée?")]
        public bool Complete { get; set; }
        [Display(Name = "Numéro du programme")]
        public string NoProgramme { get; set; }

        public Competences CodeCompetenceNavigation { get; set; }
        public Cours NoCoursNavigation { get; set; }
        public Programmes NoProgrammeNavigation { get; set; }
    }
}
