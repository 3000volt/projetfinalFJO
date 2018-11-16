using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class CoursCompetences
    {   
        [Display(Name="Nombre d'heures de la compétence")]
        public int NbHcoursCompetence { get; set; }
        [Display(Name = "Numréro du cours")]
        public string NoCours { get; set; }
        [Display(Name = "Code de compétence")]
        public string CodeCompetence { get; set; }
        [Display(Name = "Complétée")]
        public bool Complete { get; set; }

        public Competences CodeCompetenceNavigation { get; set; }
        public Cours NoCoursNavigation { get; set; }
    }
}
