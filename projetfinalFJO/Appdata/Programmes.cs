using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class Programmes
    {
        public Programmes()
        {
            ActualisationInformation = new HashSet<ActualisationInformation>();
            AnalyseCompétence = new HashSet<AnalyseCompétence>();
            AnalyseElementsCompetence = new HashSet<AnalyseElementsCompetence>();
            Commentaires = new HashSet<Commentaires>();
            Competences = new HashSet<Competences>();
            CompetencesElementCompetence = new HashSet<CompetencesElementCompetence>();
            Cours = new HashSet<Cours>();
            CoursCompetences = new HashSet<CoursCompetences>();
            Elementcompetence = new HashSet<Elementcompetence>();
            Famillecompetence = new HashSet<Famillecompetence>();
            Groupe = new HashSet<Groupe>();
            GroupeCompetence = new HashSet<GroupeCompetence>();
            Membresdesactualisations = new HashSet<Membresdesactualisations>();
            Prealables = new HashSet<Prealables>();
            RepartirHeureCompetence = new HashSet<RepartirHeureCompetence>();
            RepartitionHeureCours = new HashSet<RepartitionHeureCours>();
            RepartitionHeuresession = new HashSet<RepartitionHeuresession>();
            Sequences = new HashSet<Sequences>();
            Session = new HashSet<Session>();
        }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }
        [Display(Name = "Programme")]
        public string NomProgramme { get; set; }
        [Display(Name = "Nombre d'heure")]
        public int? NbHeure { get; set; }
        //[Required]
        //[RegularExpression(@"^(?:\d+\s+\d[/]\d|\d)")]
        [Display(Name = "Nombre d'unité")]
        public int? NbUnite { get; set; }
        [Display(Name = "Nombre de compétences obligatoires")]
        public int? NbCompetencesObligatoires { get; set; }
        [Display(Name = "Nombre de compétences optionnelles")]
        public int? NbCompetencesOptionnelles { get; set; }
        [Display(Name = "Conditions d'admissions")]
        public string CondtionsAdmission { get; set; }

        public ICollection<ActualisationInformation> ActualisationInformation { get; set; }
        public ICollection<AnalyseCompétence> AnalyseCompétence { get; set; }
        public ICollection<AnalyseElementsCompetence> AnalyseElementsCompetence { get; set; }
        public ICollection<Commentaires> Commentaires { get; set; }
        public ICollection<Competences> Competences { get; set; }
        public ICollection<CompetencesElementCompetence> CompetencesElementCompetence { get; set; }
        public ICollection<Cours> Cours { get; set; }
        public ICollection<CoursCompetences> CoursCompetences { get; set; }
        public ICollection<Elementcompetence> Elementcompetence { get; set; }
        public ICollection<Famillecompetence> Famillecompetence { get; set; }
        public ICollection<Groupe> Groupe { get; set; }
        public ICollection<GroupeCompetence> GroupeCompetence { get; set; }
        public ICollection<Membresdesactualisations> Membresdesactualisations { get; set; }
        public ICollection<Prealables> Prealables { get; set; }
        public ICollection<RepartirHeureCompetence> RepartirHeureCompetence { get; set; }
        public ICollection<RepartitionHeureCours> RepartitionHeureCours { get; set; }
        public ICollection<RepartitionHeuresession> RepartitionHeuresession { get; set; }
        public ICollection<Sequences> Sequences { get; set; }
        public ICollection<Session> Session { get; set; }
    }
}
