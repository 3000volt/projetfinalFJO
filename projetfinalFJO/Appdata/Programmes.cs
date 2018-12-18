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
        }
        [Display(Name="Numéro")]
        [Required(ErrorMessage ="Le numéro est obligatoire")]
        public string NoProgramme { get; set; }
        [Display(Name ="Titre du programme")]
        [Required(ErrorMessage = "Le nom du programme est obligatoire")]
        public string NomProgramme { get; set; }
        [Display(Name = "Heures de la formation générale")]
        [Required(ErrorMessage = "Le nombre d'heure de formation générale est obligatoire")]
        public int? NbHeureFormationGenerale { get; set; }
        [Display(Name = "Unités de la formation générale")]
        [Required(ErrorMessage = "Le nombre d'unités est obligatoire")]
        public string NbUniteFormationGenerale { get; set; }
        [Display(Name = "Heures de la formation technique")]
        [Required(ErrorMessage = "Le nombre d'heure de formation technique est obligatoire")]
        public int? NbHeureFormationTechnique { get; set; }
        [Display(Name = "Unités de la formation technique")]
        [Required(ErrorMessage = "Le nombre d'unités de la formation technique est obligatoire")]
        public string NbUniteFormationTechnique { get; set; }
        [Display(Name = "Nombre de compétences obligatoires")]
        [Required(ErrorMessage = "Le nombre de compétences obligatoires est obligatoire")]
        public int? NbCompetencesObligatoires { get; set; }
        [Display(Name = "Nombre de compétences optionnelles")]
        [Required(ErrorMessage = "Le nombre de compétences optionnelles est obligatoire")]
        public int? NbCompetencesOptionnelles { get; set; }
        [Display(Name = "Conditions d'admission")]
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
    }
}
