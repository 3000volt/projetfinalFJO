﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class CoursCompetences
    {
        [Display(Name = "")]
        public int? NbHcoursCompetence { get; set; }
        [Display(Name = "")]
        public string NoCours { get; set; }
        [Display(Name = "")]
        public string CodeCompetence { get; set; }
        [Display(Name = "")]
        public bool Complete { get; set; }
        [Display(Name = "")]
        public string NoProgramme { get; set; }

        public Competences CodeCompetenceNavigation { get; set; }
        public Cours NoCoursNavigation { get; set; }
        public Programmes NoProgrammeNavigation { get; set; }
    }
}
