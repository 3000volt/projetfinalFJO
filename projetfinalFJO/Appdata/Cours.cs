using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class Cours
    {
        public Cours()
        {
            CoursCompetences = new HashSet<CoursCompetences>();
            Prealables = new HashSet<Prealables>();
            RepartitionHeureCours = new HashSet<RepartitionHeureCours>();
        }
        [Display(Name ="Numéro de cours")]
        public string NoCours { get; set; }
        [Display(Name = "Titre du cours")]
        public string NomCours { get; set; }
        [Display(Name = "Ponderation")]
        public string PonderationCours { get; set; }
        [Display(Name = "Département")]
        public string DepartementCours { get; set; }
        [Display(Name = "Type")]
        public string TypedeCours { get; set; }
        [Display(Name = "Programme")]
        public string NoProgramme { get; set; }
        [Display(Name = "Session")]
        public string NomSession { get; set; }
        [Display(Name = "Groupe de compétence")]
        public string NomGroupe { get; set; }

        [Display(Name = "Programme")]
        public Programmes NoProgrammeNavigation { get; set; }
        [Display(Name = "Groupe de compétence")]
        public Groupe NomGroupeNavigation { get; set; }
        [Display(Name = "Session")]
        public Session NomSessionNavigation { get; set; }
        [Display(Name = "Compétences")]
        public ICollection<CoursCompetences> CoursCompetences { get; set; }
        [Display(Name = "Préalables")]
        public ICollection<Prealables> Prealables { get; set; }
        [Display(Name = "Répartition des heures")]
        public ICollection<RepartitionHeureCours> RepartitionHeureCours { get; set; }
    }
}
