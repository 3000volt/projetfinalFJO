using System;
using System.Collections.Generic;

namespace projetfinalFJO.Appdata
{
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            AnalyseCompétence = new HashSet<AnalyseCompétence>();
            Commentaires = new HashSet<Commentaires>();
            Membresdesactualisations = new HashSet<Membresdesactualisations>();
            RepartitionHeureCours = new HashSet<RepartitionHeureCours>();
            RepartitionHeuresession = new HashSet<RepartitionHeuresession>();
            UtilisateurPossedeRole = new HashSet<UtilisateurPossedeRole>();
        }

        public string AdresseCourriel { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public ICollection<AnalyseCompétence> AnalyseCompétence { get; set; }
        public ICollection<Commentaires> Commentaires { get; set; }
        public ICollection<Membresdesactualisations> Membresdesactualisations { get; set; }
        public ICollection<RepartitionHeureCours> RepartitionHeureCours { get; set; }
        public ICollection<RepartitionHeuresession> RepartitionHeuresession { get; set; }
        public ICollection<UtilisateurPossedeRole> UtilisateurPossedeRole { get; set; }
    }
}
