using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class Competences
    {
        public Competences()
        {
            AnalyseCompétence = new HashSet<AnalyseCompétence>();
            CompetencesElementCompetence = new HashSet<CompetencesElementCompetence>();
            CoursCompetences = new HashSet<CoursCompetences>();
            GroupeCompetence = new HashSet<GroupeCompetence>();
            RepartirHeureCompetence = new HashSet<RepartirHeureCompetence>();
            RepartitionHeureCours = new HashSet<RepartitionHeureCours>();
            RepartitionHeuresession = new HashSet<RepartitionHeuresession>();
        }
        [Display(Name ="Code de la compétence")]
        public string CodeCompetence { get; set; }
        [Display(Name = "Compétence obligatoire")]
        public bool? ObligatoireCégep { get; set; }
        public string Description { get; set; }
        [Display(Name = "Contexte de réalisation")]
        public string ContextRealisation { get; set; }
        [Display(Name = "Famille")]
        public string NomFamille { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }
        [Display(Name = "Séquence")]
        public string NomSequence { get; set; }

        [Display(Name = "Numéro de programme")]
        public Programmes NoProgrammeNavigation { get; set; }
        [Display(Name = "Famille")]
        public Famillecompetence NomFamilleNavigation { get; set; }
        [Display(Name = "Séquence")]
        public Sequences NomSequenceNavigation { get; set; }
        [Display(Name = "")]
        public ICollection<AnalyseCompétence> AnalyseCompétence { get; set; }
        [Display(Name = "")]
        public ICollection<CompetencesElementCompetence> CompetencesElementCompetence { get; set; }
        [Display(Name = "")]
        public ICollection<CoursCompetences> CoursCompetences { get; set; }
        [Display(Name = "")]
        public ICollection<GroupeCompetence> GroupeCompetence { get; set; }
        [Display(Name = "")]
        public ICollection<RepartirHeureCompetence> RepartirHeureCompetence { get; set; }
        [Display(Name = "")]
        public ICollection<RepartitionHeureCours> RepartitionHeureCours { get; set; }
        [Display(Name = "")]
        public ICollection<RepartitionHeuresession> RepartitionHeuresession { get; set; }
    }
}
