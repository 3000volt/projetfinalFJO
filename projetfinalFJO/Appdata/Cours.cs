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
        [Display(Name ="Numéro du cours")]
        public string NoCours { get; set; }
        [Display(Name = "Nom du cours")]
        public string NomCours { get; set; }
        [Display(Name = "Pondération")]
        public string PonderationCours { get; set; }
        [Display(Name = "Département")]
        public string DepartementCours { get; set; }
        [Display(Name = "Type de cours")]
        public string TypedeCours { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }
        [Display(Name = "Session")]
        public string NomSession { get; set; }
        [Display(Name = "Groupe de compétence")]
        public string NomGroupe { get; set; }

        [Display(Name = "Numéro de programme")]
        public Programmes NoProgrammeNavigation { get; set; }
        [Display(Name = "Groupe de compétence")]
        public Groupe NomGroupeNavigation { get; set; }
        [Display(Name = "Session")]
        public Session NomSessionNavigation { get; set; }
        [Display(Name = "")]
        public ICollection<CoursCompetences> CoursCompetences { get; set; }
        [Display(Name = "")]
        public ICollection<Prealables> Prealables { get; set; }
        [Display(Name = "")]
        public ICollection<RepartitionHeureCours> RepartitionHeureCours { get; set; }
    }
}
