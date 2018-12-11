using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace projetfinalFJO.Appdata
{
    public partial class ActualisationContext : DbContext
    {
        private readonly string ConnectionString;

        public ActualisationContext()
        {
        }

        public ActualisationContext(DbContextOptions<ActualisationContext> options)
            : base(options)
        {
        }

        public ActualisationContext(string connexion)
        {
            this.ConnectionString = connexion;
        }

        public void InsererMembresdesactualisations(Membresdesactualisations me)
        {
            //utiliser le connectionString pour pouvoir affecter la BD
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                //Requete pour ajouter un livre
                string sqlStr = "insert into Membresdesactualisations(NumActualisation, AdresseCourriel) values(@NumActualisation, @AdresseCourriel)";
                //Code pour affecter la BD
                SqlCommand cmd = new SqlCommand(sqlStr, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.Parameters.AddWithValue("NumActualisation", me.NumActualisation);
                cmd.Parameters.AddWithValue("AdresseCourriel", me.AdresseCourriel);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void SupprimerActualisation(int numAct)
        {
            //utiliser le connectionString pour pouvoir affecter la BD
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                //requete pour supprimer un livre
                string sqlStr = "delete from ActualisationInformation where NumActualisation = @numActualisation";
                //Code pour Affecter la BD
                SqlCommand cmd = new SqlCommand(sqlStr, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                //Associer la valeur de l isbn en paramettre
                cmd.Parameters.AddWithValue("numActualisation", numAct);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void RetirerMembreActu(int numAct, string email)
        {
            //utiliser le connectionString pour pouvoir affecter la BD
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                //requete pour supprimer un livre
                string sqlStr = "delete from Membresdesactualisations where NumActualisation = @numActualisation and AdresseCourriel = @adresseCourriel";
                //Code pour Affecter la BD
                SqlCommand cmd = new SqlCommand(sqlStr, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                //Associer la valeur de l isbn en paramettre
                cmd.Parameters.AddWithValue("numActualisation", numAct);
                cmd.Parameters.AddWithValue("adresseCourriel", email);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void SupprimerUtilisateur(string email)
        {
            //utiliser le connectionString pour pouvoir affecter la BD
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                //requete pour supprimer un livre
                string sqlStr = "delete from Utilisateur where AdresseCourriel = @adresseCourriel";
                //Code pour Affecter la BD
                SqlCommand cmd = new SqlCommand(sqlStr, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                //Associer la valeur de l isbn en paramettre
                cmd.Parameters.AddWithValue("adresseCourriel", email);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public virtual DbSet<ActualisationInformation> ActualisationInformation { get; set; }
        public virtual DbSet<AnalyseCompétence> AnalyseCompétence { get; set; }
        public virtual DbSet<AnalyseElementsCompetence> AnalyseElementsCompetence { get; set; }
        public virtual DbSet<Commentaires> Commentaires { get; set; }
        public virtual DbSet<Competences> Competences { get; set; }
        public virtual DbSet<CompetencesElementCompetence> CompetencesElementCompetence { get; set; }
        public virtual DbSet<Cours> Cours { get; set; }
        public virtual DbSet<CoursCompetences> CoursCompetences { get; set; }
        public virtual DbSet<Elementcompetence> Elementcompetence { get; set; }
        public virtual DbSet<Famillecompetence> Famillecompetence { get; set; }
        public virtual DbSet<Groupe> Groupe { get; set; }
        public virtual DbSet<GroupeCompetence> GroupeCompetence { get; set; }
        public virtual DbSet<Membresdesactualisations> Membresdesactualisations { get; set; }
        public virtual DbSet<Prealables> Prealables { get; set; }
        public virtual DbSet<Programmes> Programmes { get; set; }
        public virtual DbSet<RepartirHeureCompetence> RepartirHeureCompetence { get; set; }
        public virtual DbSet<RepartitionHeureCours> RepartitionHeureCours { get; set; }
        public virtual DbSet<RepartitionHeuresession> RepartitionHeuresession { get; set; }
        public virtual DbSet<Sequences> Sequences { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<Utilisateur> Utilisateur { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(" Server=localhost;Database=Actualisation ;User Id=sa;Password=sql");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActualisationInformation>(entity =>
            {
                entity.HasKey(e => e.NumActualisation);

                entity.Property(e => e.NoProgramme)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.NomActualisation).HasMaxLength(50);

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.ActualisationInformation)
                    .HasForeignKey(d => d.NoProgramme)
                    .HasConstraintName("FK__Actualisa__NoPro__76969D2E");
            });

            modelBuilder.Entity<AnalyseCompétence>(entity =>
            {
                entity.HasKey(e => e.IdAnalyseAc);

                entity.Property(e => e.IdAnalyseAc).HasColumnName("Id_Analyse_AC");

                entity.Property(e => e.AdresseCourriel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CodeCompetence)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Context)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.NiveauTaxonomique)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NoProgramme)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Reformulation)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.SavoirEtreProgramme).HasColumnType("text");

                entity.Property(e => e.SavoirFaireProgramme).HasColumnType("text");

                entity.HasOne(d => d.AdresseCourrielNavigation)
                    .WithMany(p => p.AnalyseCompétence)
                    .HasForeignKey(d => d.AdresseCourriel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AnalyseCo__Adres__6477ECF3");

                entity.HasOne(d => d.CodeCompetenceNavigation)
                    .WithMany(p => p.AnalyseCompétence)
                    .HasForeignKey(d => d.CodeCompetence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AnalyseCo__CodeC__656C112C");

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.AnalyseCompétence)
                    .HasForeignKey(d => d.NoProgramme)
                    .HasConstraintName("FK__AnalyseCo__NoPro__66603565");
            });

            modelBuilder.Entity<AnalyseElementsCompetence>(entity =>
            {
                entity.HasKey(e => new { e.IdAnalyseAc, e.AdresseCourriel, e.ElementCompétence });

                entity.Property(e => e.IdAnalyseAc)
                    .HasColumnName("Id_Analyse_AC")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AdresseCourriel).HasMaxLength(100);

                entity.Property(e => e.ElementCompétence).HasMaxLength(50);

                entity.Property(e => e.Context)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.NiveauTaxonomique)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NoProgramme).HasMaxLength(15);

                entity.Property(e => e.Reformulation)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.SavoirEtreProgramme).HasColumnType("text");

                entity.Property(e => e.SavoirFaireProgramme).HasColumnType("text");

                entity.HasOne(d => d.AdresseCourrielNavigation)
                    .WithMany(p => p.AnalyseElementsCompetence)
                    .HasForeignKey(d => d.AdresseCourriel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AnalyseEl__Adres__02FC7413");

                entity.HasOne(d => d.ElementCompétenceNavigation)
                    .WithMany(p => p.AnalyseElementsCompetence)
                    .HasForeignKey(d => d.ElementCompétence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AnalyseEl__Eleme__02084FDA");

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.AnalyseElementsCompetence)
                    .HasForeignKey(d => d.NoProgramme)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__AnalyseEl__NoPro__01142BA1");
            });

            modelBuilder.Entity<Commentaires>(entity =>
            {
                entity.HasKey(e => e.NumCom);

                entity.Property(e => e.AdresseCourriel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Categorie)
                    .IsRequired()
                    .HasColumnName("categorie")
                    .HasMaxLength(50);

                entity.Property(e => e.NoProgramme)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.TexteCom)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.AdresseCourrielNavigation)
                    .WithMany(p => p.Commentaires)
                    .HasForeignKey(d => d.AdresseCourriel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Commentai__Adres__5BE2A6F2");

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.Commentaires)
                    .HasForeignKey(d => d.NoProgramme)
                    .HasConstraintName("FK__Commentai__NoPro__5CD6CB2B");
            });

            modelBuilder.Entity<Competences>(entity =>
            {
                entity.HasKey(e => e.CodeCompetence);

                entity.Property(e => e.CodeCompetence)
                    .HasMaxLength(15)
                    .ValueGeneratedNever();

                entity.Property(e => e.ContextRealisation).HasColumnType("text");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.NoProgramme)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.NomFamille).HasMaxLength(30);

                entity.Property(e => e.NomSequence).HasMaxLength(25);

                entity.Property(e => e.Titre)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.Competences)
                    .HasForeignKey(d => d.NoProgramme)
                    .HasConstraintName("FK__Competenc__NoPro__5AEE82B9");

                entity.HasOne(d => d.NomFamilleNavigation)
                    .WithMany(p => p.Competences)
                    .HasForeignKey(d => d.NomFamille)
                    .HasConstraintName("FK__Competenc__NomFa__59FA5E80");

                entity.HasOne(d => d.NomSequenceNavigation)
                    .WithMany(p => p.Competences)
                    .HasForeignKey(d => d.NomSequence)
                    .HasConstraintName("FK__Competenc__NomSe__7D439ABD");
            });

            modelBuilder.Entity<CompetencesElementCompetence>(entity =>
            {
                entity.HasKey(e => new { e.CodeCompetence, e.ElementCompétence });

                entity.Property(e => e.CodeCompetence).HasMaxLength(15);

                entity.Property(e => e.ElementCompétence).HasMaxLength(50);

                entity.Property(e => e.NoProgramme)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.CodeCompetenceNavigation)
                    .WithMany(p => p.CompetencesElementCompetence)
                    .HasForeignKey(d => d.CodeCompetence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Competenc__CodeC__693CA210");

                entity.HasOne(d => d.ElementCompétenceNavigation)
                    .WithMany(p => p.CompetencesElementCompetence)
                    .HasForeignKey(d => d.ElementCompétence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Competenc__Eleme__6A30C649");

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.CompetencesElementCompetence)
                    .HasForeignKey(d => d.NoProgramme)
                    .HasConstraintName("FK__Competenc__NoPro__6B24EA82");
            });

            modelBuilder.Entity<Cours>(entity =>
            {
                entity.HasKey(e => e.NoCours);

                entity.Property(e => e.NoCours)
                    .HasMaxLength(15)
                    .ValueGeneratedNever();

                entity.Property(e => e.DepartementCours)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NoProgramme)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.NomCours)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NomGroupe).HasMaxLength(15);

                entity.Property(e => e.NomSession).HasMaxLength(15);

                entity.Property(e => e.PonderationCours)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.TypedeCours)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.Cours)
                    .HasForeignKey(d => d.NoProgramme)
                    .HasConstraintName("FK__Cours__NoProgram__5DCAEF64");

                entity.HasOne(d => d.NomGroupeNavigation)
                    .WithMany(p => p.Cours)
                    .HasForeignKey(d => d.NomGroupe)
                    .HasConstraintName("FK__Cours__NomGroupe__7E37BEF6");

                entity.HasOne(d => d.NomSessionNavigation)
                    .WithMany(p => p.Cours)
                    .HasForeignKey(d => d.NomSession)
                    .HasConstraintName("FK__Cours__NomSessio__5EBF139D");
            });

            modelBuilder.Entity<CoursCompetences>(entity =>
            {
                entity.HasKey(e => new { e.NoCours, e.CodeCompetence });

                entity.Property(e => e.NoCours).HasMaxLength(15);

                entity.Property(e => e.CodeCompetence).HasMaxLength(15);

                entity.Property(e => e.NbHcoursCompetence).HasColumnName("NbHCoursCompetence");

                entity.Property(e => e.NoProgramme)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.CodeCompetenceNavigation)
                    .WithMany(p => p.CoursCompetences)
                    .HasForeignKey(d => d.CodeCompetence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CoursComp__CodeC__628FA481");

                entity.HasOne(d => d.NoCoursNavigation)
                    .WithMany(p => p.CoursCompetences)
                    .HasForeignKey(d => d.NoCours)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CoursComp__NoCou__619B8048");

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.CoursCompetences)
                    .HasForeignKey(d => d.NoProgramme)
                    .HasConstraintName("FK__CoursComp__NoPro__6383C8BA");
            });

            modelBuilder.Entity<Elementcompetence>(entity =>
            {
                entity.HasKey(e => e.ElementCompétence);

                entity.Property(e => e.ElementCompétence)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.CriterePerformance)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.NoProgramme)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.Elementcompetence)
                    .HasForeignKey(d => d.NoProgramme)
                    .HasConstraintName("FK__Elementco__NoPro__60A75C0F");
            });

            modelBuilder.Entity<Famillecompetence>(entity =>
            {
                entity.HasKey(e => e.NomFamille);

                entity.Property(e => e.NomFamille)
                    .HasMaxLength(30)
                    .ValueGeneratedNever();

                entity.Property(e => e.NoProgramme)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.Famillecompetence)
                    .HasForeignKey(d => d.NoProgramme)
                    .HasConstraintName("FK__Familleco__NoPro__59063A47");
            });

            modelBuilder.Entity<Groupe>(entity =>
            {
                entity.HasKey(e => e.NomGroupe);

                entity.Property(e => e.NomGroupe)
                    .HasMaxLength(15)
                    .ValueGeneratedNever();

                entity.Property(e => e.NoProgramme)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.Groupe)
                    .HasForeignKey(d => d.NoProgramme)
                    .HasConstraintName("FK__Groupe__NoProgra__5812160E");
            });

            modelBuilder.Entity<GroupeCompetence>(entity =>
            {
                entity.HasKey(e => new { e.NomGroupe, e.CodeCompetence, e.NomSession });

                entity.Property(e => e.NomGroupe).HasMaxLength(15);

                entity.Property(e => e.CodeCompetence).HasMaxLength(15);

                entity.Property(e => e.NomSession).HasMaxLength(15);

                entity.Property(e => e.NoProgramme).HasMaxLength(15);

                entity.HasOne(d => d.CodeCompetenceNavigation)
                    .WithMany(p => p.GroupeCompetence)
                    .HasForeignKey(d => d.CodeCompetence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GroupeCom__CodeC__07C12930");

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.GroupeCompetence)
                    .HasForeignKey(d => d.NoProgramme)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__GroupeCom__NoPro__05D8E0BE");

                entity.HasOne(d => d.NomGroupeNavigation)
                    .WithMany(p => p.GroupeCompetence)
                    .HasForeignKey(d => d.NomGroupe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GroupeCom__NomGr__06CD04F7");

                entity.HasOne(d => d.NomSessionNavigation)
                    .WithMany(p => p.GroupeCompetence)
                    .HasForeignKey(d => d.NomSession)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GroupeCom__NomSe__08B54D69");
            });

            modelBuilder.Entity<Membresdesactualisations>(entity =>
            {
                entity.HasKey(e => new { e.NumActualisation, e.AdresseCourriel });

                entity.Property(e => e.AdresseCourriel).HasMaxLength(100);

                entity.Property(e => e.NoProgramme)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.AdresseCourrielNavigation)
                    .WithMany(p => p.Membresdesactualisations)
                    .HasForeignKey(d => d.AdresseCourriel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Membresde__Adres__787EE5A0");

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.Membresdesactualisations)
                    .HasForeignKey(d => d.NoProgramme)
                    .HasConstraintName("FK__Membresde__NoPro__797309D9");

                entity.HasOne(d => d.NumActualisationNavigation)
                    .WithMany(p => p.Membresdesactualisations)
                    .HasForeignKey(d => d.NumActualisation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Membresde__NumAc__778AC167");
            });

            modelBuilder.Entity<Prealables>(entity =>
            {
                entity.HasKey(e => new { e.NoCoursPrealable, e.NoCours });

                entity.Property(e => e.NoCoursPrealable)
                    .HasColumnName("NoCours_prealable")
                    .HasMaxLength(15);

                entity.Property(e => e.NoCours).HasMaxLength(15);

                entity.Property(e => e.NoProgramme)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.NoCoursNavigation)
                    .WithMany(p => p.Prealables)
                    .HasForeignKey(d => d.NoCours)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Prealable__NoCou__6D0D32F4");

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.Prealables)
                    .HasForeignKey(d => d.NoProgramme)
                    .HasConstraintName("FK__Prealable__NoPro__6E01572D");
            });

            modelBuilder.Entity<Programmes>(entity =>
            {
                entity.HasKey(e => e.NoProgramme);

                entity.Property(e => e.NoProgramme)
                    .HasMaxLength(15)
                    .ValueGeneratedNever();

                entity.Property(e => e.CondtionsAdmission).HasColumnType("text");

                entity.Property(e => e.NbUniteFormationGenerale).HasMaxLength(100);

                entity.Property(e => e.NbUniteFormationTechnique).HasMaxLength(100);

                entity.Property(e => e.NomProgramme).HasMaxLength(100);
            });

            modelBuilder.Entity<RepartirHeureCompetence>(entity =>
            {
                entity.HasKey(e => new { e.CodeCompetence, e.NoProgramme });

                entity.Property(e => e.CodeCompetence).HasMaxLength(15);

                entity.Property(e => e.NoProgramme).HasMaxLength(15);

                entity.Property(e => e.NbHtotalCompetence).HasColumnName("NbHTotalCompetence");

                entity.HasOne(d => d.CodeCompetenceNavigation)
                    .WithMany(p => p.RepartirHeureCompetence)
                    .HasForeignKey(d => d.CodeCompetence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RepartirH__CodeC__6754599E");

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.RepartirHeureCompetence)
                    .HasForeignKey(d => d.NoProgramme)
                    .HasConstraintName("FK__RepartirH__NoPro__68487DD7");
            });

            modelBuilder.Entity<RepartitionHeureCours>(entity =>
            {
                entity.HasKey(e => e.IdAnalyseRhc);

                entity.Property(e => e.IdAnalyseRhc).HasColumnName("Id_Analyse_RHC");

                entity.Property(e => e.AdresseCourriel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CodeCompetence)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.NoCours)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.NoProgramme)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.AdresseCourrielNavigation)
                    .WithMany(p => p.RepartitionHeureCours)
                    .HasForeignKey(d => d.AdresseCourriel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Repartiti__Adres__72C60C4A");

                entity.HasOne(d => d.CodeCompetenceNavigation)
                    .WithMany(p => p.RepartitionHeureCours)
                    .HasForeignKey(d => d.CodeCompetence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Repartiti__CodeC__74AE54BC");

                entity.HasOne(d => d.NoCoursNavigation)
                    .WithMany(p => p.RepartitionHeureCours)
                    .HasForeignKey(d => d.NoCours)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Repartiti__NoCou__73BA3083");

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.RepartitionHeureCours)
                    .HasForeignKey(d => d.NoProgramme)
                    .HasConstraintName("FK__Repartiti__NoPro__75A278F5");
            });

            modelBuilder.Entity<RepartitionHeuresession>(entity =>
            {
                entity.HasKey(e => e.IdAnalyseRhs);

                entity.Property(e => e.IdAnalyseRhs).HasColumnName("Id_Analyse_RHS");

                entity.Property(e => e.AdresseCourriel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CodeCompetence)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.NoProgramme)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.NomSession)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.AdresseCourrielNavigation)
                    .WithMany(p => p.RepartitionHeuresession)
                    .HasForeignKey(d => d.AdresseCourriel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Repartiti__Adres__6EF57B66");

                entity.HasOne(d => d.CodeCompetenceNavigation)
                    .WithMany(p => p.RepartitionHeuresession)
                    .HasForeignKey(d => d.CodeCompetence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Repartiti__CodeC__6FE99F9F");

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.RepartitionHeuresession)
                    .HasForeignKey(d => d.NoProgramme)
                    .HasConstraintName("FK__Repartiti__NoPro__71D1E811");

                entity.HasOne(d => d.NomSessionNavigation)
                    .WithMany(p => p.RepartitionHeuresession)
                    .HasForeignKey(d => d.NomSession)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Repartiti__NomSe__70DDC3D8");
            });

            modelBuilder.Entity<Sequences>(entity =>
            {
                entity.HasKey(e => e.NomSequence);

                entity.Property(e => e.NomSequence)
                    .HasMaxLength(25)
                    .ValueGeneratedNever();

                entity.Property(e => e.NoProgramme).HasMaxLength(15);

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.Sequences)
                    .HasForeignKey(d => d.NoProgramme)
                    .HasConstraintName("FK__Sequences__NoPro__7C4F7684");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => e.NomSession);

                entity.Property(e => e.NomSession)
                    .HasMaxLength(15)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasKey(e => e.AdresseCourriel);

                entity.Property(e => e.AdresseCourriel)
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Prenom)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.RegisterDate).HasColumnType("date");
            });
        }
    }
}
