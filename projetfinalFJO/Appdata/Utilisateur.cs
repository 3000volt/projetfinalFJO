using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projetfinalFJO.Appdata
{
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            AnalyseCompétence = new HashSet<AnalyseCompétence>();
            AnalyseElementsCompetence = new HashSet<AnalyseElementsCompetence>();
            Commentaires = new HashSet<Commentaires>();
            Membresdesactualisations = new HashSet<Membresdesactualisations>();
            RepartitionHeureCours = new HashSet<RepartitionHeureCours>();
            RepartitionHeuresession = new HashSet<RepartitionHeuresession>();
        }
        [Display(Name ="Courriel")]
        public string AdresseCourriel { get; set; }
        [Display(Name = "Date d'enregistrement")]
        public DateTime RegisterDate { get; set; }
        [Display(Name ="Nom")]
        public string Nom { get; set; }
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        public ICollection<AnalyseCompétence> AnalyseCompétence { get; set; }
        public ICollection<AnalyseElementsCompetence> AnalyseElementsCompetence { get; set; }
        public ICollection<Commentaires> Commentaires { get; set; }
        public ICollection<Membresdesactualisations> Membresdesactualisations { get; set; }
        public ICollection<RepartitionHeureCours> RepartitionHeureCours { get; set; }
        public ICollection<RepartitionHeuresession> RepartitionHeuresession { get; set; }
    }
}
