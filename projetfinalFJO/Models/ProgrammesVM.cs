using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetfinalFJO.Models
{
    public class ProgrammesVM
    {
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }
        [Display(Name = "Nom du programme")]
        public string NomProgramme { get; set; }
        [Display(Name = "Nombre d'heures")]
        public int? NbHeure { get; set; }
        [Display(Name = "Nombre d'unité")]
        public int? NbUnite { get; set; }
        [Display(Name = "Nombre de compétences obligatoires")]
        public int? NbCompetencesObligatoires { get; set; }
        [Display(Name = "Nombre de compétences optionnelles")]
        public int? NbCompetencesOptionnelles { get; set; }
        [Display(Name = "Conditions d'admissions")]
        public string CondtionsAdmission { get; set; }
        public bool ProgrammeDeuxAns { get; set; }

    }
}
