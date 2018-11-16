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
        [Display(Name = "Nom du cours")]
        public string NomCours { get; set; }
        [Display(Name = "Pondération")]
        public string PonderationCours { get; set; }
        [Display(Name = "Département")]
        public string DepartementCours { get; set; }
        [Display(Name = "Type de cours")]
        public string TypedeCours { get; set; }
        [Display(Name = "Numero de programme")]
        public string NoProgramme { get; set; }
        [Display(Name = "Session")]
        public string NomSession { get; set; }
        [Display(Name = "Groupe")]
        public string NomGroupe { get; set; }
        public Programmes NoProgrammeNavigation { get; set; }
        public Groupe NomGroupeNavigation { get; set; }
        public Session NomSessionNavigation { get; set; }
        public ICollection<CoursCompetences> CoursCompetences { get; set; }
        public ICollection<Prealables> Prealables { get; set; }
        public ICollection<RepartitionHeureCours> RepartitionHeureCours { get; set; }
    }
}
