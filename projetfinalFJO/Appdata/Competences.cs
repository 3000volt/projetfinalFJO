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
        [Display(Name = "Code de compétence : ")]
        public string CodeCompetence { get; set; }
        [Display(Name = "Obligatoire au cégep : ")]
        public bool? ObligatoireCégep { get; set; }
        [Display(Name = "Titre :")]
        public string Titre { get; set; }
        [Display(Name = "Description de la compétence : ")]
        public string Description { get; set; }
        [Display(Name = "Contexte de réalisation : ")]
        public string ContextRealisation { get; set; }
        public string NomFamille { get; set; }
        public string NoProgramme { get; set; }
        public string NomSequence { get; set; }

        public Programmes NoProgrammeNavigation { get; set; }
        public Famillecompetence NomFamilleNavigation { get; set; }
        public Sequences NomSequenceNavigation { get; set; }
        public ICollection<AnalyseCompétence> AnalyseCompétence { get; set; }
        public ICollection<CompetencesElementCompetence> CompetencesElementCompetence { get; set; }
        public ICollection<CoursCompetences> CoursCompetences { get; set; }
        public ICollection<GroupeCompetence> GroupeCompetence { get; set; }
        public ICollection<RepartirHeureCompetence> RepartirHeureCompetence { get; set; }
        public ICollection<RepartitionHeureCours> RepartitionHeureCours { get; set; }
        public ICollection<RepartitionHeuresession> RepartitionHeuresession { get; set; }
    }
}
