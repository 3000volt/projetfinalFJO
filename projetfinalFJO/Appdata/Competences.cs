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
        [Display(Name ="Code de compétence")]
        public string CodeCompetence { get; set; }
        [Display(Name = "Obligatoire")]
        public bool? ObligatoireCégep { get; set; }
        public string Description { get; set; }
        [Display(Name = "Contexte de réalisation")]
        public string ContextRealisation { get; set; }
        [Display(Name = "Famille de compétence")]
        public string NomFamille { get; set; }
        [Display(Name = "Numéro de programme")]
        public string NoProgramme { get; set; }
        [Display(Name = "Séquence")]
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
